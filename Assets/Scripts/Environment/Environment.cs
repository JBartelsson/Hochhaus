using System;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using WeekSystem;

public class Environment : MonoBehaviour, IInitHandler, IGameEventReceivable
{
    private CommandInvoker _commandInvoker;
    [SerializeField] private EnvSettings envSettings;
    [SerializeField] private EnvSettings debugEnvSettings;


    [FormerlySerializedAs("boardManager")] [SerializeField]
    private TowerManager towerManager;

    [FormerlySerializedAs("artPersonLibrary")] [SerializeField]
    private ItemLibrary itemLibrary;

    [SerializeField] private WeekManager _weekManager;

    [Header("Debug Settings")] [SerializeField]
    private bool debug;
    public WeekManager WeekManager => _weekManager;

    public CommandInvoker CommandInvoker => _commandInvoker;


    public TowerManager TowerManager => towerManager;

    private CardSystem _cardSystem;

    public CardSystem CardSystem => _cardSystem;

    private Inventory _inventory;
    public ItemLibrary ItemLibrary => itemLibrary;

    public Inventory Inventory => _inventory;
    public EnvSettings EnvSettings => envSettings;

    private Shop shop;

    public Shop Shop => shop;

    public bool BlockActions { get; set; }

    //Events

    public enum GameStateType
    {
        BUILD_ROOM,
        BUILD_ROOM_END,
        BUILD_ROOM_START,
        ITEM_ADDED,
        ITEM_REMOVED
    }


    private PlayerStats _playerStats;

    public PlayerStats PlayerStats => _playerStats;


    private Context ctx;

    public Context Ctx => ctx;

    public event Action<Environment, GameStateType> GameStateUpdate;

    public void Init()
    {
        #if UNITY_EDITOR
        if (debug)
        {
            envSettings = debugEnvSettings;
        }
        #endif
        ctx = new Context(this);
        _commandInvoker = new CommandInvoker(this);
        _playerStats = new PlayerStats(envSettings);
       
        _cardSystem = new CardSystem(this);
        _inventory = new Inventory(itemLibrary);
        shop = new Shop(this);
    }

    public void StartEnvironment()
    {
        _playerStats.Init();
        _cardSystem.DrawNewHand();
        towerManager.SetEnvironment(this);
        
        

        // _inventory.AddArtPerson(itemLibrary.CreateArtPerson(ArtPersonType.PinkSkies));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.DrawingAssistant));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.Repainter));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.TheBlue));
        // artPersonGroup.AddArtPerson(artPersonLibrary.CreateArtPerson(ArtPersonType.LoudNeighbors));
        // towerManager.InitTower();
    }

    private void TestItems()
    {
        AddItemCommand addItemCommand = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.BasicPoints),
            ItemLocations.INVENTORY);
        _commandInvoker.ExecuteAndRecord(addItemCommand);
        // AddItemCommand addItemCommand23 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.DrawChance1));
        // _commandInvoker.ExecuteAndRecord(addItemCommand23);
        AddItemCommand addItemCommand22 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.ShinyNail), ItemLocations.INVENTORY);
        _commandInvoker.ExecuteAndRecord(addItemCommand22);
        AddItemCommand addItemCommand3 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.x2Maybe), ItemLocations.INVENTORY);
        _commandInvoker.ExecuteAndRecord(addItemCommand3);
        // AddItemCommand addItemCommand4 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.TheRichest));
        // _commandInvoker.ExecuteAndRecord(addItemCommand4);
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            TestItems();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddRandomShopItemCommand randomShopItemCommand = new AddRandomShopItemCommand(this, null);
            _commandInvoker.ExecuteAndRecord(randomShopItemCommand);
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

    public void EndRoomBuilding()
    {
        PlayerStats.Score.CalculateScore();
        Score lastScore = (Score)PlayerStats.Score.Clone();
        PlayerStats.Score.ResetRoomScore();
        GameUpdate(GameStateType.BUILD_ROOM_END, new Context(this)
        {
            LastScore = lastScore
        });
        
    }


    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public Context GameUpdate(GameStateType gameStateType, Context context = null)
    {
        if (context == null)
        {
            context = new Context(this) { };
        }

        context = _inventory.GameUpdate(gameStateType, context);
        GameStateUpdate?.Invoke(this, gameStateType);
        return context;
    }
}