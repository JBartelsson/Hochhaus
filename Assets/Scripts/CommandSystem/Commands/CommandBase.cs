using Items;

namespace CommandSystem.Commands
{
    public abstract class CommandBase : ICommand
    {

        protected Environment _env;

        public Environment Env => _env;


        protected Item sender;

        public Item Sender => sender;
        public abstract void Execute();
        public abstract void Undo();

        public void SetEnv(Environment env)
        {
            _env = env;
        }

        public CommandBase(Environment env, Item sender)
        {
            _env = env;
            this.sender = sender;
        }
        
    }


}