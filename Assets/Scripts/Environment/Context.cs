using CommandSystem.Commands;
using Items;
using UnityEditor;
using UnityEngine;

public class Context
{
    

    public Environment Env;

    public Item CurrentItem;

    public CommandBase NextCommand;

    public Score LastScore;

    public Card LastDrawnCard;

    public UpdateGameStatCommand UpdateGameStatCommand;
    public Context(Environment env)
    {
        Env = env;
        if (env == null)
        {
            Debug.LogError($"ENVIRONMENT ISNT PASSED!!!");
        }
    }
}