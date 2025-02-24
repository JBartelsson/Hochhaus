using CommandSystem.Commands;

namespace CommandSystem.AnimationCommands
{
    public class AnimationAddCardToHand : AnimationCommand
    {
        private AddCardToHandCommand _addCardToHandCommand;

        public AnimationAddCardToHand(UIController ui, AddCardToHandCommand addCardToHandCommand) : base(ui)
        {
            _addCardToHandCommand = addCardToHandCommand;
        }

        protected override void ExecuteCmd()
        {
            ui.UIInventoryManager.HandCardManager.AddHandItem(_addCardToHandCommand.Card);

        }
    }
}