using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class BasicPoints : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            return context;
        }
    }
}