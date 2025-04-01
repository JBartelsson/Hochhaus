using System;
using System.Linq;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CommandSystem.AnimationCommands
{
    public class AnimationUpdateScore : AnimationCommand
    {
        private UpdateGameStatCommand _updateGameStatCommand;

        public AnimationUpdateScore(UIController ui, UpdateGameStatCommand updateGameStatCommand) : base(ui)
        {
            _updateGameStatCommand = updateGameStatCommand;
        }


        protected override void ExecuteCmd()
        {
            switch (_updateGameStatCommand.PlayerStat)
            {
                case PlayerStats.PlayerStat.FABRIC:
                    UpdateFabric();
                    break;
                case PlayerStats.PlayerStat.SCORE_POINTS:
                    UpdateScore();
                    break;
                case PlayerStats.PlayerStat.SCORE_MULT:
                    UpdateScore();
                    break;
                case PlayerStats.PlayerStat.SCORE_xMULT:
                    UpdateScore();
                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    UpdateDrawCost();
                    break;
                case PlayerStats.PlayerStat.HAND_SIZE:
                    break;
                
                case PlayerStats.PlayerStat.SHOP_SIZE_ITEMS:
                case PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES:
                case PlayerStats.PlayerStat.REROLL_COST:
                    UpdateRerollCost();
                    break;
                case PlayerStats.PlayerStat.REROLL_COST_INCREASE:
                case PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE:

                case PlayerStats.PlayerStat.HANDS_LEFT:
                    UpdateHands();
                    break;
                case PlayerStats.PlayerStat.HANDS_TOTAL:
                default:
                    break;
            }
            
        }

        private void UpdateRerollCost()
        {
            ui.UIInventoryManager.RerollText.text = "Reroll (" + _updateGameStatCommand.NewValue.ToString() +")";
        }

        private void UpdateHands()
        {
            ui.DrawUI.DrawIncreaseText.text = _updateGameStatCommand.NewValue.ToString();
        }
        private void UpdateFabric()
        {
            ui.DrawUI.FabricText.text = _updateGameStatCommand.NewValue.ToString();
        }
        
        private void UpdateDrawCost()
        {
            ui.DrawUI.DrawCostText.text = _updateGameStatCommand.NewValue.ToString();
        }
        
        private void UpdateScore()
        {
            PlayerStats newStats = _updateGameStatCommand.NewStats;
            float oldScore = _updateGameStatCommand.LastStats.Stats.TotalScore;
            Tween scoreTween = DOTween.To(() => oldScore, x => oldScore = x, _updateGameStatCommand.NewStats.Stats.TotalScore, 0.1f).OnUpdate(() => ui.ScoreUI.ScoreText.text = Mathf.FloorToInt(oldScore).ToString(""));
            if (_updateGameStatCommand.Init) return;
            
            s.Append(scoreTween);
            RoomVisual roomVisual = ui.TowerVisual.AppartmentVisuals.Last();
           
            // if (newScore.RoomScore == roomVisual.TowerRoom._PlacedCard.CardCopy.AppartmentReference.Height) return;
            if (roomVisual != null)
            {
                
                s.Join(roomVisual.Pivot.transform.DOScaleY(newStats.RoomScore, 0.5f));
            }
            Debug.Log("Animation Update new Stats: " + newStats);

        }
    }
}