using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class Ladder : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;
            UpdateGameStatCommand updateGameStatCommand =
                new UpdateGameStatCommand(context.Env, context.CurrentItem, PlayerStats.PlayerStat.SCORE_xMULT, effectData.MultMult);
            context.Env.CommandInvoker.Execute(updateGameStatCommand);
            return context;
        }
    }
}