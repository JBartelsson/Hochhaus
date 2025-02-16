using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationDelay : AnimationCommand
    {
        private float delay;
        private UIController _uiController;

        public AnimationDelay(UIController ui, float delay) : base()
        {
            this.delay = delay;
            _uiController = ui;
        }


        protected override void ExecuteCmd()
        {
            s.AppendInterval(delay);
        }
    }
}