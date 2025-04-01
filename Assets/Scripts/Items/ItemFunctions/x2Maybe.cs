using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;
using Utility;


namespace Items.ItemFunctions
{
    public class x2Maybe : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;
            if (ItemUtility.CheckRandom(effectData.FloatValue))
            {
                UpdateGameStatCommand updateGameStatCommand =
                    new UpdateGameStatCommand(context.Env, context.CurrentItem, PlayerStats.PlayerStat.SCORE_MULT, effectData.MultMult);
                context.Env.CommandInvoker.Execute(updateGameStatCommand);
            }

            return context;
        }
    }
}