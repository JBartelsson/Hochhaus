using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class AddRandomShopItemCommand : CommandBase
    {
        private ItemClass _itemClass;

        public AddRandomShopItemCommand(Environment env, Item sender, ItemClass itemClass, CommandBase parent = null) : base(env, sender, parent)
        {
            _itemClass = itemClass;
        }

        protected override void ExecuteSingle()
        {
            Item _itemToAdd = _env.ItemLibrary.CreateRandomItem(_itemClass);

            if (_itemToAdd == null) return;

            ItemCommand itemCommand = new ItemCommand(_env, Sender, _itemToAdd, ItemLocations.SHOP, ItemCommand.Mode.ADD);
            AddChild(itemCommand);
        }

        public override void Undo()
        {
        }
    }
}