using UnityEngine;

public class Context
{
    

    public Environment Env;
    public class PaintOnContextClass
    {
        public TowerRoom TowerRoom;
    }

    public class CardMixingContextClass
    {
        public Card TopCard;
        public Card BottomCard;
    }
    
    public class MainPhaseContextClass
    {
        public TowerRoom ScoringTowerRoom;
    }

    public PaintOnContextClass PaintOnContext;
    public CardMixingContextClass CardMixingContext;
    public MainPhaseContextClass MainPhaseContext;
    public PlayerStats PlayerStats;

    public Context(Environment env)
    {
        Env = env;
        PaintOnContext = new PaintOnContextClass();
        CardMixingContext = new CardMixingContextClass();
        MainPhaseContext = new MainPhaseContextClass();
        this.PlayerStats = env.PlayerStats;
        if (env == null)
        {
            Debug.LogError($"ENVIRONMENT ISNT PASSED!!!");
        }
    }
}