using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.Commands;
using Items;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

[Serializable]
public class PlayerStats : EnvBase, IInitHandler, IResetHandler, ICloneable
{
    public static int idnr = 0;
    public int ID;
    public PlayerStats(Environment env) : base(env)
    {
        startStats = env.EnvSettings.startEnvStats;
        stats = new EnvStats();
        ID = idnr;
        idnr++;

        Reset();
    }
    
    public class StatModifier
    {
        public float Value;
        public PlayerStats.PlayerStat stat;
        public Item sender;
        public Environment.GameStateType removeCondition;

        public override string ToString()
        {
            return $"{stat}: {Value}";
        }
    }
    
    List<StatModifier> statModifiers = new List<StatModifier>();
    
    public void InsertScoreModifier(PlayerStat scoreType, float value, Environment.GameStateType removeCondition, Item sender = null)
    {
        statModifiers.Add(new StatModifier(){stat = scoreType, Value = value, sender = sender, removeCondition = removeCondition});        
    }
    
    public float xMult
    {
        get
        {
            float xMult = 1f;
            statModifiers.Where((x) => x.stat == PlayerStats.PlayerStat.SCORE_xMULT).ToList().ForEach((x) => xMult *= x.Value);
            return xMult;
        }
    }
    
    public float RoomScore
    {
        get
        {
            float score = 0;
            foreach (var modifier in statModifiers)
            {
                switch (modifier.stat)
                {
                    case PlayerStats.PlayerStat.SCORE_POINTS:
                        score += modifier.Value;
                        break;
                    default:
                        break;
                }
            }
            return score * RoomMult;
        }
    }

    public float RoomMult
    {
        get
        {
            float mult = 1f;
            foreach (var modifier in statModifiers)
            {
                switch (modifier.stat)
                {
                    
                    case PlayerStats.PlayerStat.SCORE_MULT:
                        mult += modifier.Value;
                        break;
                    case PlayerStats.PlayerStat.SCORE_xMULT:
                        mult *= modifier.Value;
                        break;
                    default:
                        break;
                }
            }
            return mult;
        }
    }

    public float RoomFabric
    {
        get
        {
            //calculate Fabric based on the modifiers
            float fabric = 0;
            float fabricMult = 1f;
            foreach (var modifier in statModifiers)
            {
                if (modifier.stat == PlayerStats.PlayerStat.FABRIC)
                {
                    fabric += modifier.Value;
                }
                if (modifier.stat == PlayerStats.PlayerStat.FABRIC_MULT)
                {
                    fabricMult += modifier.Value;
                }
                if (modifier.stat == PlayerStats.PlayerStat.FABRIC_xMULT)
                {
                    fabricMult *= modifier.Value;
                }
            }

            return fabric * fabricMult;
        }
    }

    private EnvStats stats;
    private EnvStats startStats;

    public enum PlayerStat
    {
        FABRIC = 0,
        SCORE_POINTS = 1,
        SCORE_MULT = 2,
        SCORE_xMULT = 3,
        DRAW_COST = 4,
        HAND_SIZE = 5,
        SHOP_SIZE_ITEMS = 6,
        SHOP_SIZE_CONSUMABLES = 7,
        REROLL_COST = 8,
        REROLL_COST_INCREASE = 9,
        HANDS_UNTIL_INCREASE = 10,
        HANDS_LEFT = 11,
        HANDS_TOTAL = 12,
        FABRIC_MULT = 13,
        CHANCE_DATA = 14,
        INT_DATA = 15,
        FLOAT_DATA = 16,
        SCORE_TOTAL = 17,
        FABRIC_TOTAL = 18,
        FABRIC_xMULT = 19,
    }

    public EnvStats Stats => stats;


    public void Init()
    {
        var dictCopy = startStats.Stats.ToList().ToDictionary(x => x.Key, x => x.Value);
        foreach (var keyPair in dictCopy)
        {
            UpdateGameStatCommand updateFabric =
                new UpdateGameStatCommand(Env, null, keyPair.Key, keyPair.Value, true);
            Env.CommandInvoker.Execute(updateFabric);
        }
        Reset();
    }

    public void Reset()
    {
        ResetTotalScore();
    }
    
    public void CalculateScore()
    {
        //calculate the score of the room
        stats.ScoreTotal += RoomScore;
        stats.FabricTotal += RoomFabric;
        // UpdateGameStatCommand fabricCommand = new UpdateGameStatCommand(Env, null, PlayerStat.FABRIC_TOTAL, RoomFabric);
        // Env.CommandInvoker.Execute(fabricCommand);
        // UpdateGameStatCommand scoreCommand = new UpdateGameStatCommand(Env, null, PlayerStat.SCORE_TOTAL, RoomScore);
        // Env.CommandInvoker.Execute(scoreCommand);
        //log all the score modifiers
        // Debug.Log($"PlayerStats {ID}:" +statModifiers.ToFormattedString());
    }

    // Reset the score and multiplier
    public void ResetRoomScore()
    {
        statModifiers.RemoveAll((x) => x.removeCondition == Environment.GameStateType.BUILD_ROOM_END);
        // Notify all listeners about the reset
    }

    public void ResetTotalScore()
    {
        stats.ScoreTotal = 0;
        ResetRoomScore();
    }

    public object Clone()
    {
        PlayerStats newScore = new PlayerStats(env);
        newScore.stats = (EnvStats)stats.Clone();
        newScore.statModifiers = new List<StatModifier>(statModifiers);
        return newScore;
    }

    public override string ToString()
    {
        return $"Player Stats ID {ID}" + stats.ToString() + " " + statModifiers.ToFormattedString();
    }
}