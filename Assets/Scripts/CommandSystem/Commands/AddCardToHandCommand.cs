using Items;

namespace CommandSystem.Commands
{
    public class AddCardToHandCommand : CommandBase
    {
        private Card _card;

        public Card Card => _card;
        
        public int Index { get; set; }

        public AddCardToHandCommand(Environment env, Item sender, Card card, CommandBase parent = null) : base(env, sender, parent)
        {
            _card = card;
            
        }

        protected override void ExecuteSingle()
        {
            _env.CardSystem.Hand.Add(_card);
            Index = _env.CardSystem.Hand.Count - 1;
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