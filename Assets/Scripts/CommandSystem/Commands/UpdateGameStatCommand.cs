using System;
using Items;
using UnityEngine;

namespace CommandSystem.Commands
{
    public class UpdateGameStatCommand : CommandBase
    {
        public float Value => value;


        private PlayerStats lastStats;
        private PlayerStats newStats;

        public bool Init { get; set; }

        public PlayerStats LastStats => lastStats;

        public PlayerStats NewStats => newStats;

        private float value;
        private float oldValue;
        private float newValue;

        public float OldValue => oldValue;

        public float NewValue => newValue;

        private PlayerStats.PlayerStat _playerStat;
        public PlayerStats.PlayerStat PlayerStat => _playerStat;


        public UpdateGameStatCommand(Environment env, Item sender, PlayerStats.PlayerStat playerStat, float value,
            bool init = false, CommandBase parent = null) :
            base(env, sender, parent)
        {
            Init = init;
            _playerStat = playerStat;
            this.value = value;
        }


        protected override void ExecuteSingle()
        {
            Debug.Log($"Executing UpdateGameStatCommand: {PlayerStat} {Value}");

            switch (_playerStat)
            {
                case PlayerStats.PlayerStat.FABRIC:
                    UpdateFabric();
                    break;

                case PlayerStats.PlayerStat.DRAW_COST:
                    UpdateDraws();
                    break;
                case PlayerStats.PlayerStat.HAND_SIZE:
                    UpdateHandSize();
                    break;
                case PlayerStats.PlayerStat.SHOP_SIZE_ITEMS:
                    UpdateShopSizeItems();
                    break;

                case PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES:
                    UpdateShopSizeConsumables();
                    break;
                case PlayerStats.PlayerStat.REROLL_COST:
                    UpdateRerollCost();
                    break;
                case PlayerStats.PlayerStat.REROLL_COST_INCREASE:
                    UpdateRerollCostIncrease();
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
                case PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE:
                    UpdateHandsUntilIncrease();
                    break;
                    
                case PlayerStats.PlayerStat.HANDS_LEFT:
                    UpdateHandsLeft();
                    break;
                case PlayerStats.PlayerStat.HANDS_TOTAL:
                    UpdateHands();
                    break;
                case PlayerStats.PlayerStat.FABRIC_MULT:
                    UpdateFabricMult();
                    break;
                case PlayerStats.PlayerStat.CHANCE_DATA:
                case PlayerStats.PlayerStat.INT_DATA:
                case PlayerStats.PlayerStat.FLOAT_DATA:
                default:
                    break;
            }

            // Debug.Log("STATS");
            // Debug.Log("Stats " + _env.PlayerStats.Stats.Stats.Count);
            // foreach (var pair in _env.PlayerStats.Stats.Stats)
            // {
            //     Debug.Log($"{pair.Key} : {pair.Value}");
            // }

            _env.GameUpdate(Environment.GameStateType.GAME_STAT_UPDATE, new Context(_env)
            {
                UpdateGameStatCommand = this
            });
        }

        private void UpdateFabricMult()
        {
            oldValue = _env.PlayerStats.Stats.FabricMult;
            newValue = (float)_env.PlayerStats.Stats.FabricMult + value;
            _env.PlayerStats.Stats.FabricMult = newValue;
        }
        
        //Update Hands UNTIL Increase
        private void UpdateHandsUntilIncrease()
        {
            oldValue = _env.PlayerStats.Stats.HandsUntilIncrease;
            newValue = (float)_env.PlayerStats.Stats.HandsUntilIncrease + value;
            _env.PlayerStats.Stats.HandsUntilIncrease = Mathf.FloorToInt(newValue);
        }

        private void UpdateHandsLeft()
        {
            oldValue = _env.PlayerStats.Stats.HandsLeft;
            newValue = (float)_env.PlayerStats.Stats.HandsLeft + value;
            _env.PlayerStats.Stats.HandsLeft = Mathf.FloorToInt(newValue);
            if (_env.PlayerStats.Stats.HandsLeft == 0)
            {
                //Increase Draw Cost
                UpdateGameStatCommand updateDrawCost =
                    new UpdateGameStatCommand(_env, null, PlayerStats.PlayerStat.DRAW_COST, 1);
                _env.CommandInvoker.Execute(updateDrawCost);
                // //Reset Hands
                UpdateGameStatCommand updateHandsLeft = new UpdateGameStatCommand(_env, null,
                    PlayerStats.PlayerStat.HANDS_LEFT, _env.PlayerStats.Stats.HandsUntilIncrease);
                _env.CommandInvoker.Execute(updateHandsLeft);
            }
        }

        private void UpdateHands()
        {
            oldValue = _env.PlayerStats.Stats.HandsTotal;

            newValue = (float)_env.PlayerStats.Stats.HandsTotal + value;
            _env.PlayerStats.Stats.HandsTotal = Mathf.FloorToInt(newValue);
            UpdateGameStatCommand updateHands =
                new UpdateGameStatCommand(_env, null, PlayerStats.PlayerStat.HANDS_LEFT, -value);
            _env.CommandInvoker.Execute(updateHands);
        }

        private void UpdateRerollCostIncrease()
        {
            oldValue = _env.PlayerStats.Stats.RerollCostIncrease;
            newValue = (float)_env.PlayerStats.Stats.RerollCostIncrease + value;
            _env.PlayerStats.Stats.RerollCostIncrease = Mathf.FloorToInt(newValue);
        }

        private void UpdateRerollCost()
        {
            oldValue = _env.PlayerStats.Stats.RerollCost;
            newValue = (float)_env.PlayerStats.Stats.RerollCost + value;
            _env.PlayerStats.Stats.RerollCost = Mathf.FloorToInt(newValue);
        }

        // Update shop size Items
        private void UpdateShopSizeItems()
        {
            oldValue = _env.PlayerStats.Stats.ShopSizeItems;
            newValue = (float)_env.PlayerStats.Stats.ShopSizeItems + value;
            _env.PlayerStats.Stats.ShopSizeItems = Mathf.FloorToInt(newValue);
        }

        // Update shop size Consumables
        private void UpdateShopSizeConsumables()
        {
            oldValue = _env.PlayerStats.Stats.ShopSizeConsumables;
            newValue = (float)_env.PlayerStats.Stats.ShopSizeConsumables + value;
            _env.PlayerStats.Stats.ShopSizeConsumables = Mathf.FloorToInt(newValue);
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
            lastStats = (PlayerStats)_env.PlayerStats.Clone();
            Debug.Log($"Last Score {lastStats}");
            _env.PlayerStats.InsertScoreModifier(PlayerStat, value, Environment.GameStateType.BUILD_ROOM_END, sender);
            newStats = (PlayerStats)_env.PlayerStats.Clone();
            newStats.CalculateScore();
            Debug.Log($"New Score {newStats}");
        }

        public override void Undo()
        {
        }

        public override string ToString()
        {
            return $"UpdateGameStatCommand: {PlayerStat} {Value}";
        }
    }
}