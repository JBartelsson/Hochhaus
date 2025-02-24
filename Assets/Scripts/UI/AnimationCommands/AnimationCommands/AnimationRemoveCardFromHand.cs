using System;
using CommandSystem.Commands;

namespace CommandSystem.AnimationCommands
{
    public class AnimationRemoveCardFromHand : AnimationCommand
    {
        private RemoveCardFromHandCommand _removeCardFromHandCommand;

        public AnimationRemoveCardFromHand(UIController ui, RemoveCardFromHandCommand removeCardFromHandCommand) : base(ui)
        {
            _removeCardFromHandCommand = removeCardFromHandCommand;
        }

        protected override void ExecuteCmd()
        {
            ui.UIInventoryManager.HandCardManager.RemoveItem(_removeCardFromHandCommand.OldIndex);

        }
    }
}