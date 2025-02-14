using System.Linq;
using CommandSystem.Commands;
using UnityEngine;

namespace Items.ItemFunctions
{
    public class BasicPoints : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            TowerRoom placedRoom = context.Env.TowerManager.TowerRooms.Last();
            UpdateScoreCommand updateScore = new UpdateScoreCommand(context.Env, Score.ScoreType.POINTS, placedRoom._PlacedCard.CardCopy.RuntimePoints);
            Debug.Log($"placed Room {placedRoom}");
            context.Env.CommandInvoker.ExecuteAndRecord(updateScore);
            return context;
        }
    }
}