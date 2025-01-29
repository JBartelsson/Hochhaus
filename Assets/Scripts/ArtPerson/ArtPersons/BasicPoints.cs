using UnityEngine;

namespace Art.ArtPersonFunctions
{
    public class BasicPoints : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.MAIN_SCORING) return context;
            BoardField.PaintedCard paintedCard = context.MainPhaseContext.ScoringBoardField.lastPaintedCard;
            if (paintedCard == null) return context;
            Debug.Log($"Painted card: {paintedCard.CardCopy.RuntimePoints}");
            context.Env.RoundStats.Score.AddPoints(paintedCard.CardCopy.RuntimePoints);

            return context;
        }
    }
}