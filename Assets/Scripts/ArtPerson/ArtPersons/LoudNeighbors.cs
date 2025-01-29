namespace Art.ArtPersonFunctions
{
    public class LoudNeighbors : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.MAIN_SCORING) return context;
            if (context.MainPhaseContext.ScoringBoardField.lastPaintedCard == null) return context;

            int neighborsCount = context.MainPhaseContext.ScoringBoardField.FilterNeighbors((boardField) =>
            {
                if (boardField.TopPaintedCard == null) return false;
                return boardField.TopPaintedCard.CardCopy.ColorReference == effectData.EffectColor1;
            }).Count;
            
            if (neighborsCount > 0)
            {
                context.Env.RoundStats.Score.AddMultiplier(effectData.MultEffect * neighborsCount);
            }

            return context;
        }
    }
}