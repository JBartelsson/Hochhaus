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

    public PaintOnContextClass PaintOnContext;
    public CardMixingContextClass CardMixingContext;

    public Context(Environment env)
    {
        Env = env;
        if (env == null)
        {
            Debug.LogError($"ENVIRONMENT ISNT PASSED!!!");
        }
    }
}