using System;
using CommandSystem.Commands;
using DG.Tweening;
using Items;
using UI;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationEffectDisplay : AnimationCommand
    {
        private CommandBase _command;
        private EffectDisplay _effectDisplay;

        public AnimationEffectDisplay(UIController ui, CommandBase command) : base(ui)
        {
            _command = command;
            this.ui = ui;
            _effectDisplay = ui.EffectDisplay;
        }

        protected override void ExecuteCmd()
        {
            string prefix = "";
            Debug.Log("AnimationEffectDisplay Executing");
            _effectDisplay.Text.text = prefix;
            if (_command.GetType() == typeof(UpdateGameStatCommand))
            {
                UpdateGameStatCommand updateGameStatCommand = (UpdateGameStatCommand)_command;
                prefix = GetInformation(updateGameStatCommand);
                _effectDisplay.Text.text = prefix + updateGameStatCommand.Value;
            }
            _effectDisplay.ImageContainer.SetActive(false);

            if (_command.Sender != null)
            {
                if (_command.Sender.ItemData.ItemType != ItemType.BasicPoints)
                {
                    _effectDisplay.ImageContainer.SetActive(true);
                }

                _effectDisplay.Image.sprite = _command.Sender.ItemData.Image;
            }


            _effectDisplay.CanvasGroup.alpha = 0f;
            s.Append(_effectDisplay.CanvasGroup.DOFade(1f, 0.4f));
                
            int itemIndex = EnvironmentManager.Instance.GetActiveEnvironment().Inventory.Items.IndexOf(_command.Sender);
            if (itemIndex != -1)
            {
                Transform item = ui.UIInventoryManager.ItemDraggableManager.GetItemByIndex(itemIndex).transform;
                s.JoinCallback(()=> DoItemScale(item));
            }
            s.AppendInterval(0.2f)
                .Append(_effectDisplay.CanvasGroup.DOFade(0f, 0.4f));
        }

        private void DoItemScale(Transform item)
        {
            Sequence s2 = DOTween.Sequence();
            s2.Join(item.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f))
                .AppendInterval(0.2f)
                .Join(item.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
        }

        private string GetInformation(UpdateGameStatCommand updateGameStatCommand)
        {
            switch (updateGameStatCommand.PlayerStat)
            {
                case PlayerStats.PlayerStat.SCORE:
                    switch (updateGameStatCommand.ScoreType)
                    {
                        case Score.ScoreType.xMULT:
                            return "x";
                            break;
                        case Score.ScoreType.POINTS:
                            return "+";
                            break;
                        case Score.ScoreType.DRAWS:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case PlayerStats.PlayerStat.FABRIC:
                    Debug.Log("MONEYY!!!");
                    return "";

                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    return "Draws +";
                    break;
                default:
                    break;
            }

            return "+";
        }
    }
}