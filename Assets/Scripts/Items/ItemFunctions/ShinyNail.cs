using UnityEngine;
using System.Collections.Generic;


namespace Items.ItemFunctions
{
    public class ShinyNail : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;
            int index = context.Env.Inventory.Items.IndexOf(context.CurrentItem);
            do
            {
                index++;
                if (index >= context.Env.Inventory.Items.Count)
                {
                    return context;
                }
            } while (context.Env.Inventory.Items[index].ItemData.ItemType == ItemType.ShinyNail);

            // context.CurrentItem = context.Env.Inventory.Items[nextIndex];
            context.Env.Inventory.Items[index].GameUpdate(gameStateType, context);
            return context;
        }
    }
}