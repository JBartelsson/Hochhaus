using System;
using System.Collections;
using System.Collections.Generic;
using Art;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

public class Environment : MonoBehaviour, IInitHandler, IGameEventReceivable
{
    [SerializeField] ColorMixingDatabase colorMixingDatabase;
    [SerializeField] private EnvSettings envSettings;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private ArtPersonLibrary artPersonLibrary;


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

    //Events

    public enum GameEventType
    {
        MIX_CARDS,
        PAINT_ON_FACE,
        DRAW_CARDS
    }

   

    private Score _score;

    public Score Score => _score;

    public void Init()
    {
        _score = new Score();
        _cardSystem = new CardSystem(colorMixingDatabase, envSettings);
        artPersonGroup = new ArtPersonGroup(artPersonLibrary);
    }

    public void StartEnvironment()
    {
        _cardSystem.Shuffle();
        _cardSystem.DrawFullHand();
        _score.ResetScore();
        artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.Repainter));
        artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.TheBlue));
        artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.LoudNeighbors));
        artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.DrawingAssistant));
        boardManager.InitBoard();
    }

    public void MixHandCards(Card topCard, Card bottomCard)
    {
        _cardSystem.MixHandCards(topCard, bottomCard);
        Context ctx = new Context(this)
        {
           CardMixingContext = new Context.CardMixingContextClass()
           {
               TopCard = topCard,
               BottomCard = bottomCard
           }
        };
        GameUpdate(GameEventType.MIX_CARDS, ctx);
    }

    public void AddCardColorToFace(Card card, BoardField boardField)
    {
        Score.AddPoints(card.RuntimePoints);
        boardField.AddCard(card);
        Context ctx = new Context(this)
        {
            PaintOnContext = new Context.PaintOnContextClass()
            {
                BoardField = boardField
            }
        };
        _cardSystem.DiscardCard(card);
        GameUpdate(GameEventType.PAINT_ON_FACE, ctx);
    }



    public Context GameUpdate(GameEventType gameEventType, Context context)
    {
        artPersonGroup.GameUpdate(gameEventType, context);
        return context;
    }
}