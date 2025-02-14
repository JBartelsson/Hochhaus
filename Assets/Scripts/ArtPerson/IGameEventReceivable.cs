namespace Art
{
    public interface IGameEventReceivable
    {
        public Context GameUpdate(Environment.GameStateType gameStateType, Context context = null);
    }
}