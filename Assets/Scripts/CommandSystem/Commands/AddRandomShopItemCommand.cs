using Items;

namespace CommandSystem.Commands
{
    public class AddRandomShopItemCommand : CommandBase
    {
        public AddRandomShopItemCommand(Environment env, Item sender) : base(env, sender)
        {
        }

        public override void Execute()
        {
            Item _itemToAdd = _env.ItemLibrary.CreateRandomItem();
            AddItemCommand addItemCommand = new AddItemCommand(_env, Sender, _itemToAdd, ItemLocations.SHOP);
            _env.CommandInvoker.ExecuteAndRecord(addItemCommand);
            
        }

        public override void Undo()
        {
        }
    }
}