using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIInventoryManager : UIBase
    {
        [FormerlySerializedAs("boosterDraggableManager")] [FormerlySerializedAs("boosterItemManager")] [SerializeField] ItemManager itemDraggableManager;
        [FormerlySerializedAs("boosterDraggableManager")] [FormerlySerializedAs("boosterItemManager")] [SerializeField] ItemManager shopManager;
        [FormerlySerializedAs("boosterItemManager")] [SerializeField] HandCardManager handCardManager;

        public HandCardManager HandCardManager => handCardManager;

        public ItemManager ItemDraggableManager => itemDraggableManager;
        
        public ItemManager ShopManager => shopManager;

        public override void InitSubscriptions(UIController uiController)
        {
            base.InitSubscriptions(uiController);
            handCardManager.InitSubscriptions(UI);
            itemDraggableManager.InitSubscriptions(UI);
        }

        public override void ResetSubscriptions()
        {
            
        }
    }
}