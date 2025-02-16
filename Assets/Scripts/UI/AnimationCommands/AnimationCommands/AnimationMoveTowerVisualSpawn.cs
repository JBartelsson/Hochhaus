using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationMoveTowerVisualSpawn : AnimationCommand
    {
        private Environment.GameStateType gameStateType;
        private UIController _uiController;

        public AnimationMoveTowerVisualSpawn(UIController ui, Environment.GameStateType gameStateType) : base()
        {
            _uiController = ui;
            this.gameStateType = gameStateType;
        }


        protected override void ExecuteCmd()
        {
            if (gameStateType == Environment.GameStateType.BUILD_ROOM)
            {
                _uiController.TowerVisual.MoveCurrentSpawn();
            }

        }
    }
}