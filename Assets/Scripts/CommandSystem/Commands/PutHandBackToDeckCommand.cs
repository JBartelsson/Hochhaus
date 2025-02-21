using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class PutHandBackToDeckCommand : CommandBase
    {
        public PutHandBackToDeckCommand(Environment env, Item sender) : base(env, sender)
        {
            
        }

        public override void Execute()
        {
            _env.CardSystem.DrawPile.AddRange(_env.CardSystem.Hand);
            Debug.Log($"Put {_env.CardSystem.Hand.ToFormattedString()} back to Deck");
        }

        public override void Undo()
        {
        }
    }
}