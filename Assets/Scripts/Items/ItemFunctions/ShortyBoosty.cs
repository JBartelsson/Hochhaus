using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class ShortyBoosty : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;
            
            if (context.Env.TowerManager.TowerRooms.Last()._PlacedCard.CardCopy.AppartmentReference.AppartmentName == "Shorty")
            {
                UpdateGameStatCommand updateGameStatCommand =
                    new UpdateGameStatCommand(context.Env, context.CurrentItem, Score.ScoreType.POINTS, effectData.PointEffect);
                context.Env.CommandInvoker.ExecuteAndRecord(updateGameStatCommand);
            }
            return context;
        }
    }
}