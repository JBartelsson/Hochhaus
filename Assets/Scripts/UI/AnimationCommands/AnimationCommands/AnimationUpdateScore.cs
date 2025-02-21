using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationUpdateScore : AnimationCommand
    {
        private UpdateGameStatCommand _updateGameStatCommand;

        public AnimationUpdateScore(UIController ui, UpdateGameStatCommand updateGameStatCommand) : base(ui)
        {
            _updateGameStatCommand = updateGameStatCommand;
        }


        protected override void ExecuteCmd()
        {
            if (_updateGameStatCommand.PlayerStat != PlayerStats.PlayerStat.SCORE) return;
            
            Score newScore = _updateGameStatCommand.NewScore;
            ui.ScoreUI.ScoreText.text = _updateGameStatCommand.NewScore.TotalScore.ToString();
            RoomVisual roomVisual = ui.TowerVisual.AppartmentVisuals.Last();
            Debug.Log($"Sequence Active: {s.IsActive()}");
           
            // if (newScore.RoomScore == roomVisual.TowerRoom._PlacedCard.CardCopy.AppartmentReference.Height) return;
            if (roomVisual != null)
            {
                
                s.Append(roomVisual.transform.DOScaleY(newScore.RoomScore, 0.5f));
            }

            Debug.Log($"Update Score: {newScore}");
        }
    }
}