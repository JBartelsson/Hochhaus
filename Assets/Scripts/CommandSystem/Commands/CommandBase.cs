namespace CommandSystem.Commands
{
    public abstract class CommandBase : ICommand
    {

        protected Environment _env;
        public abstract void Execute();
        public abstract void Undo();

        public void SetEnv(Environment env)
        {
            _env = env;
        }
        
    }


}