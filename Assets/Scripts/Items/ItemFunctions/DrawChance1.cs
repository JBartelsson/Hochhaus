using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class DrawChance1 : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;

            float rng = effectData.FloatValue;
            int cardDrawAmount = effectData.INTValue;
            if (Random.Range(0, 1f) <= rng)
            {
                UpdateGameStatCommand updateGameStatCommand =
                    new UpdateGameStatCommand(context.Env, context.CurrentItem, PlayerStats.PlayerStat.DRAWS, cardDrawAmount );
                context.Env.CommandInvoker.ExecuteAndRecord(updateGameStatCommand);
            }
            return context;
        }
    }
}