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
            if (_animationQueue.Count > 0)
            {
                StartAnimationBlock();
                Animation currentAnimation = _animationQueue.Peek();
                AnimationCommand command = currentAnimation.Command;
                _animationQueue.Dequeue();
                ;

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
            Debug.Log("START OF Game State Update replay log");
            foreach (var commandInvokerReplayCommand in arg1.CommandInvoker.ReplayCommands)
            {
                Debug.Log(commandInvokerReplayCommand);
            }

            Debug.Log("END OF Game State Update replay log");

            for (int i = lastGameCommandIndex; i < arg1.CommandInvoker.ReplayCommands.Count; i++)
            {
                Debug.Log($"Adding {arg1.CommandInvoker.ReplayCommands[i].GetType()} to animation queue");
                ICommand command = arg1.CommandInvoker.ReplayCommands[i];
                CheckCreateRoomCommand(command);
                CheckUpdateGameStatCommand(command);
                CheckItemAddedCommand(command);
                CheckReorderItemsCommand(command);
                CheckAddCardToHandCommand(command);
                CheckRemoveCardFromHandCommand(command);

                AnimationDelay animationDelay2 = new AnimationDelay(ui, 0.2f);

                // QueueAnimation(animationDelay2);
            }


            StartPlaying();
            lastGameCommandIndex = arg1.CommandInvoker.ReplayCommands.Count;
        }

        private void CheckCreateRoomCommand(ICommand command)
        {
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
                QueueAnimation(animationMultiple);
                Debug.Log($"Info: {createRoomCommand.PlacedRoom}");
            }
        }

        private void CheckUpdateGameStatCommand(ICommand command)
        {
            if (command.GetType() == typeof(UpdateGameStatCommand))
            {
                UpdateGameStatCommand updateGameStatCommand = (UpdateGameStatCommand)command;
                AnimationUpdateScore animationUpdateScore = new AnimationUpdateScore(ui, updateGameStatCommand);
                AnimationEffectDisplay effectDisplay = new AnimationEffectDisplay(ui, updateGameStatCommand);
                AnimationMoveTowerVisualSpawn animationMoveTowerVisualSpawn =
                    new AnimationMoveTowerVisualSpawn(ui, updateGameStatCommand);

                AnimationMultiple animationMultiple = new AnimationMultiple(ui);
                animationMultiple
                    .AddCommand(animationUpdateScore)
                    .AddCommand(effectDisplay);
                QueueAnimation(animationMoveTowerVisualSpawn);
                QueueAnimation(animationMultiple);
                Debug.Log($"Info: {updateGameStatCommand.ScoreType} {updateGameStatCommand.Value}");
            }
        }

        private void CheckItemAddedCommand(ICommand command)
        {
            if (command.GetType() == typeof(AddItemCommand))
            {
                AddItemCommand addedItemCommand = (AddItemCommand)command;

                AnimationAddedItemCommand animationAddedItemCommand =
                    new AnimationAddedItemCommand(ui, addedItemCommand);
                QueueAnimation(animationAddedItemCommand);
            }
        }

        private void CheckAddCardToHandCommand(ICommand command)
        {
            if(command.GetType() == typeof(AddCardToHandCommand))
            {
                AddCardToHandCommand addCardToHandCommand = (AddCardToHandCommand)command;
                AnimationAddCardToHand animationAddCardToHandCommand = new AnimationAddCardToHand(ui, addCardToHandCommand);
                QueueAnimation(animationAddCardToHandCommand);
            }
            
        }
        
        private void CheckRemoveCardFromHandCommand(ICommand command)
        {
            if (command.GetType() == typeof(RemoveCardFromHandCommand))
            {
                RemoveCardFromHandCommand removeCardFromHandCommand = (RemoveCardFromHandCommand)command;
                AnimationRemoveCardFromHand animationRemoveCardFromHand = new AnimationRemoveCardFromHand(ui, removeCardFromHandCommand);
                QueueAnimation(animationRemoveCardFromHand);
            }
        }

        private void CheckReorderItemsCommand(ICommand command)
        {
            if (command.GetType() == typeof(ReorderItemsCommand))
            {
                ReorderItemsCommand reorderItemsCommand = (ReorderItemsCommand)command;

                UIReorderItemsCommand uiReorderItemsCommand =
                    new UIReorderItemsCommand(ui);
                QueueAnimation(uiReorderItemsCommand);
            }
        }


        public void ResetSubscriptions()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().GameStateUpdate -= OnGameStateUpdate;
        }
    }
}