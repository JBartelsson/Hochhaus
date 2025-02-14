using DG.Tweening;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationCreateTowerCommand : AnimationCommand
    {
        TowerVisual _towerVisual;
        TowerRoom _room;
        UIController _uiController;
        
        public AnimationCreateTowerCommand(TowerVisual towerVisual, TowerRoom room)
        {
            _towerVisual = towerVisual;
            _room = room;
            
        }
        
        public override void Execute()
        {
            AppartmentVisual appartmentVisual = GameObject.Instantiate(_towerVisual.AppartmentVisualPrefab, _towerVisual.TowerVisualParent);
            appartmentVisual.Init(_room);
            Debug.Log($"{_room._PlacedCard.CardCopy.AppartmentReference.AppartmentName} " + Time.time * 1000);
            _towerVisual.AppartmentVisuals.Add(appartmentVisual);
            appartmentVisual.transform.position = _towerVisual.CurrentSpawnPosition.position;
            _towerVisual.MoveCurrentSpawn();
            Sequence s = DOTween.Sequence();
            Debug.Log("Executed Animation Command");
            
            s.Append(appartmentVisual.transform.DOShakePosition(0.3f, new Vector3(0,0, 1f)))
                .AppendInterval(.2f).AppendCallback(() => _callback.Invoke());
            s.Play();
            
        }
        
    }
}