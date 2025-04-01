using UnityEngine;
using System.Collections.Generic;


namespace Items.ItemFunctions
{
    public class MOD_SwimmingPool : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;

            return context;
        }
    }
}