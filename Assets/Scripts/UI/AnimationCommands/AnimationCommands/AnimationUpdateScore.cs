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
                case PlayerStats.PlayerStat.SCORE:
                    UpdateScore();
                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    UpdateDrawCost();
                    break;
                case PlayerStats.PlayerStat.HAND_SIZE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
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
            Score newScore = _updateGameStatCommand.NewScore;
            float oldScore = _updateGameStatCommand.LastScore.TotalScore;
            Tween scoreTween = DOTween.To(() => oldScore, x => oldScore = x, _updateGameStatCommand.NewScore.TotalScore, 0.1f).OnUpdate(() => ui.ScoreUI.ScoreText.text = Mathf.FloorToInt(oldScore).ToString(""));
            s.Append(scoreTween);
            RoomVisual roomVisual = ui.TowerVisual.AppartmentVisuals.Last();
            Debug.Log($"Sequence Active: {s.IsActive()}");
           
            // if (newScore.RoomScore == roomVisual.TowerRoom._PlacedCard.CardCopy.AppartmentReference.Height) return;
            if (roomVisual != null)
            {
                
                s.Join(roomVisual.Pivot.transform.DOScaleY(newScore.RoomScore, 0.5f));
            }

            Debug.Log($"Update Score: {newScore}");
        }
    }
}