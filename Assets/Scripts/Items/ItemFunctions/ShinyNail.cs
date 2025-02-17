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
            int nextIndex = index + 1;
            if (context.Env.Inventory.Items.Count > nextIndex)
            {
                context.CurrentItem = context.Env.Inventory.Items[nextIndex];
                context.Env.Inventory.Items[nextIndex].GameUpdate(gameStateType, context);
            }
            return context;
        }
    }
}