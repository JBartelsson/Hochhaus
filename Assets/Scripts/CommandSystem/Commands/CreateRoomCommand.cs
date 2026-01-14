using System.Collections.Generic;
using Items;
using Unity.VisualScripting;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class CreateRoomCommand : CommandBase
    {
        private Card _cardToPlace;

        private TowerRoom _placedRoom;

        public TowerRoom PlacedRoom => _placedRoom;

        public Card CardToPlace => _cardToPlace;

        public CreateRoomCommand(Environment env, Card cardToPlace, Item sender = null, CommandBase parent = null) :
            base(env, sender, parent)
        {
            SetEnv(env);
            _cardToPlace = cardToPlace;
            _placedRoom = new TowerRoom(_cardToPlace, (PlayerStats)_env.PlayerStats.Clone());
            _placedRoom._PlacedCard.CardCopy.Sender = sender;
            // Debug.Log($"PLACED ROOM LAST SCORE: {PlacedRoom.LastStats}");
        }


        protected override void ExecuteSingle()
        {
            _env.TowerManager.TowerRooms.Add(_placedRoom);
            _env.GameUpdate(Environment.GameStateType.BUILD_ROOM, new Context(_env)
            {
                TriggerOnlyBasic = true
            });

            _env.Tokens.GameUpdate(Environment.GameStateType.TOKEN_TRIGGER, new Context(_env)
            {
            });
            foreach (var tokensItem in new List<Item>(_env.Tokens.Items))
            {
                ItemCommand removeTokenCommand = new ItemCommand(_env, null, tokensItem, ItemLocations.TOKENS, ItemCommand.Mode.REMOVE);
                _env.CommandInvoker.Execute(removeTokenCommand);
            }
                
                
            foreach (var item in _cardToPlace.Mods)
            {
                item.GameUpdate(Environment.GameStateType.MODIFICATION_TRIGGER, new Context(_env)
                {
                    CurrentItem = item
                });
            }

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