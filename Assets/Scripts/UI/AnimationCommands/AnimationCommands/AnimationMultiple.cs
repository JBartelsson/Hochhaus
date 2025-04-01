using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationMultiple : AnimationCommand
    {
        private List<AnimationCommand> _animationCommands = new ();
        int _callbackCount = 0;


        public AnimationMultiple(UIController ui) : base(ui)
        {
        }

        public override void SetCallback(Action callback)
        {
            base.SetCallback(() =>
            {
                if (_callbackCount == _animationCommands.Count)
                    callback?.Invoke();
            });
        }
        
        public AnimationMultiple AddCommand(AnimationCommand command)
        {
            if (command == null)
            {
                Debug.LogWarning("Command is null");
                return this;
            }
            _animationCommands.Add(command);
            command.SetCallback(CountCallbacks);
            return this;
        }

        private void CountCallbacks()
        {
            _callbackCount++;
            _callback?.Invoke();
        }


        protected override void ExecuteCmd()
        {
            
            foreach (var animationCommand in _animationCommands)
            {
                animationCommand.Execute();
            }
        }

        public override string ToString()
        {
            string s = $"{this.GetType()}";
            if (_animationCommands.Count == 0)
            {
                return s;
            }

            s += ": ";
            foreach (var commandBase in _animationCommands)
            {
                s += commandBase.ToString() + ",";
            }

            s.Remove(s.Length - 1);
            return s;
        }
    }
}