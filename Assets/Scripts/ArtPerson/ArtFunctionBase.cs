using UnityEngine;

namespace Art
{
    public class ArtFunctionBase : IGameEventReceivable
    {

        
    

    public ArtPersonData effectData { get; set; }

        public ArtFunctionBase(ArtPersonData effectData)
        {
            this.effectData = effectData;
        }

        protected ArtFunctionBase()
        {
        }

        public ArtFunctionBase SetEffectData(ArtPersonData effectData)
        {
            this.effectData = effectData;
            return this;
        }

        public Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            Debug.LogError("Base ArtPerson Executed! This is bad!");
            return context;
        }
    }
}