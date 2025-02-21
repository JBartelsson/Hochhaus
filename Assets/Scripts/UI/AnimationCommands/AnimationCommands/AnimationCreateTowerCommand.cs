using CommandSystem.Commands;
using DG.Tweening;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationCreateTowerCommand : AnimationCommand
    {
        TowerVisual _towerVisual;
        TowerRoom _room;
        
        public AnimationCreateTowerCommand(UIController ui, CreateRoomCommand createRoomCommand) : base(ui)
        {
            _towerVisual = ui.TowerVisual;
            _room = createRoomCommand.PlacedRoom;
        }

        protected override void ExecuteCmd()
        {
            RoomVisual roomVisual = GameObject.Instantiate(_towerVisual.RoomVisualPrefab, _towerVisual.TowerVisualParent);
            roomVisual.InitSubscriptions(ui);
            roomVisual.Init(_room);
            _towerVisual.AppartmentVisuals.Add(roomVisual);
            roomVisual.transform.position = _towerVisual.CurrentSpawnPosition.position;

            // s.Append(roomVisual.transform.DOShakePosition(0.3f, new Vector3(0, 0, 1f)));

        }
        
    }
}