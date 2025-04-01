using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.AnimationCommands;
using CommandSystem.Commands;
using UnityEngine;

namespace UI.AnimationCommands
{
    [Serializable]
    public class AnimationCommandInvoker : CommandInvoker, ISubscriber
    {
        private Queue<Animation> _animationQueue = new Queue<Animation>();
        private int lastGameCommandIndex = 0;

        private UIController ui;

        private bool isPlaying;

        [Serializable]
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
            if (command == null)
            {
                return;
            }

            // Debug.Log($"Adding {command} to animation queue");

            _animationQueue.Enqueue(new Animation(command, invisible));
        }
        //Write debug.log in checkqueue so it can be seen in the console

        public void CheckQueue()
        {
            // Debug.Log($"Checking Animation Queue with {_animationQueue.Count} elements");
            if (_animationQueue.Count > 0)
            {
                StartAnimationBlock();
                Animation currentAnimation = _animationQueue.Peek();
                AnimationCommand command = currentAnimation.Command;
                // Debug.Log("Next command is " + command);
                _animationQueue.Dequeue();


                command.SetCallback(NextCommand);
                command.Execute();

                int i = 0;
                Animation[] animations = _animationQueue.ToArray();
                if (animations.Length == 0)
                {
                    return;
                }

                while (animations[i].Invisible)
                {
                    animations[i].Command.Execute();
                    _animationQueue.Dequeue();
                    i++;
                    if (i >= animations.Length)
                    {
                        break;
                    }
                }
            }
        }

        public void StartPlaying()
        {
            if (!isPlaying)
            {
                CheckQueue();
            }
        }

        public void NextCommand()
        {
            // Debug.Log("Next Command Callback Invoked");
            if (_animationQueue.Count == 0)
            {
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
            // Debug.Log("Ending animation blok");
            isPlaying = false;
            EnvironmentManager.Instance.GetActiveEnvironment().SetBlockActions(false);
        }

        public void InitSubscriptions(UIController uiController)
        {
            ui = uiController;
            EnvironmentManager.Instance.GetActiveEnvironment().CommandInvoker.CommandUpdate += OnGameStateUpdate;
        }

        private void OnGameStateUpdate(Environment arg1)
        {
            // Debug.Log($"START OF Game State Update replay log Current Index: {lastGameCommandIndex}");
            for (int i = 0; i < arg1.CommandInvoker.ReplayCommands.Count; i++)
            {
                // Debug.Log(i + " " + arg1.CommandInvoker.ReplayCommands[i]);
            }

            // Debug.Log("END OF Game State Update replay log");

            for (int i = lastGameCommandIndex; i < arg1.CommandInvoker.ReplayCommands.Count; i++)
            {
                CommandBase command = arg1.CommandInvoker.ReplayCommands[i];
                AnimationCommand animation = CheckCommand(command);
                QueueAnimation(animation);
                AnimationDelay animationDelay2 = new AnimationDelay(ui, 0.2f);

                // QueueAnimation(animationDelay2);
            }


            StartPlaying();
            lastGameCommandIndex = arg1.CommandInvoker.ReplayCommands.Count;
        }

        private AnimationCommand CheckCommand(CommandBase command)
        {
            AnimationCommand animationCommand = null;
            if (CheckCompositeCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckCreateRoomCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckUpdateGameStatCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckItemAddedCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckAddCardToHandCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckRemoveCardFromHandCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            if (CheckReorderItemsCommand(command, out animationCommand))
            {
                return animationCommand;
            }

            return null;
        }

        private bool CheckCompositeCommand(CommandBase command, out AnimationCommand animationCommand)
        {
            animationCommand = null;
            if (command.Children.Count == 0)
            {
                return false;
            }

            AnimationMultiple animationMultiple = new AnimationMultiple(ui);
            foreach (var child in command.Children)
            {
                AnimationCommand newCommand = CheckCommand(child);
                if (newCommand == null) continue;

                animationMultiple.AddCommand(
                    newCommand);
            }

            animationCommand = animationMultiple;
            return true;
        }

        private bool CheckCreateRoomCommand(ICommand command, out AnimationCommand animationCommand)
        {
            animationCommand = null;
            if (command.GetType() == typeof(CreateRoomCommand))
            {
                CreateRoomCommand createRoomCommand = (CreateRoomCommand)command;
                AnimationCreateRoomCommand animationCreateRoomCommand =
                    new AnimationCreateRoomCommand(ui, createRoomCommand);
                AnimationEffectDisplay effectDisplayCommand = new AnimationEffectDisplay(ui, createRoomCommand);
                AnimationMoveTowerVisualSpawn animationMoveTowerVisualSpawn =
                    new AnimationMoveTowerVisualSpawn(ui, createRoomCommand);
                AnimationMultiple animationMultiple = new AnimationMultiple(ui);
                // QueueAnimation(animationMoveTowerVisualSpawn);
                animationMultiple
                    .AddCommand(animationCreateRoomCommand)
                    .AddCommand(animationMoveTowerVisualSpawn)
                    .AddCommand(effectDisplayCommand);

                animationCommand = animationMultiple;
                return true;
            }

            return false;
        }

        private bool CheckUpdateGameStatCommand(ICommand command, out AnimationCommand animationCommand)
        {
            animationCommand = null;
            if (command.GetType() == typeof(UpdateGameStatCommand))
            {
                UpdateGameStatCommand updateGameStatCommand = (UpdateGameStatCommand)command;
                AnimationUpdateScore animationUpdateScore = new AnimationUpdateScore(ui, updateGameStatCommand);
                AnimationOrdered animationOrdered = new AnimationOrdered(ui);
                if (!updateGameStatCommand.Init)
                {
                    AnimationEffectDisplay effectDisplay = new AnimationEffectDisplay(ui, updateGameStatCommand);
                    AnimationMoveTowerVisualSpawn animationMoveTowerVisualSpawn =
                        new AnimationMoveTowerVisualSpawn(ui, updateGameStatCommand);

                    AnimationMultiple animationMultiple = new AnimationMultiple(ui);
                    animationMultiple
                        .AddCommand(animationUpdateScore)
                        .AddCommand(effectDisplay);
                    animationOrdered.AddCommand(animationMoveTowerVisualSpawn)
                        .AddCommand(animationMultiple);
                }
                else
                {
                    animationOrdered.AddCommand(animationUpdateScore);
                }

                animationCommand = animationOrdered;
                return true;
            }

            return false;
        }


        private bool CheckItemAddedCommand(CommandBase command, out AnimationCommand animationCommand)
        {
            animationCommand = null;
            if (command.GetType() == typeof(ItemCommand))
            {
                ItemCommand addedItemCommand = (ItemCommand)command;

                AnimationItemCommand animationItemCommand =
                    new AnimationItemCommand(ui, addedItemCommand);
                animationCommand = animationItemCommand;
                return true;
            }

            return false;
        }

        private bool CheckAddCardToHandCommand(ICommand command, out AnimationCommand animationCommand)
        {
            animationCommand = null;
            if (command.GetType() == typeof(AddCardToHandCommand))
            {
                AddCardToHandCommand addCardToHandCommand = (AddCardToHandCommand)command;
                AnimationAddCardToHand animationAddCardToHandCommand =
                    new AnimationAddCardToHand(ui, addCardToHandCommand);
                animationCommand = animationAddCardToHandCommand;
                return true;
            }

            return false;
        }

        private bool CheckRemoveCardFromHandCommand(ICommand command, out AnimationCommand animationCommand)
        {
            animationCommand = null;

            if (command.GetType() == typeof(RemoveCardFromHandCommand))
            {
                RemoveCardFromHandCommand removeCardFromHandCommand = (RemoveCardFromHandCommand)command;
                AnimationRemoveCardFromHand animationRemoveCardFromHand =
                    new AnimationRemoveCardFromHand(ui, removeCardFromHandCommand);
                animationCommand = animationRemoveCardFromHand;
                return true;
            }

            return false;
        }

        private bool CheckReorderItemsCommand(ICommand command, out AnimationCommand animationCommand)
        {
            animationCommand = null;

            if (command.GetType() == typeof(ReorderItemsCommand))
            {
                ReorderItemsCommand reorderItemsCommand = (ReorderItemsCommand)command;

                UIReorderItemsCommand uiReorderItemsCommand =
                    new UIReorderItemsCommand(ui);
                animationCommand = uiReorderItemsCommand;
                return true;
            }

            return false;
        }


        public void ResetSubscriptions()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().CommandInvoker.CommandUpdate -= OnGameStateUpdate;
        }
    }
}