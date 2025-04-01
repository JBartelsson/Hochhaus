using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIInventoryManager : UIBase
    {
        [FormerlySerializedAs("boosterDraggableManager")] [FormerlySerializedAs("boosterItemManager")] [SerializeField] ItemManager itemDraggableManager;
        [FormerlySerializedAs("boosterDraggableManager")] [FormerlySerializedAs("boosterItemManager")] [SerializeField] ItemManager shopManager;
        [FormerlySerializedAs("boosterItemManager")] [SerializeField] HandCardManager handCardManager;
        [SerializeField] private TextMeshProUGUI rerollText;
        public HandCardManager HandCardManager => handCardManager;

        public ItemManager ItemDraggableManager => itemDraggableManager;
        
        public ItemManager ShopManager => shopManager;
        
        public TextMeshProUGUI RerollText => rerollText;

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