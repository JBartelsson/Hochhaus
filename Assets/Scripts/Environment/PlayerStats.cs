using System;
using Utility;
[Serializable]
public class PlayerStats : IInitHandler, IResetHandler, ICloneable
{
    private Score _score;

    public Score Score => _score;

    public float Money { get; set; }

    private EnvStats stats;
    private EnvStats startStats;

    public enum PlayerStat
    {
        MONEY,
        SCORE,
        DRAWS,
        HAND_SIZE
    }
    public EnvStats Stats => stats;

    public PlayerStats(EnvSettings envSettings)
    {
        startStats = envSettings.startEnvStats;
        stats = (EnvStats)envSettings.startEnvStats.Clone();
        _score = new Score();
        Reset();
    }

    


    public void Init()
    {
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