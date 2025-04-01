using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

[Serializable]
public class EnvStats: ICloneable, IInitHandler
{
    
    
    public List<StatItem> StatsList = new List<StatItem>();
    public Dictionary<PlayerStats.PlayerStat, float> Stats;
    
    
    public float Fabric
    {
        get => Stats[PlayerStats.PlayerStat.FABRIC];
        set => Stats[PlayerStats.PlayerStat.FABRIC] = value;
    }
    public float FabricMult
    {
        get => Stats[PlayerStats.PlayerStat.FABRIC_MULT];
        set => Stats[PlayerStats.PlayerStat.FABRIC_MULT] = value;
    }
    public float ScorePoints
    {
        get => Stats[PlayerStats.PlayerStat.SCORE_POINTS];
        set => Stats[PlayerStats.PlayerStat.SCORE_POINTS] = value;
    }
    public float ScoreMult
    {
        get => Stats[PlayerStats.PlayerStat.SCORE_MULT];
        set => Stats[PlayerStats.PlayerStat.SCORE_MULT] = value;
    }
    public float ScoreXMult
    {
        get => Stats[PlayerStats.PlayerStat.SCORE_xMULT];
        set => Stats[PlayerStats.PlayerStat.SCORE_xMULT] = value;
    }
    public float DrawCost
    {
        get => Stats[PlayerStats.PlayerStat.DRAW_COST];
        set => Stats[PlayerStats.PlayerStat.DRAW_COST] = value;
    }
    public float HandSize
    {
        get => Stats[PlayerStats.PlayerStat.HAND_SIZE];
        set => Stats[PlayerStats.PlayerStat.HAND_SIZE] = value;
    }
    public float ShopSizeItems
    {
        get => Stats[PlayerStats.PlayerStat.SHOP_SIZE_ITEMS];
        set => Stats[PlayerStats.PlayerStat.SHOP_SIZE_ITEMS] = value;
    }
    public float ShopSizeConsumables
    {
        get => Stats[PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES];
        set => Stats[PlayerStats.PlayerStat.SHOP_SIZE_CONSUMABLES] = value;
    }
    public float RerollCost
    {
        get => Stats[PlayerStats.PlayerStat.REROLL_COST];
        set => Stats[PlayerStats.PlayerStat.REROLL_COST] = value;
    }
    public float RerollCostIncrease
    {
        get => Stats[PlayerStats.PlayerStat.REROLL_COST_INCREASE];
        set => Stats[PlayerStats.PlayerStat.REROLL_COST_INCREASE] = value;
    }
    public float HandsUntilIncrease
    {
        get => Stats[PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE];
        set => Stats[PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE] = value;
    }
    public float HandsLeft
    {
        get => Stats[PlayerStats.PlayerStat.HANDS_LEFT];
        set => Stats[PlayerStats.PlayerStat.HANDS_LEFT] = value;
    }
    public float HandsTotal
    {
        get => Stats[PlayerStats.PlayerStat.HANDS_TOTAL];
        set => Stats[PlayerStats.PlayerStat.HANDS_TOTAL] = value;
    }
    
    public float TotalScore
    {
        get => Stats[PlayerStats.PlayerStat.TOTAL_SCORE];
        set => Stats[PlayerStats.PlayerStat.TOTAL_SCORE] = value;
    }
    
    //property for TOtal Score
    
    
[Serializable]
    public class StatItem
    {
        public PlayerStats.PlayerStat stat;
        public float value;
    }

    public EnvStats()
    {
        Init();
        Stats[PlayerStats.PlayerStat.HANDS_LEFT] = Stats[PlayerStats.PlayerStat.HANDS_UNTIL_INCREASE];
    }


    public void ResetStats()
    {
    }
   
    public object Clone()
    {
        EnvStats clone = (EnvStats) MemberwiseClone();
        clone.Stats = new Dictionary<PlayerStats.PlayerStat, float>(Stats);
        return clone;
    }

    public bool CanDraw()
    {
        return Fabric - DrawCost >= 0;
    }

    public void Init()
    {
        Stats = StatsList.ToDictionary(x => x.stat, x => x.value);
        for (int i = 0; i <= Enum.GetValues(typeof(PlayerStats.PlayerStat)).Cast<int>().Max(); i++)
        {
            if (!Stats.ContainsKey((PlayerStats.PlayerStat)i))
            {
                Stats.Add((PlayerStats.PlayerStat)i, 0);
            }
        }
      
    }

    public override string ToString()
    {
        string result = "";
        foreach (var stat in Stats)
        {
            result += $"{stat.Key}: {stat.Value}, ";
        }

        return result;
    }
}