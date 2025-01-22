namespace Art.ArtPersonFunctions
{
    public class TheBlue : ArtFunctionBase, IGameEventReceivable
    {
        public new Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            if (gameEventType != Environment.GameEventType.PAINT_ON_FACE) return context;

            if (context.PaintOnContext.BoardField.lastPaintedCard.CardCopy.ColorReference == effectData.EffectColor1)
            {
                context.Env.Score.AddPoints(20);
            }

            return context;
        }
    }
}