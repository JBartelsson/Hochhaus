using System;
using System.Collections;
using System.Collections.Generic;
using Art;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using WeekSystem;

public class Environment : MonoBehaviour, IInitHandler
{
    private CommandInvoker _commandInvoker;
    [SerializeField] private EnvSettings envSettings;


    [FormerlySerializedAs("boardManager")] [SerializeField] private TowerManager towerManager;
    [FormerlySerializedAs("artPersonLibrary")] [SerializeField] private ItemLibrary itemLibrary;
    [SerializeField] private WeekManager _weekManager;

    public WeekManager WeekManager => _weekManager;
    
    public CommandInvoker CommandInvoker => _commandInvoker;


    public TowerManager TowerManager => towerManager;

    private CardSystem _cardSystem;

    public CardSystem CardSystem => _cardSystem;

    private Inventory _inventory;
    public ItemLibrary ItemLibrary => itemLibrary;

    public Inventory Inventory => _inventory;
    public EnvSettings EnvSettings => envSettings;
    
    public bool BlockActions { get; set; }

    //Events

    public enum GameEventType
    {
        BUILD_ROOM
    }


    private PlayerStats _playerStats;

    public PlayerStats PlayerStats => _playerStats;


    private Context ctx;

    public Context Ctx => ctx;

    public void Init()
    {
        ctx = new Context(this);
        _commandInvoker = new CommandInvoker(this);
        _playerStats = new PlayerStats(envSettings);
        _cardSystem = new CardSystem(this);
        _inventory = new Inventory(itemLibrary);
    }

    public void StartEnvironment()
    {
        _playerStats.Init();
        _cardSystem.DrawNewHand();
        towerManager.SetEnvironment(this);
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.BasicPoints));
        // _inventory.AddArtPerson(itemLibrary.CreateArtPerson(ArtPersonType.PinkSkies));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.DrawingAssistant));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.Repainter));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.TheBlue));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.LoudNeighbors));
        // towerManager.InitTower();
    }

    private void Update()
    {
        if (_cardSystem.DrawsEmpty())
        {
            Debug.Log("Gameover!");
            BlockActions = true;
        }
        if (BlockActions) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            PlayHand();
            _cardSystem.DrawNewHand();
        }
    }

    public void SetBlockActions(bool block)
    {
        BlockActions = block;
    }

    public void PlayHand()
    {
        _cardSystem.BuildHand();
    }


    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public Context CalculateRoom(GameEventType gameEventType)
    {
        Context context = new Context(this){};
        context = _inventory.GameUpdate(gameEventType, context);
        PlayerStats.Score.EndRoomBuilding();
        return context;
    }
}