namespace Art.ArtPersonFunctions
{
    public class LoudNeighbors : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.PAINT_ON_FACE) return context;

            int neighborsCount = context.PaintOnContext.BoardField.FilterNeighbors((boardField) =>
            {
                if (boardField.TopPaintedCard == null) return false;
                return boardField.TopPaintedCard.CardCopy.ColorReference == effectData.EffectColor1;
            }).Count;
            if (neighborsCount > 0)
            {
                context.Env.Score.AddMultiplier(effectData.MultEffect * neighborsCount);
            }

            return context;
        }
    }
}