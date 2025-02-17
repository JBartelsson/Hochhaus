using System.Linq;
using CommandSystem.Commands;
using UnityEngine;

namespace Items.ItemFunctions
{
    public class BasicPoints : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM) return context;

            TowerRoom placedRoom = context.Env.TowerManager.TowerRooms.Last();
            UpdateGameStatCommand updateScore = new UpdateGameStatCommand(context.Env, context.CurrentItem, Score.ScoreType.POINTS, placedRoom._PlacedCard.CardCopy.RuntimePoints);
            Debug.Log($"placed Room {placedRoom}");
            context.Env.CommandInvoker.ExecuteAndRecord(updateScore);
            return context;
        }
    }
}