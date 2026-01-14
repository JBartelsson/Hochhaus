using System;
using CommandSystem.Commands;
using DG.Tweening;
using Items;
using TMPro;
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
            SetValues(_command as UpdateGameStatCommand);


            _effectDisplay.CanvasGroup.alpha = 1f;
            // s.Append(_effectDisplay.CanvasGroup.DOFade(1f, 0.4f));
                
            int itemIndex = EnvironmentManager.Instance.GetActiveEnvironment().Inventory.Items.IndexOf(_command.Sender);
            if (itemIndex != -1 && !_command.Sender.ItemData.IsBasic)
            {
                Transform item = ui.UIInventoryManager.ItemDraggableManager.GetItemByIndex(itemIndex).transform;
                s.JoinCallback(()=> DoItemScale(item));
            }
            s.AppendInterval(0.2f)
                // .Append(_effectDisplay.CanvasGroup.DOFade(0f, 0.4f))
                ;
        }

        private void DoItemScale(Transform item)
        {
            Sequence s2 = DOTween.Sequence();
            s2.Join(item.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f))
                .AppendInterval(0.2f)
                .Join(item.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
        }

        private void SetValues(UpdateGameStatCommand updateGameStatCommand)
        {
            switch (updateGameStatCommand.PlayerStat)
            {
                case PlayerStats.PlayerStat.FABRIC:
                    UpdateText(_effectDisplay.FabricText, updateGameStatCommand.NewStats.Stats.Fabric);
                    break;
                case PlayerStats.PlayerStat.SCORE_POINTS:
                    UpdateText(_effectDisplay.PointsText, updateGameStatCommand.NewStats.Stats.ScorePoints);

                    break;
                case PlayerStats.PlayerStat.SCORE_MULT:
                    UpdateText(_effectDisplay.MultText, updateGameStatCommand.NewStats.RoomMult);
                    break;
                case PlayerStats.PlayerStat.SCORE_xMULT:
                    UpdateText(_effectDisplay.MultText, updateGameStatCommand.NewStats.RoomMult);
                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    break;
                case PlayerStats.PlayerStat.HAND_SIZE:
                    break;
                case PlayerStats.PlayerStat.SHOP_SIZE_ITEMS:
                    break;
                case PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES:
                    break;
                case PlayerStats.PlayerStat.REROLL_COST:
                    break;
                case PlayerStats.PlayerStat.REROLL_COST_INCREASE:
                    break;
                case PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE:
                    break;
                case PlayerStats.PlayerStat.HANDS_LEFT:
                    break;
                case PlayerStats.PlayerStat.HANDS_TOTAL:
                    break;
                case PlayerStats.PlayerStat.FABRIC_MULT:
                    break;
                case PlayerStats.PlayerStat.CHANCE_DATA:
                    break;
                case PlayerStats.PlayerStat.INT_DATA:
                    break;
                case PlayerStats.PlayerStat.FLOAT_DATA:
                    break;
                case PlayerStats.PlayerStat.SCORE_TOTAL:
                    break;
                case PlayerStats.PlayerStat.FABRIC_TOTAL:
                    break;
                case PlayerStats.PlayerStat.FABRIC_xMULT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void UpdateText(TextMeshProUGUI text, float value)
        {
            text.text = value.ToString();
        }
    }
}