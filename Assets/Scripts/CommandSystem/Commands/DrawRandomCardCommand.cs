using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class DrawRandomCardCommand : CommandBase
    {
        public DrawRandomCardCommand(Environment env, Item sender) : base(env, sender)
        {
        }

        public override void Execute()
        {
            int randomIndex = Random.Range(0, _env.CardSystem.DrawPile.Count);
            Card card = _env.CardSystem.DrawPile[randomIndex];
            _env.CardSystem.DrawPile.RemoveAt(randomIndex);
            AddCardToHandCommand addCardToHandCommand = new AddCardToHandCommand(_env, sender, card);
            _env.CommandInvoker.ExecuteAndRecord(addCardToHandCommand);
            Debug.Log("Draw random card executed");
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}