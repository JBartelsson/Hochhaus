using Items;

namespace CommandSystem.Commands
{
    public class RemoveCardFromHandCommand : CommandBase
    {
        private Card _card;
        private int oldIndex;

        public int OldIndex => oldIndex;

        public Card Card => _card;
        public bool Destroy { get;  }

        public RemoveCardFromHandCommand(Environment env, Item sender, Card card, bool destroy = false , CommandBase parent = null) : base(env, sender, parent)
        {
            _card = card;
            Destroy = destroy;
        }

        protected override void ExecuteSingle()
        {
            oldIndex = _env.CardSystem.Hand.IndexOf(_card);
            _env.CardSystem.Hand.Remove(_card);
            if (!Destroy)
            {
                _env.CardSystem.DiscardPile.Add(_card);
            }
            _env.GameUpdate(Environment.GameStateType.CARD_REMOVED_FROM_HAND, new Context(_env)
            {
                LastDrawnCard = _card
            });
        }

        public override void Undo()
        {
        }
    }
}