using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class PinkSkies : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.MAIN_SCORING) return context;

            return context;
        }
    }
}