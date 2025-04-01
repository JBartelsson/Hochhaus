using Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] protected Image itemImage;
        protected Item Item;
        public virtual void SetItem(Item _item)
        {
            if (_item == null)
            {
                Debug.LogWarning("Item is null!");
                return;
            }
            this.Item = _item;
            itemImage.sprite = Item.ItemData.Image;
        }
    }
}