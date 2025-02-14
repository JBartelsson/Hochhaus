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
        
        public AnimationCreateTowerCommand(UIController ui, CreateTowerCommand createTowerCommand)
        {
            _towerVisual = ui.TowerVisual;
            _room = createTowerCommand.PlacedRoom;
        }

        public override void Execute()
        {
            AppartmentVisual appartmentVisual = GameObject.Instantiate(_towerVisual.AppartmentVisualPrefab, _towerVisual.TowerVisualParent);
            appartmentVisual.Init(_room);
            _towerVisual.AppartmentVisuals.Add(appartmentVisual);
            appartmentVisual.transform.position = _towerVisual.CurrentSpawnPosition.position;
            _towerVisual.MoveCurrentSpawn();
            Sequence s = DOTween.Sequence();
            
            s.Append(appartmentVisual.transform.DOShakePosition(0.3f, new Vector3(0,0, 1f)))
                .AppendInterval(.2f).AppendCallback(() => _callback.Invoke());
            s.Play();
            
        }
        
    }
}