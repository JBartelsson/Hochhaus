using System.Collections.Generic;
using System.Linq;
using CommandSystem.AnimationCommands;
using CommandSystem.Commands;
using UnityEngine;

namespace UI.AnimationCommands
{
    public class AnimationCommandInvoker : CommandInvoker, ISubscriber
    {
        private Queue<Animation> _animationQueue = new Queue<Animation>();
        private int lastGameCommandIndex = 0;

        private UIController ui;

        private bool isPlaying;

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

        public void QueueAnimation(AnimationCommand command, bool invisible = false)
        {
            _animationQueue.Enqueue(new Animation(command, invisible));
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
                _animationQueue.Dequeue();
                Debug.Log($"Command Invisble: {currentAnimation.Invisible} Type: {command.GetType()}");;

                command.SetCallback(NextCommand);
                command.Execute();

                int i = 0;
                Animation[] animations = _animationQueue.ToArray();
                Debug.Log($"Animation Q Invisible Search");
                if (animations.Length == 0)
                {
                    Debug.Log("Animation Q Invisible Search End");
                    return;
                }

                while (animations[i].Invisible)
                {
                    Debug.Log($"{i}: Invisble: {animations[i].Invisible} Type: {animations[i].Command.GetType()}");;
                    animations[i].Command.Execute();
                    _animationQueue.Dequeue();
                    i++;
                    if (i >= animations.Length)
                    {
                        break;
                    }
                }

                Debug.Log($"Animation Q Invisible Search End");
            }
        }

        public void StartPlaying()
        {
            if (!isPlaying) CheckQueue();
        }

        public void NextCommand()
        {
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
            isPlaying = true;
            EnvironmentManager.Instance.GetActiveEnvironment().SetBlockActions(true);
        }

        public void EndAnimationBlock()
        {
            isPlaying = false;
            EnvironmentManager.Instance.GetActiveEnvironment().SetBlockActions(false);
        }

        public void InitSubscriptions(UIController uiController)
        {
            ui = uiController;
            EnvironmentManager.Instance.GetActiveEnvironment().GameStateUpdate += OnGameStateUpdate;
        }

        private void OnGameStateUpdate(Environment arg1, Environment.GameStateType arg2)
        {
            for (int i = lastGameCommandIndex; i < arg1.CommandInvoker.ReplayCommands.Count; i++)
            {
                if (arg1.CommandInvoker.ReplayCommands[i].GetType() == typeof(CreateTowerCommand))
                {
                    CreateTowerCommand createTowerCommand = (CreateTowerCommand)arg1.CommandInvoker.ReplayCommands[i];
                    AnimationCreateTowerCommand animationCreateTowerCommand =
                        new AnimationCreateTowerCommand(ui, createTowerCommand);
                    QueueAnimation(animationCreateTowerCommand);
                }
                else if (arg1.CommandInvoker.ReplayCommands[i].GetType() == typeof(UpdateScoreCommand))
                {
                    UpdateScoreCommand updateScoreCommand = (UpdateScoreCommand)arg1.CommandInvoker.ReplayCommands[i];
                    AnimationUpdatePoints animationUpdateScore = new AnimationUpdatePoints(ui, updateScoreCommand);
                    QueueAnimation(animationUpdateScore, true);
                }

                // AnimationUpdatePoints animationUpdatePoints = new AnimationUpdatePoints(UIController, i, arg3, towerManager);
                // UIController.AnimationCommandInvoker.ExecuteAnimationCommand(animationUpdatePoints, true);
            Debug.Log($"Adding {arg1.CommandInvoker.ReplayCommands[i].GetType()} to animation queue");
            }

            StartPlaying();
            lastGameCommandIndex = arg1.CommandInvoker.ReplayCommands.Count;
        }


        public void ResetSubscriptions()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().GameStateUpdate -= OnGameStateUpdate;
        }
    }
}