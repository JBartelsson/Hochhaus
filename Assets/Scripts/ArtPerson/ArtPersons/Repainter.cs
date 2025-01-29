using Art;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Art.ArtPersonFunctions
{
    //EffectColor2 is Color that is exchanged for EffectColor1
    public class Repainter : Art.ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType == Environment.GameEventType.MIX_CARDS)
            {
                Debug.Log("REPAINTER MIX Cards Draw");
            }
            if (gameEventType != Environment.GameEventType.PAINT_ON_FACE) return context;

            if (context.PaintOnContext.BoardField.lastPaintedCard.CardCopy.ColorReference == effectData.EffectColor1)
            {
                context.PaintOnContext.BoardField.ChangeTopColor(effectData.EffectColor2);
            }

            return context;
        }

    }
    
}