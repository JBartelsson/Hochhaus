using CommandSystem.Commands;
using DG.Tweening;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationCreateRoomCommand : AnimationCommand
    {
        TowerVisual _towerVisual;
        TowerRoom _room;
        private CreateRoomCommand _createRoomCommand;

        public AnimationCreateRoomCommand(UIController ui, CreateRoomCommand createRoomCommand) : base(ui)
        {
            _towerVisual = ui.TowerVisual;
            _room = createRoomCommand.PlacedRoom;
            _createRoomCommand = createRoomCommand;
        }

        protected override void ExecuteCmd()
        {
            RoomVisual roomVisual =
                GameObject.Instantiate(_towerVisual.RoomVisualPrefab, _towerVisual.TowerVisualParent);
            roomVisual.InitSubscriptions(ui);
            roomVisual.Init(_room);
            _towerVisual.AppartmentVisuals.Add(roomVisual);
            roomVisual.transform.position = _towerVisual.CurrentSpawnPosition.position;

            float scaleAmount = 1.2f;
            float duration = .25f;
            if (_createRoomCommand.Sender == null)
            {
                DraggableItem item = ui.UIInventoryManager.HandCardManager.GetItemByIndex(0);
                Vector3 oldItemScale = item.transform.localScale;
                s.Append(item.transform.DOScale(oldItemScale * scaleAmount, duration))
                    .Append(item.transform.DOScale(oldItemScale, duration))
                    ;
            }

            Vector3 oldScale = roomVisual.Sprite.transform.localScale;

            Vector3 newScale = oldScale * scaleAmount;
            s
                .Append(roomVisual.Sprite.transform.DOScale(newScale, duration))
                .Append(roomVisual.Sprite.transform.DOScale(oldScale, duration))
                ;
        }
    }
}