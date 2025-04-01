using System;
using CommandSystem.Commands;
using UI;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationItemCommand :AnimationCommand
    {
        private ItemCommand _itemCommand;

        public AnimationItemCommand(UIController ui, ItemCommand itemCommand) : base(ui)
        {
            _itemCommand = itemCommand;
        }

        protected override void ExecuteCmd()
        {

            ItemManager location = null;
            switch (_itemCommand.ItemLocation)
            {
                case ItemLocations.INVENTORY:
                    location = ui.UIInventoryManager.ItemDraggableManager;

                    break;
                case ItemLocations.CONSUMABLES:
                    break;
                case ItemLocations.SHOP:
                    location = ui.UIInventoryManager.ShopManager;

                    break;
                default:
                    break;
            }
            
            // Debug.Log($"EXECUTING THIS ITEM COMMAMND AT LOCATION {location} with {_itemCommand.CommandMode}");
            if (_itemCommand.CommandMode == ItemCommand.Mode.ADD)
            {
                location.AddItem(_itemCommand);
            }
            else
            {
                location.RemoveItem(_itemCommand.Index);

            }
        }
    }
}