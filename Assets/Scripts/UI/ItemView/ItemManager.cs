using System;
using Items;

namespace UI
{
    public class ItemManager : DraggableManager
    {
        public void AddItem(Object item)
        {
            DraggableItem itemObject = AddItem();
            Item addItem = item as Item;
            UIItem uiItem = itemObject.GetComponent<UIItem>();
            uiItem.SetItem(addItem);
            
        }

        
    }
}