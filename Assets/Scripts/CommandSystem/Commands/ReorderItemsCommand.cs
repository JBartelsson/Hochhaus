using Items;

namespace CommandSystem.Commands
{
    public class ReorderItemsCommand : CommandBase
    {
        public ReorderItemsCommand(Environment env, Item sender, CommandBase parent = null) : base(env, sender, parent)
        {
        }

        protected override void ExecuteSingle()
        {
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}