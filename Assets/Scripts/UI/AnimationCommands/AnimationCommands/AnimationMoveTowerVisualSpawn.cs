using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationMoveTowerVisualSpawn : AnimationCommand
    {
        private UpdateGameStatCommand _updateGameStatCommand;
        private PlayerStats newStats;
        private float yOffsetSpawn;
        private float yOffsetDisplayText;

        public AnimationMoveTowerVisualSpawn(UIController ui, UpdateGameStatCommand updateGameStatCommand) : base(ui)
        {
            if (updateGameStatCommand.PlayerStat != PlayerStats.PlayerStat.SCORE_POINTS && updateGameStatCommand.PlayerStat != PlayerStats.PlayerStat.SCORE_MULT && updateGameStatCommand.PlayerStat != PlayerStats.PlayerStat.SCORE_xMULT) return;
            _updateGameStatCommand = updateGameStatCommand;
            // Debug.Log($"Visual Spawn Update Game Stat {_updateGameStatCommand}");
            newStats = _updateGameStatCommand.NewStats;
            float totalScore = newStats.Stats.TotalScore;
            float halfDownTotal = totalScore - newStats.RoomScore / 2f;
            yOffsetSpawn = totalScore * ui.VisualSettings.roomPxPerUnit;
            yOffsetDisplayText = halfDownTotal * ui.VisualSettings.roomPxPerUnit;
        }

        public AnimationMoveTowerVisualSpawn(UIController ui, CreateRoomCommand createRoomCommand) : base(ui)
        {
            float totalScore = createRoomCommand.PlacedRoom.LastStats.Stats.TotalScore + createRoomCommand.PlacedRoom._PlacedCard.CardCopy.AppartmentReference.BasePoints;
            float halfDownTotal = createRoomCommand.PlacedRoom.LastStats.Stats.TotalScore + createRoomCommand.PlacedRoom._PlacedCard.CardCopy.AppartmentReference.BasePoints * .5f;
            yOffsetSpawn = totalScore * ui.VisualSettings.roomPxPerUnit;
            yOffsetDisplayText  = halfDownTotal * ui.VisualSettings.roomPxPerUnit;
            // Debug.Log($"Calculated offset as {yOffsetSpawn} and {yOffsetDisplayText}");
            
        }


        protected override void ExecuteCmd()
        {
         if (yOffsetSpawn == 0) return;  
            ui.TowerVisual.CurrentSpawnPosition.localPosition = new Vector3(ui.TowerVisual.CurrentSpawnPosition.localPosition.x,
                yOffsetSpawn, 0);
            ui.TowerVisual.DisplayTextPosition.localPosition = new Vector3(
                ui.TowerVisual.DisplayTextPosition.localPosition.x, yOffsetDisplayText, 0);
                
        }
    }
}