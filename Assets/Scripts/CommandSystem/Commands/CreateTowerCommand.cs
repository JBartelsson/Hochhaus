namespace CommandSystem.Commands
{
    public class CreateTowerCommand : CommandBase
    {
        private Card _cardToPlace;
        
        public CreateTowerCommand(Environment env, Card cardToPlace)
        {
            SetEnv(env);
            _cardToPlace = cardToPlace;
        }


        public override void Execute()
        {
            _env.TowerManager.CreateTower(_cardToPlace);
           
        }

        public override void Undo()
        {
        }
    }
}