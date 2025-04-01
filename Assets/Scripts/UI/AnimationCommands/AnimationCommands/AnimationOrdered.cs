using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationOrdered : AnimationCommand
    {
        private List<AnimationCommand> _animationCommands = new ();
        int _callbackCount = 0;


        public AnimationOrdered(UIController ui) : base(ui)
        {
        }

        public override void SetCallback(Action callback)
        {
            base.SetCallback(() =>
            {
                if (_callbackCount >= _animationCommands.Count)
                {
                    callback?.Invoke();
                }
            });
        }
        
        public AnimationOrdered AddCommand(AnimationCommand command)
        {
            _animationCommands.Add(command);
            command.SetCallback(MoveToNextCommand);
            return this;
        }

        private void MoveToNextCommand()
        {
            _callbackCount++;
            if (_callbackCount >= _animationCommands.Count)
            {
                _callback?.Invoke();
                return;
            }
            _animationCommands[_callbackCount].Execute();
        }


        protected override void ExecuteCmd()
        {
            if(_animationCommands.Count == 0)
                return;
            
            _animationCommands.First().Execute();
        }
        
        public override string ToString()
        {
            string s = $"{this.GetType()}";
            if (_animationCommands.Count == 0)
            {
                return s;
            }

            s += ": ";
            int i = 0;
            foreach (var commandBase in _animationCommands)
            {
                i++;
                s += i + " " + commandBase.ToString() + ",";
            }

            s.Remove(s.Length - 1);
            return s;
        }
    }
}