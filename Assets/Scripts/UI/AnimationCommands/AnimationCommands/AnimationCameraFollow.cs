using DG.Tweening;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationCameraFollow : AnimationCommand
    {
        TowerVisual _towerVisual;
        TowerRoom _room;
        
        public AnimationCameraFollow(TowerVisual towerVisual, TowerRoom room)
        {
            _towerVisual = towerVisual;
            _room = room;
        }
        
        public override void Execute()
        {
            if (_towerVisual.CurrentSpawnPosition.position.y <= 540)
            {
                _callback?.Invoke();
                return;
            }
            
            Camera.main.transform.DOMoveY(_towerVisual.CurrentSpawnPosition.position.y, 1.2f)
                .SetEase(Ease.InOutSine).OnComplete(() => _callback?.Invoke());
            
        }
        
    }
}