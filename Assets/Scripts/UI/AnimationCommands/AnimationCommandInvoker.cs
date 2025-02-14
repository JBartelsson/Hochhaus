using System.Collections.Generic;
using UnityEngine;

namespace UI.AnimationCommands
{
    public class AnimationCommandInvoker : CommandInvoker
    {
        private Queue<Animation> _animationQueue = new Queue<Animation>();

        public class Animation
        {
            public AnimationCommand Command { get; set; }
            public bool Invisible { get; set; }

            public Animation(AnimationCommand command, bool invisible)
            {
                Command = command;
                Invisible = invisible;
            }
        }


        public AnimationCommandInvoker() : base(null)
        {
            
        }

        public void ExecuteAnimationCommand(AnimationCommand command, bool invisible = false)
        {
            Debug.Log($"{_animationQueue.Count}");
            _animationQueue.Enqueue(new Animation(command, invisible));
            if (_animationQueue.Count == 1)
                CheckQueue();
        }
        //Write debug.log in checkqueue so it can be seen in the console

        public void CheckQueue()
        {
            Debug.Log("Checking animation queue");
            if (_animationQueue.Count > 0)
            {
                Debug.Log("Starting animation");
                StartAnimationBlock();
                Animation currentAnimation = _animationQueue.Peek();
                AnimationCommand command = currentAnimation.Command;
                if (currentAnimation.Invisible)
                {
                    command.Execute();
                    NextCommand();
                }
                else
                {
                    command.SetCallback(NextCommand);
                    command.Execute();
                }
                
            }
        }

        public void NextCommand()
        {
            _animationQueue.Dequeue();
            if (_animationQueue.Count == 0)
            {
                Debug.Log("Animation Q over");
                EndAnimationBlock();
                return;
            }

            CheckQueue();
        }

        public void StartAnimationBlock()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().SetBlockActions(true);
        }

        public void EndAnimationBlock()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().SetBlockActions(false);
        }
    }
}