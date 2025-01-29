using UnityEngine;

public class Context
{
    

    public Environment Env;
    public class PaintOnContextClass
    {
        public BoardField BoardField;
    }

    public class CardMixingContextClass
    {
        public Card TopCard;
        public Card BottomCard;
    }
    
    public class MainPhaseContextClass
    {
        public BoardField ScoringBoardField;
    }

    public PaintOnContextClass PaintOnContext;
    public CardMixingContextClass CardMixingContext;
    public MainPhaseContextClass MainPhaseContext;
    public RoundStats RoundStats;

    public Context(Environment env)
    {
        Env = env;
        PaintOnContext = new PaintOnContextClass();
        CardMixingContext = new CardMixingContextClass();
        MainPhaseContext = new MainPhaseContextClass();
        this.RoundStats = env.RoundStats;
        if (env == null)
        {
            Debug.LogError($"ENVIRONMENT ISNT PASSED!!!");
        }
    }
}