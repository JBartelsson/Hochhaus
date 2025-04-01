using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class DrawRandomCardCommand : CommandBase
    {
        public DrawRandomCardCommand(Environment env, Item sender, CommandBase parent = null) : base(env, sender, parent)
        {
        }

        protected override void ExecuteSingle()
        {
            int randomIndex = Random.Range(0, _env.CardSystem.DrawPile.Count);
            Card card = _env.CardSystem.DrawPile[randomIndex];
            _env.CardSystem.DrawPile.RemoveAt(randomIndex);
            AddCardToHandCommand addCardToHandCommand = new AddCardToHandCommand(_env, sender, card);
            _env.CommandInvoker.Execute(addCardToHandCommand);
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}