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

        public AnimationDelay(UIController ui, float delay) : base(ui)
        {
            this.delay = delay;
        }


        protected override void ExecuteCmd()
        {
            s.AppendInterval(delay);
        }
    }
}