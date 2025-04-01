using CommandSystem.Commands;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIShopItem : UIItem, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI priceText;
        public void OnPointerClick(PointerEventData eventData)
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
            UpdateGameStatCommand updateFabric = new UpdateGameStatCommand(env, null, PlayerStats.PlayerStat.FABRIC, -Item.Price);
            env.CommandInvoker.Execute(updateFabric);
            ItemCommand itemCommandAdd = new ItemCommand(env, null, Item, ItemLocations.INVENTORY, ItemCommand.Mode.ADD);
            ItemCommand itemCommandRemove = new ItemCommand(env, null, Item, ItemLocations.SHOP, ItemCommand.Mode.REMOVE);
            env.CommandInvoker.Execute(itemCommandAdd);
            env.CommandInvoker.Execute(itemCommandRemove);
            
        }

        public override void SetItem(Item _item)
        {
            // Debug.Log("Set Item in ShopItem " + _item);
            priceText.text = _item.Price.ToString() + "F";
            base.SetItem(_item);
        }
    }
}