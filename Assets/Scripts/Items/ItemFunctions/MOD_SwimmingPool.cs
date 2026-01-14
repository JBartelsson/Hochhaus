using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;
using NUnit;


namespace Items.ItemFunctions
{
    public class MOD_SwimmingPool : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType == Environment.GameStateType.MODIFICATION_TRIGGER)
            {
            ItemCommand addMod = new ItemCommand( context.Env, context.CurrentItem,
                context.Env.ItemLibrary.CreateItem(ItemType.MOD_SwimmingPool), ItemLocations.TOKENS,
                ItemCommand.Mode.ADD);
            context.Env.CommandInvoker.Execute(addMod);
                
            }

            if (gameStateType == Environment.GameStateType.TOKEN_TRIGGER)
            {
                UpdateGameStatCommand command = new UpdateGameStatCommand(context.Env, context.CurrentItem,
                    PlayerStats.PlayerStat.SCORE_xMULT, effectData.MultMult);
                context.Env.CommandInvoker.Execute(command);
            }
            
            return context;
        }
    }
}