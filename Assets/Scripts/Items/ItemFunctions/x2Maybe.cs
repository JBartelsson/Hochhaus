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
                Debug.Log($"{context.CurrentItem} did something!");
                UpdateScoreCommand updateScoreCommand =
                    new UpdateScoreCommand(context.Env, Score.ScoreType.xMULT, effectData.MultMult);
                context.Env.CommandInvoker.ExecuteAndRecord(updateScoreCommand);
                Debug.Log("Triggered");
            }

            return context;
        }
    }
}