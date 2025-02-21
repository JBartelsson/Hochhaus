using Items;

public class Shop : EnvBase, IGameEventReceivable
{
    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    public Shop(Environment env) : base(env)
    {
        _inventory = new Inventory(env.ItemLibrary, true);
    }

    public Context GameUpdate(Environment.GameStateType gameStateType, Context context)
    {
       return _inventory.GameUpdate(gameStateType, context);
    }
}