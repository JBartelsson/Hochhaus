using System.Collections.Generic;
using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class PinkSkies : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.MAIN_SCORING) return context;

            List<List<Face>> redFaces = context.Env.BoardManager.Dcel.FindConnectedFaceGroups((face) =>
            {
                if (face.BoardField.TopPaintedCard == null) return false;
                return face.BoardField.TopPaintedCard.CardCopy.ColorReference == effectData.EffectColor1 ||
                       face.BoardField.TopPaintedCard.CardCopy.ColorReference == effectData.EffectColor2;
            });
            foreach (var redFace in redFaces)
            {
                Debug.Log(redFace.ToFormattedString());
            }

            var outerFaces = context.Env.BoardManager.Dcel.GetBoundaryFaces();
            foreach (var keyValuePair in outerFaces)
            {
                Debug.Log($"{keyValuePair.Key} : {keyValuePair.Value.ToFormattedString()}");
            }

            var areas = context.Env.BoardManager.Dcel.GetHalfs();
            foreach (var keyValuePair in areas)
            {
                Debug.Log($"{keyValuePair.Key} : {keyValuePair.Value.ToFormattedString()}");
            }

            return context;
        }
    }
}