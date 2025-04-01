using System;
using CommandSystem.Commands;
using Items;
[Serializable]
public class Shop : EnvBase, IGameEventReceivable
{
    private Inventory _items;

    public Inventory Items => _items;
    
    public Shop(Environment env) : base(env)
    {
        _items = new Inventory(env.ItemLibrary, true);
    }

    public Context GameUpdate(Environment.GameStateType gameStateType, Context context)
    {
       return _items.GameUpdate(gameStateType, context);
    }

    public void RerollFree()
    {
        RerollShopCommand rerollShopCommand = new RerollShopCommand(env, null, 0);
        env.CommandInvoker.Execute(rerollShopCommand);
    }

    }