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
            string valueString = "";
            _effectDisplay.Text.text = valueString;
            if (_command.GetType() == typeof(UpdateGameStatCommand))
            {
                UpdateGameStatCommand updateGameStatCommand = (UpdateGameStatCommand)_command;
                valueString = GetInformation(updateGameStatCommand);
                _effectDisplay.Text.text = valueString;
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
            else
            {
                return;
            }


            _effectDisplay.CanvasGroup.alpha = 0f;
            s.Append(_effectDisplay.CanvasGroup.DOFade(1f, 0.4f));
                
            int itemIndex = EnvironmentManager.Instance.GetActiveEnvironment().Inventory.Items.IndexOf(_command.Sender);
            if (itemIndex != -1 && !_command.Sender.ItemData.IsBasic)
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
                case PlayerStats.PlayerStat.SCORE_POINTS:
                    return "+" + + updateGameStatCommand.Value;

                    break;
                case PlayerStats.PlayerStat.FABRIC:
                    return "";

                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    return "Draws +" + + updateGameStatCommand.Value;
                    break;
                case PlayerStats.PlayerStat.SCORE_MULT:
                    return "x" +  + updateGameStatCommand.Value + "\nx" + updateGameStatCommand.LastStats.xMult;

                case PlayerStats.PlayerStat.SCORE_xMULT:
                    return "+x" + updateGameStatCommand.Value;

                case PlayerStats.PlayerStat.HAND_SIZE:
                case PlayerStats.PlayerStat.SHOP_SIZE_ITEMS:
                case PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES:
                case PlayerStats.PlayerStat.REROLL_COST:
                case PlayerStats.PlayerStat.REROLL_COST_INCREASE:
                default:
                    break;
            }

            return "+";
        }
    }
}