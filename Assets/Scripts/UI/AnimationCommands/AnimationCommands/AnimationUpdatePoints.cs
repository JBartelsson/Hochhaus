using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationUpdatePoints : AnimationCommand
    {
        private int towerRoomIndex;
        private TowerRoom _towerRoom;
        private TowerManager _towerManager;
        private UIController _uiController;

        public AnimationUpdatePoints(UIController ui, int towerRoomIndex, TowerRoom towerRoom, TowerManager towerManager)
        {
            this.towerRoomIndex = towerRoomIndex;
            _towerRoom = towerRoom;
            _towerManager = towerManager;
            _uiController = ui;
        }


        public override void Execute()
        {
            float totalScore = 0;
            Debug.Log("NEW ROOM");
            for (int i = 0; i <= towerRoomIndex; i++)
            {
                totalScore += _towerManager.TowerAppartments[i].Score.StoryScore;
                Debug.Log($"ADDING {_towerManager.TowerAppartments[i].Score.StoryScore}");
            }
            Debug.Log($"TOTAL = {totalScore}");

            _uiController.ScoreUI.ScoreText.text = totalScore.ToString();
            base.Execute();
        }
        
    }
}