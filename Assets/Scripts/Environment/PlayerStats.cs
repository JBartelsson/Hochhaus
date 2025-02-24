using System;
using CommandSystem.Commands;
using Utility;
[Serializable]
public class PlayerStats : EnvBase, IInitHandler, IResetHandler, ICloneable
{
    public PlayerStats(Environment env) : base(env)
    {
        startStats = env.EnvSettings.startEnvStats;
        stats = new EnvStats();
        _score = new Score();
        
        Reset();
    }


    private Score _score;

    public Score Score => _score;

    private EnvStats stats;
    private EnvStats startStats;

    public enum PlayerStat
    {
        FABRIC,
        SCORE,
        DRAW_COST,
        HAND_SIZE
    }
    public EnvStats Stats => stats;

    


    public void Init()
    {
        UpdateGameStatCommand updateFabric = new UpdateGameStatCommand(Env, null, PlayerStat.FABRIC,  (float)startStats.Fabric);
        Env.CommandInvoker.ExecuteAndRecord(updateFabric);
        UpdateGameStatCommand updateDrawCost = new UpdateGameStatCommand(Env, null, PlayerStat.DRAW_COST,  (float)startStats.DrawCost);
        Env.CommandInvoker.ExecuteAndRecord(updateDrawCost);
        UpdateGameStatCommand updateHandsize = new UpdateGameStatCommand(Env, null, PlayerStat.HAND_SIZE,  (float)startStats.HandSize);
        Env.CommandInvoker.ExecuteAndRecord(updateHandsize);
        Reset();
    }

    public void Reset()
    {
        _score.ResetTotalScore();
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}