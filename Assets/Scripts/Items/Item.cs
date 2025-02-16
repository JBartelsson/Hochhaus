using System;
using UnityEngine;

namespace Items
{
    public class Item: IGameEventReceivable
    {
        private IGameEventReceivable _updateFunction;
        
        private ItemData _itemData;

        public ItemData ItemData => _itemData;

        public Item(IGameEventReceivable updateFunction)
        {
            _updateFunction = updateFunction;
        }

        public void SetArtPersonData(ItemData itemData)
        {
            _itemData = itemData;
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

        public override string ToString()
        {
            return _itemData.DisplayName;
        }
    }
}