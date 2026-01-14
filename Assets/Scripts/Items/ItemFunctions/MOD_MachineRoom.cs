using UnityEngine;
using System.Collections.Generic;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class MOD_MachineRoom : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.MODIFICATION_TRIGGER) return context;
            UpdateGameStatCommand fabric = new UpdateGameStatCommand(context.Env, context.CurrentItem,
                PlayerStats.PlayerStat.FABRIC, effectData.Fabric);
            UpdateGameStatCommand fabricMult = new UpdateGameStatCommand(context.Env, context.CurrentItem,
                PlayerStats.PlayerStat.FABRIC_xMULT, effectData.FabricMult);
            context.Env.CommandInvoker.Execute(fabric);
            context.Env.CommandInvoker.Execute(fabricMult);
            return context;
        }
    }
}