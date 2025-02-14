namespace CommandSystem.Commands
{
    public class CreateTowerCommand : CommandBase
    {
        private Card _cardToPlace;
        
        private TowerRoom _placedRoom;

        public TowerRoom PlacedRoom => _placedRoom;

        public Card CardToPlace => _cardToPlace;

        public CreateTowerCommand(Environment env, Card cardToPlace)
        {
            SetEnv(env);
            _cardToPlace = cardToPlace;
        }


        public override void Execute()
        {
            _placedRoom = new TowerRoom(_cardToPlace, (Score)_env.PlayerStats.Score.Clone());
            _env.TowerManager.TowerRooms.Add(_placedRoom);
            UpdateScoreCommand updateScore = new UpdateScoreCommand(_env, Score.ScoreType.POINTS, _placedRoom.Score.Points);
            _env.CommandInvoker.ExecuteAndRecord(updateScore);
            _env.GameUpdate(Environment.GameStateType.BUILD_ROOM);
        }

        public override void Undo()
        {
        }
    }
}