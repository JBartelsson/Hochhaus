using System;
using System.Collections;
using System.Collections.Generic;
using Art;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using WeekSystem;

public class Environment : MonoBehaviour, IInitHandler, IGameEventReceivable
{
    [SerializeField] ColorMixingDatabase colorMixingDatabase;
    [SerializeField] private EnvSettings envSettings;


    [SerializeField] private BoardManager boardManager;
    [SerializeField] private ArtPersonLibrary artPersonLibrary;
    [SerializeField] private WeekManager _weekManager;

    public WeekManager WeekManager => _weekManager;


    public BoardManager BoardManager
    {
        get => boardManager;
        set => boardManager = value;
    }

    private CardSystem _cardSystem;

    public CardSystem CardSystem => _cardSystem;

    private ArtPersonGroup artPersonGroup;
    public ArtPersonLibrary ArtPersonLibrary => artPersonLibrary;

    public ArtPersonGroup ArtPersonGroup => artPersonGroup;
    public EnvSettings EnvSettings => envSettings;

    //Events

    public enum GameEventType
    {
        MIX_CARDS,
        PAINT_ON_FACE,
        DRAW_CARDS,
        MAIN_SCORING
    }


    private RoundStats _roundStats;

    public RoundStats RoundStats => _roundStats;


    private Context ctx;

    public Context Ctx => ctx;

    public void Init()
    {
        ctx = new Context(this);
        _roundStats = new RoundStats(envSettings);
        _cardSystem = new CardSystem(colorMixingDatabase, this);
        artPersonGroup = new ArtPersonGroup(artPersonLibrary);
    }

    public void StartEnvironment()
    {
        _cardSystem.DrawFullHand();
        _roundStats.Init();
        _weekManager.Init();
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.BasicPoints));
        artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.PinkSkies));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.DrawingAssistant));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.Repainter));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.TheBlue));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.LoudNeighbors));
        boardManager.InitBoard();
    }

    public void MixHandCards(Card topCard, Card bottomCard)
    {
        if (!_cardSystem.MixHandCards(topCard, bottomCard)) return;
        ctx.CardMixingContext = new Context.CardMixingContextClass()
        {
            TopCard = topCard,
            BottomCard = bottomCard
        };
        GameUpdate(GameEventType.MIX_CARDS, ctx);
    }

    public void AddCardColorToFace(Card card, BoardField boardField)
    {
        boardField.AddCard(card);
        ctx.PaintOnContext = new Context.PaintOnContextClass()
        {
            BoardField = boardField
        };
        _cardSystem.DiscardCard(card);
        GameUpdate(GameEventType.PAINT_ON_FACE, ctx);
    }

    public void SellCurrentPainting()
    {
        ctx = GameUpdate(GameEventType.MAIN_SCORING, ctx);
        
        _roundStats.SellPainting();
        if (_weekManager.IsLevelSuccessful(_roundStats.Score))
        {
            Debug.Log("Next Week!");
            _weekManager.NextWeek();
            return;
        }

        if (_roundStats.Stats.AmountOfPaintings <= 0)
        {
            GameOver();
            return;
        }

        _cardSystem.DrawFullHand();
        boardManager.Reset();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public Context GameUpdate(GameEventType gameEventType, Context context)
    {
        context = artPersonGroup.GameUpdate(gameEventType, context);
        return context;
    }
}