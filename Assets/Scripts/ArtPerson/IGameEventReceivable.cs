namespace Art
{
    public interface IGameEventReceivable
    {
        public Context GameUpdate(Environment.GameEventType gameEventType, Context context);
    }
}