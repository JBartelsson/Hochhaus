using UnityEngine;

namespace Items
{
    public class ItemFunctionBase : IGameEventReceivable
    {

        
    

    public ItemData effectData { get; set; }

        public ItemFunctionBase(ItemData effectData)
        {
            this.effectData = effectData;
        }

        protected ItemFunctionBase()
        {
        }

        public ItemFunctionBase SetEffectData(ItemData effectData)
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