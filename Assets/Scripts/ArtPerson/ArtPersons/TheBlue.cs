using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class TheBlue : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType == Environment.GameEventType.DRAW_CARDS)
            {
                Debug.Log("The Blue Card Draw");
            }
            if (gameEventType != Environment.GameEventType.MAIN_SCORING) return context;

            if (context.MainPhaseContext.ScoringBoardField.lastPaintedCard == null) return context;
            if (context.MainPhaseContext.ScoringBoardField.lastPaintedCard.CardCopy.ColorReference == effectData.EffectColor1)
            {
                context.Env.RoundStats.Score.AddPoints(20);
            }

            return context;
        }
    }
}