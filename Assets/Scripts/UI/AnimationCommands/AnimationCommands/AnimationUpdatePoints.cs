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

        public AnimationUpdatePoints(UIController ui, UpdateScoreCommand updateScoreCommand)
        {
            _uiController = ui;
            _updateScoreCommand = updateScoreCommand;
        }


        public override void Execute()
        {
            _uiController.ScoreUI.ScoreText.text = _updateScoreCommand.NewScore.TotalScore.ToString();
            base.Execute();
        }
        
    }
}