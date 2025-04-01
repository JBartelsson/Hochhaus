using System;
using CommandSystem.Commands;
using Items;

namespace UI
{
    public class ItemManager : DraggableManager
    {
        public void AddItem(ItemCommand itemCommand)
        {
            Item addItem = itemCommand.Item;
            DraggableItem itemObject;
            if (!addItem.ItemData.IsBasic)
            {
                itemObject = AddDraggableItem(itemCommand.Index, false);
            UIItem uiItem = itemObject.GetComponent<UIItem>();
            uiItem.SetItem(addItem);
            }
            else
            {
                // itemObject = AddDraggableItem(true);
            }

        }
    }
}