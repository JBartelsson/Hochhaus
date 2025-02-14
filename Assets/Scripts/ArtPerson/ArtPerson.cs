using System;
using Art.ArtPersonFunctions;
using UnityEngine;

namespace Art
{
    public class ArtPerson: IGameEventReceivable
    {
        private IGameEventReceivable _updateFunction;
        
        private ArtPersonData _artPersonData;

        public ArtPersonData ArtPersonData => _artPersonData;

        public ArtPerson(IGameEventReceivable updateFunction)
        {
            _updateFunction = updateFunction;
        }

        public void SetArtPersonData(ArtPersonData artPersonData)
        {
            _artPersonData = artPersonData;
        }
        
        public Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            if (_updateFunction == null)
            {
                Debug.Log("Art Person has no ability! just like you!");
                return null;
            }
            return _updateFunction.GameUpdate(gameStateType, context);
        }
    }
}