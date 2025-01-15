using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnvironmentManager : MonoBehaviourSingleton<EnvironmentManager>
{
    private int activeEnvironment = 0;
    private List<Environment> activeEnvironments = new List<Environment>();
    [SerializeField] UIController uiController;

    private void Start()
    {
        activeEnvironments = GameObject.FindObjectsByType<Environment>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        uiController.InitSubscriptions();
        foreach (Environment env in activeEnvironments)
        {
            env.StartEnvironment();
        }
    }

    public Environment GetActiveEnvironment()
    {
        if (activeEnvironment < 0 || activeEnvironment >= activeEnvironments.Count)
        {
            Debug.LogError("No environment selected");
            return null;
        }
        return activeEnvironments[activeEnvironment];
    }
}
