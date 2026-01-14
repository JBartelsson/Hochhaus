using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class UIReorderItemsCommand :AnimationCommand
    {
        public UIReorderItemsCommand(UIController ui) : base(ui)
        {
        }

        protected override void ExecuteCmd()
        {

            List<Item> items = EnvironmentManager.Instance.GetActiveEnvironment().Inventory.Items;
            List<Item> newItems = ReorderList(items, ui.UIInventoryManager.ItemDraggableManager);
            EnvironmentManager.Instance.GetActiveEnvironment().Inventory.Items = newItems;
            List<Card> hand = EnvironmentManager.Instance.GetActiveEnvironment().CardSystem.Hand;
            List<Card> newHand = ReorderList(hand, ui.UIInventoryManager.HandCardManager);
            EnvironmentManager.Instance.GetActiveEnvironment().CardSystem.Hand = newHand;
        }
        
        private List<T> ReorderList<T>(List<T> list, DraggableManager draggableManager) where T : class
        {
            List<T> newList = new List<T>();
            newList.AddRange(list);
            
            
            List<int> indices = draggableManager.Items.Select((x) => x.OriginalIndex).ToList();
            foreach (var draggableManagerItem in draggableManager.Items)
            {
                newList[draggableManagerItem.Index] = list[draggableManagerItem.OriginalIndex];    
                draggableManagerItem.OriginalIndex = draggableManagerItem.Index;
            }
            // Debug.Log(indices.ToFormattedString());
            // Debug.Log("Reordered List: " + newList.ToFormattedString());
            return newList;
        }
    }
    
}