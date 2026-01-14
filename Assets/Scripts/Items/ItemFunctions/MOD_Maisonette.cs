using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class MOD_Maisonette : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.MODIFICATION_TRIGGER) return context;

            UpdateGameStatCommand fabric = new UpdateGameStatCommand(context.Env, context.CurrentItem,
                PlayerStats.PlayerStat.SCORE_xMULT, effectData.MultMult);
            context.Env.CommandInvoker.Execute(fabric);
            return context;
        }
    }
}