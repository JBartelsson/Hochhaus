using UnityEngine;
using System.Collections.Generic;


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
                context.Env.CardSystem.AddDraws(cardDrawAmount);
                Debug.Log("Got Back draw!");
            }
            return context;
        }
    }
}