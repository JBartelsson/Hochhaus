using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class WeirdPipe : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;
            
            UpdateGameStatCommand command = new UpdateGameStatCommand(context.Env, context.CurrentItem,
                PlayerStats.PlayerStat.FABRIC_MULT, effectData.FabricMult);
            context.Env.CommandInvoker.Execute(command);

            return context;
        }
    }
}