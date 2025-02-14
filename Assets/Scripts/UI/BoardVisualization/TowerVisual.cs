using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandSystem.AnimationCommands;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerVisual : UIBase, ISubscriber
{
    [SerializeField] AppartmentVisual appartmentVisualPrefab;


    [SerializeField] private Transform towerVisualParent;
    [SerializeField] private Transform currentSpawnPosition;
    [SerializeField] private float scale = 4f;

    public AppartmentVisual AppartmentVisualPrefab => appartmentVisualPrefab;
    public Transform CurrentSpawnPosition => currentSpawnPosition;
    public Transform TowerVisualParent => towerVisualParent;

    List<AppartmentVisual> appartmentVisuals = new List<AppartmentVisual>();
    public List<AppartmentVisual> AppartmentVisuals => appartmentVisuals;

    private void TowerManagerOnInit(TowerManager towerManager)
    {
    }


    private void TowerManagerOnOnAddedAppartment(TowerManager towerManager, int i, TowerRoom arg3)
    {
        
    }

    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);

        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnInit += TowerManagerOnInit;
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnAddedAppartment +=
            TowerManagerOnOnAddedAppartment;
    }

    public void MoveCurrentSpawn()
    {
        currentSpawnPosition.transform.position += new Vector3(0f, appartmentVisuals.Last().Height, 0f);
    }


    public override void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnInit -= TowerManagerOnInit;
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnAddedAppartment -=
            TowerManagerOnOnAddedAppartment;
    }
}