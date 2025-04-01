using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class PutHandBackToDeckCommand : CommandBase
    {
        public PutHandBackToDeckCommand(Environment env, Item sender, CommandBase parent = null) : base(env, sender, parent)
        {
            
        }

        protected override void ExecuteSingle()
        {
            Debug.Log($"Put {_env.CardSystem.Hand.ToFormattedString()} back to Deck");
            for (var i = _env.CardSystem.Hand.Count - 1; i >= 0; i--)
            {
                RemoveCardFromHandCommand removeCardFromHandCommand = new RemoveCardFromHandCommand(_env, sender, _env.CardSystem.Hand[i]);
                _env.CommandInvoker.Execute(removeCardFromHandCommand);
            }
            _env.CardSystem.ReturnDiscardPile();
            Debug.Log($"Put {_env.CardSystem.Hand.ToFormattedString()} back to Deck");
        }

        public override void Undo()
        {
        }
    }
}