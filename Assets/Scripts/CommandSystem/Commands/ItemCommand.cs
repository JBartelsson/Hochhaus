using System;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class ItemCommand : CommandBase
    {
        private Item _item;

        public Item Item => _item;
        
        public int Index { get; set; }

        public enum Mode
        {
            ADD,
            REMOVE
        }

        public Mode CommandMode { get; }


        public ItemLocations ItemLocation { get; }

        public ItemCommand(Environment env, Item sender, Item item, ItemLocations itemLocation, Mode mode, CommandBase parent = null) : 
            base(env, sender, parent)
        {
            CommandMode = mode;
            _item = item;
            ItemLocation = itemLocation;
        }

        protected override void ExecuteSingle()
        {

            Inventory location = null;
            switch (ItemLocation)
            {
                case ItemLocations.INVENTORY:
                    location = _env.Inventory;
                    break;
                case ItemLocations.CONSUMABLES:
                    location = _env.Shop.Items;
                    break;
                case ItemLocations.SHOP:
                    location = _env.Shop.Items;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Environment.GameStateType g;
            if (CommandMode == Mode.ADD)
            {
                g = Environment.GameStateType.ITEM_ADDED;
                location.AddItem(_item);
                Index = location.Items.Count - 1;

            }
            else
            {
                g = Environment.GameStateType.ITEM_REMOVED;
                Index = location.Items.IndexOf(_item);
                location.RemoveItem(_item);
            }
            // Debug.Log("Trying to add Item: " + _item + " to " + ItemLocation + " with mode " + CommandMode + "Index: " + Index);

            _env.GameUpdate(g, new Context(_env)
            {
                CurrentItem = Sender
            });
        }

        public override void Undo()
        {
        }
    }
}