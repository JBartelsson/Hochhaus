using Utility;

public class RoundStats : IInitHandler, ISellPaintingHandler, IResetHandler
{
    private Score _score;

    public Score Score => _score;
    
    private EnvStats stats;
    private EnvStats startStats;

    public EnvStats Stats => stats;

    public RoundStats(EnvSettings envSettings)
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

    public void SellPainting()
    {
        _score.SellPainting();
        stats.Subtract(amountOfPaintings: 1);
    }

    public void Reset()
    {
        stats.Reset(startStats);
        _score.ResetTotalScore();
    }
}