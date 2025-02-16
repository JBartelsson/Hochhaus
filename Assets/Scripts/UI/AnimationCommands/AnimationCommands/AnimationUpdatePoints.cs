using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationUpdatePoints : AnimationCommand
    {
        private UpdateScoreCommand _updateScoreCommand;
        private UIController _uiController;

        public AnimationUpdatePoints(UIController ui, UpdateScoreCommand updateScoreCommand) : base()
        {
            _uiController = ui;
            _updateScoreCommand = updateScoreCommand;
        }


        protected override void ExecuteCmd()
        {
            Score newScore = _updateScoreCommand.NewScore;
            _uiController.ScoreUI.ScoreText.text = _updateScoreCommand.NewScore.TotalScore.ToString();
            RoomVisual roomVisual = _uiController.TowerVisual.AppartmentVisuals.Last();
            Debug.Log($"Sequence Active: {s.IsActive()}");
            if (newScore.RoomScore == roomVisual.TowerRoom._PlacedCard.CardCopy.AppartmentReference.Height) return;
            if (roomVisual != null)
            {
                s.Append(roomVisual.transform.DOScaleY(newScore.RoomScore, 0.5f)).AppendCallback(()=> Debug.Log("Changed Scale"));
            }

            Debug.Log($"Update Score: {newScore}");
        }
    }
}