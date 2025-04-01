using System.Collections.Generic;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class RerollShopCommand : CommandBase
    {
        public int CustomCost { get; }

        public RerollShopCommand(Environment env, Item sender, int cost = -1, CommandBase parent = null) : base(env, sender, parent)
        {
            CustomCost = cost;
        }

        protected override void ExecuteSingle()
        {
            float cost = CustomCost == -1 ? -_env.PlayerStats.Stats.RerollCost : CustomCost;
            UpdateGameStatCommand updateFabric =
                new UpdateGameStatCommand(_env, sender, PlayerStats.PlayerStat.FABRIC, cost);

           AddChild(updateFabric);
            for (var i = _env.Shop.Items.Items.Count - 1; i >= 0; i--)
            {
                Item item = _env.Shop.Items.Items[i];
                ItemCommand removeItemCommand =
                    new ItemCommand(_env, Sender, item, ItemLocations.SHOP, ItemCommand.Mode.REMOVE);
                AddChild(removeItemCommand);
            }

            for (int i = 0; i < _env.PlayerStats.Stats.ShopSizeItems; i++)
            {
                AddRandomShopItemCommand addRandomShopItemCommand =
                    new AddRandomShopItemCommand(_env, sender, ItemClass.Item);
                AddChild(addRandomShopItemCommand);

            }

            // for (int i = 0; i < _env.PlayerStats.Stats.ShopSizeConsumables; i++)
            // {
            //     AddRandomShopItemCommand addRandomShopItemCommand =
            //         new AddRandomShopItemCommand(_env, sender, ItemClass.Consumable);
            //     AddChild(addRandomShopItemCommand);
            //
            // }
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}