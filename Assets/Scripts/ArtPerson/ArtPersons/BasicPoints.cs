using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class BasicPoints : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            return context;
        }
    }
}