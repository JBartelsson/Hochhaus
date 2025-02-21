using System;
using Items;

namespace CommandSystem.Commands
{
    public class AddItemCommand : CommandBase
    {
        private Item _itemToAdd;

        public Item ItemToAdd => _itemToAdd;

        

        public ItemLocations ItemLocation { get; }

        public AddItemCommand(Environment env, Item sender, Item itemToAdd, ItemLocations itemLocation) : base(env, sender)
        {
            _itemToAdd = itemToAdd;
            ItemLocation = itemLocation;
        }

        public override void Execute()
        {
            switch (ItemLocation)
            {
                case ItemLocations.INVENTORY:
                    _env.Inventory.AddItem(_itemToAdd);
                    _env.GameUpdate(Environment.GameStateType.ITEM_ADDED, new Context(_env)
                    {
                        CurrentItem = Sender
                    });
                    break;
                case ItemLocations.CONSUMABLES:
                    break;
                case ItemLocations.SHOP:
                    _env.Shop.Inventory.AddItem(_itemToAdd);
                    _env.GameUpdate(Environment.GameStateType.ITEM_ADDED, new Context(_env)
                    {
                        CurrentItem = Sender
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public override void Undo()
        {
        }
    }
}