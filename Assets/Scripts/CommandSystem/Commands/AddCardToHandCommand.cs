using Items;

namespace CommandSystem.Commands
{
    public class AddCardToHandCommand : CommandBase
    {
        private Card _card;

        public Card Card => _card;

        public AddCardToHandCommand(Environment env, Item sender, Card card) : base(env, sender)
        {
            _card = card;
        }

        public override void Execute()
        {
            _env.CardSystem.Hand.Add(_card);
            _env.GameUpdate(Environment.GameStateType.CARD_DRAWN, new Context(_env)
            {
                LastDrawnCard = _card
            });
        }

        public override void Undo()
        {
        }
    }
}