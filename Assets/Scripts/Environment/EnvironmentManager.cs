using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EnvironmentManager : MonoBehaviourSingleton<EnvironmentManager>
{
    private int activeEnvironment = 0;
    private List<Environment> activeEnvironments = new List<Environment>();
    [SerializeField] UIController uiController;
    [SerializeField] TowerVisual towerVisual;

    private void Start()
    {
        activeEnvironments = GameObject.FindObjectsByType<Environment>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        foreach (Environment env in activeEnvironments)
        {
            env.Init();
        }
        uiController.InitSubscriptions(uiController);
        towerVisual.InitSubscriptions(uiController);
        foreach (Environment env in activeEnvironments)
        {
            env.StartEnvironment();
        }
    }

    private void OnDisable()
    {
        uiController.ResetSubscriptions();
        towerVisual.ResetSubscriptions();
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
