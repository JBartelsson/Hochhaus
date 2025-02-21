using Items;
using Object = System.Object;

namespace UI
{
    public class HandCardManager : DraggableManager
    {
        public void AddHandItem(Object item)
        {
            DraggableItem itemObject = AddItem();
            Card addCard = item as Card;
            SingleCardUI singleCardUI = itemObject.GetComponent<SingleCardUI>();
            singleCardUI.SetCardUI(addCard, UI);
            
        }
    }
}