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
                    new UpdateGameStatCommand(context.Env, context.CurrentItem, Score.ScoreType.xMULT, effectData.MultMult);
                context.Env.CommandInvoker.ExecuteAndRecord(updateGameStatCommand);
            }

            return context;
        }
    }
}