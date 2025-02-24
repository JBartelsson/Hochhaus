using System;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class UpdateGameStatCommand : CommandBase
    {
        public float Value => value;

        public Score.ScoreType ScoreType => _scoreType;

        private Score lastScore;
        private Score newScore;

        

        public Score LastScore => lastScore;

        public Score NewScore => newScore;

        private float value;
        private float oldValue;
        private float newValue;

        public float OldValue => oldValue;

        public float NewValue => newValue;

        private Score.ScoreType _scoreType;
        private PlayerStats.PlayerStat _playerStat;
        public PlayerStats.PlayerStat PlayerStat => _playerStat;

        public UpdateGameStatCommand(Environment env, Item sender, Score.ScoreType scoreType, float value) : base(env, sender)
        {
            _scoreType = scoreType;
            this.value = value;
            _playerStat = PlayerStats.PlayerStat.SCORE;
        }
        
        public UpdateGameStatCommand(Environment env, Item sender, PlayerStats.PlayerStat playerStat, float value) : base(env, sender)
        {
            if (playerStat == PlayerStats.PlayerStat.SCORE)
            {
                Debug.LogError("You shouldnt use this Constructur with Scores");
            }
            _playerStat = playerStat;
            this.value = value;
        }


        public override void Execute()
        {
            switch (_playerStat)
            {
                case PlayerStats.PlayerStat.FABRIC:
                    UpdateFabric();
                    break;
                case PlayerStats.PlayerStat.SCORE:
                    UpdateScore();
                    break;
                case PlayerStats.PlayerStat.DRAW_COST:
                    UpdateDraws();
                    break;
                case PlayerStats.PlayerStat.HAND_SIZE:
                    UpdateHandSize();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _env.GameUpdate(Environment.GameStateType.GAME_STAT_UPDATE, new Context(_env)
            {
                UpdateGameStatCommand = this
            });
        }

        private void UpdateDraws()
        {
            oldValue = _env.PlayerStats.Stats.DrawCost;
            newValue = (float)_env.PlayerStats.Stats.DrawCost + value;
            _env.PlayerStats.Stats.DrawCost = Mathf.FloorToInt(newValue);
        }

        private void UpdateHandSize()
        {
            oldValue = _env.PlayerStats.Stats.HandSize;
            newValue = (float)_env.PlayerStats.Stats.HandSize + value;
            _env.PlayerStats.Stats.HandSize = Mathf.FloorToInt(newValue);
        }

        private void UpdateFabric()
        {
            oldValue = _env.PlayerStats.Stats.Fabric;
            newValue = (float)_env.PlayerStats.Stats.Fabric + value;
            _env.PlayerStats.Stats.Fabric = Mathf.FloorToInt(newValue);
        }

        private void UpdateScore()
        {
            lastScore = (Score)_env.PlayerStats.Score.Clone();
            Debug.Log($"Last Score {lastScore}");
            _env.PlayerStats.Score.InsertScoreModifier(_scoreType, value);
            newScore = (Score)_env.PlayerStats.Score.Clone();
            newScore.CalculateScore();
            Debug.Log($"New Score {newScore}");
        }

        public override void Undo()
        {
            _env.PlayerStats.Score.RemovePoints(value);

        }

        public override string ToString()
        {
            return $"UpdateGameStatCommand: {PlayerStat} {ScoreType} {Value}";
        }
    }
}