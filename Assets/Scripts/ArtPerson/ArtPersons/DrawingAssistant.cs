using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class DrawingAssistant : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.MIX_CARDS) return context;

            float random = Random.Range(0f, 1f);
            if (random <= effectData.FloatValue)
            {
                context.Env.CardSystem.Draw();
            }

            return context;
        }
    }
}