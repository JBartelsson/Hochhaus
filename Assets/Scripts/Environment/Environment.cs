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

    private Inventory _tokens;
    
    public Inventory Tokens => _tokens;
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
        CARD_DRAWN,
        CARD_REMOVED_FROM_HAND,
        GAME_STAT_UPDATE,
        CARD_DESTROYED_FROM_HAND,
        ITEM_REMOVED,
        EMPTY,
        MODIFICATION_TRIGGER,
        TOKEN_TRIGGER
    }


    private PlayerStats _playerStats;

    public PlayerStats PlayerStats => _playerStats;


    private Context ctx;

    public Context Ctx => ctx;
    public void Init()
    {
#if UNITY_EDITOR
        if (debug)
        {
            envSettings = debugEnvSettings;
        }
#endif
        envSettings.Init();
        ctx = new Context(this);
        _commandInvoker = new CommandInvoker(this);
        _playerStats = new PlayerStats(this);

        _cardSystem = new CardSystem(this);
        _inventory = new Inventory(itemLibrary);
        _tokens = new Inventory(itemLibrary);
        shop = new Shop(this);
    }

    public void StartEnvironment()
    {
        _playerStats.Init();
        _cardSystem.DrawNewHand();
        towerManager.SetEnvironment(this);
        shop.RerollFree();
        Invoke(nameof(TestItems),.01f);


        // towerManager.InitTower();
    }

    private void TestItems()
    {
        ItemCommand itemCommand = new ItemCommand(this, null, itemLibrary.CreateItem(ItemType.BasicPoints),
            ItemLocations.INVENTORY, ItemCommand.Mode.ADD);
        _commandInvoker.Execute(itemCommand);
        // ItemCommand addItemCommand23 = new ItemCommand(this, null, itemLibrary.CreateItem(ItemType.Ladder),
        //     ItemLocations.INVENTORY, ItemCommand.Mode.ADD);
        // _commandInvoker.Execute(addItemCommand23);
        // ItemCommand addItemCommand22 = new ItemCommand(this, null, itemLibrary.CreateItem(ItemType.x2Maybe),
        //     ItemLocations.INVENTORY, ItemCommand.Mode.ADD);
        // _commandInvoker.Execute(addItemCommand22);
        // AddItemCommand addItemCommand3 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.x2Maybe), ItemLocations.INVENTORY);
        // _commandInvoker.ExecuteAndRecord(addItemCommand3);
        // AddItemCommand addItemCommand4 = new AddItemCommand(this, null, itemLibrary.CreateItem(ItemType.TheRichest), ItemLocations.INVENTORY);
        // _commandInvoker.ExecuteAndRecord(addItemCommand4);
    }

    private void Update()
    {
        if (!_playerStats.Stats.CanDraw())
        {
            Debug.Log("Gameover!");
            BlockActions = true;
        }

        if (BlockActions) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float time = Time.time;
            PlayHand();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TestItems();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddRandomShopItemCommand randomShopItemCommand = new AddRandomShopItemCommand(this, null, ItemClass.Item);
            _commandInvoker.Execute(randomShopItemCommand);
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
        PlayerStats.CalculateScore();
        PlayerStats lastScore = (PlayerStats)PlayerStats.Clone();
        GameUpdate(GameStateType.BUILD_ROOM_END, new Context(this)
        {
            LastScore = lastScore
        });
        PlayerStats.ResetRoomScore();
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

        if (gameStateType != GameStateType.EMPTY)
        {
            context = _inventory.GameUpdate(gameStateType, context);
        }

        return context;
    }
}