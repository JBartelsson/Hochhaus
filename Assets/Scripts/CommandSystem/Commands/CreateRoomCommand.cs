using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class CreateRoomCommand : CommandBase
    {
        private Card _cardToPlace;
        
        private TowerRoom _placedRoom;

        public TowerRoom PlacedRoom => _placedRoom;

        public Card CardToPlace => _cardToPlace;

        public CreateRoomCommand(Environment env, Card cardToPlace, Item sender = null) : base(env, sender)
        {
            SetEnv(env);
            _cardToPlace = cardToPlace;
            _placedRoom = new TowerRoom(_cardToPlace, (Score)_env.PlayerStats.Score.Clone());
            _placedRoom._PlacedCard.CardCopy.Sender = sender;
            Debug.Log($"PLACED ROOM LAST SCORE: {PlacedRoom.LastScore}");
        }


        public override void Execute()
        {
            _env.TowerManager.TowerRooms.Add(_placedRoom);
         
            _env.GameUpdate(Environment.GameStateType.BUILD_ROOM);
            _env.EndRoomBuilding();

        }

        public override void Undo()
        {
        }
        
        //create tostring
        public override string ToString()
        {
            return $"CreateTowerCommand: {CardToPlace}";
        }
    }
}