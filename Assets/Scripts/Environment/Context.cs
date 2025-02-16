using Items;
using UnityEditor;
using UnityEngine;

public class Context
{
    

    public Environment Env;

    public Item CurrentItem;

    public Context(Environment env)
    {
        Env = env;
        if (env == null)
        {
            Debug.LogError($"ENVIRONMENT ISNT PASSED!!!");
        }
    }
}