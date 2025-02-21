using Items;

namespace CommandSystem.Commands
{
    public class ReorderItemsCommand : CommandBase
    {
        public ReorderItemsCommand(Environment env, Item sender) : base(env, sender)
        {
        }

        public override void Execute()
        {
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}