using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands;


namespace Items.ItemFunctions
{
    public class TheRichest : ItemFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (gameStateType != Environment.GameStateType.BUILD_ROOM_END) return context;
            if (context.Env.TowerManager.TowerRooms.Last()._PlacedCard.CardCopy.Sender != null) return context;
            Debug.Log($"LAST BUILDING SCORE IS {context.LastScore.RoomScore}");;
            if (context.LastScore.RoomScore >= effectData.FloatValue)
            {
                Card newCard = new Card(effectData.EffectRoom1, context.Env, context.CurrentItem);
                CreateRoomCommand createRoomCommand =
                    new CreateRoomCommand(context.Env, newCard, context.CurrentItem);
                context.Env.CommandInvoker.Execute(createRoomCommand);
            }
            return context;
        }
    }
}