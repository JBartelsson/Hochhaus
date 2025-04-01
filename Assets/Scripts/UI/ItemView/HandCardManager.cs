using CommandSystem.Commands;
using Items;
using Object = System.Object;

namespace UI
{
    public class HandCardManager : DraggableManager
    {
        public void AddHandItem(AddCardToHandCommand addCardToHandCommand)
        {
            DraggableItem itemObject = AddDraggableItem(addCardToHandCommand.Index);
            Card addCard = addCardToHandCommand.Card;
            SingleCardUI singleCardUI = itemObject.GetComponent<SingleCardUI>();
            singleCardUI.SetCardUI(addCard, UI);
            
        }
    }
}