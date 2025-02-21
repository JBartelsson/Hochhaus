using System;
using CommandSystem.Commands;

namespace CommandSystem.AnimationCommands
{
    public class AnimationAddedItemCommand :AnimationCommand
    {
        private AddItemCommand _addItemCommand;

        public AnimationAddedItemCommand(UIController ui, AddItemCommand addItemCommand) : base(ui)
        {
            _addItemCommand = addItemCommand;
        }

        protected override void ExecuteCmd()
        {
            switch (_addItemCommand.ItemLocation)
            {
                case ItemLocations.INVENTORY:
                    ui.UIInventoryManager.ItemDraggableManager.AddItem(_addItemCommand.ItemToAdd);

                    break;
                case ItemLocations.CONSUMABLES:
                    break;
                case ItemLocations.SHOP:
                    ui.UIInventoryManager.ShopManager.AddItem(_addItemCommand.ItemToAdd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}