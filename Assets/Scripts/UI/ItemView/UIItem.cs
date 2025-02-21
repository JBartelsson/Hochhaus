using Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        public void SetItem(Item item)
        {
            if (item == null)
            {
                Debug.LogWarning("Item is null!");
                return;
            }

            itemImage.sprite = item.ItemData.Image;
        }
    }
}