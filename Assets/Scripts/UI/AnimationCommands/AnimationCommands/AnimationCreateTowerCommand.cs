using CommandSystem.Commands;
using DG.Tweening;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationCreateTowerCommand : AnimationCommand
    {
        TowerVisual _towerVisual;
        TowerRoom _room;
        UIController _uiController;
        
        public AnimationCreateTowerCommand(UIController ui, CreateTowerCommand createTowerCommand) : base()
        {
            _towerVisual = ui.TowerVisual;
            _room = createTowerCommand.PlacedRoom;
        }

        protected override void ExecuteCmd()
        {
            RoomVisual roomVisual = GameObject.Instantiate(_towerVisual.RoomVisualPrefab, _towerVisual.TowerVisualParent);
            roomVisual.Init(_room);
            _towerVisual.AppartmentVisuals.Add(roomVisual);
            roomVisual.transform.position = _towerVisual.CurrentSpawnPosition.position;

            s.Append(roomVisual.transform.DOShakePosition(0.3f, new Vector3(0, 0, 1f)))
                .AppendInterval(.2f);
            
        }
        
    }
}