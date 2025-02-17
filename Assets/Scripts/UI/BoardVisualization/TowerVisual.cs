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
    [FormerlySerializedAs("appartmentVisualPrefab")] [SerializeField] RoomVisual roomVisualPrefab;


    [SerializeField] private Transform towerVisualParent;
    [SerializeField] private Transform currentSpawnPosition;
    [SerializeField] private Transform displayTextPosition;

    public RoomVisual RoomVisualPrefab => roomVisualPrefab;
    public Transform CurrentSpawnPosition => currentSpawnPosition;
    public Transform TowerVisualParent => towerVisualParent;
    
    public Transform DisplayTextPosition => displayTextPosition;

    List<RoomVisual> appartmentVisuals = new List<RoomVisual>();
    public List<RoomVisual> AppartmentVisuals => appartmentVisuals;

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
        Debug.Log("Moving current spawn");
        currentSpawnPosition.transform.position += new Vector3(0f, appartmentVisuals.Last().Height, 0f);
    }


    public override void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnInit -= TowerManagerOnInit;
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnAddedAppartment -=
            TowerManagerOnOnAddedAppartment;
    }
}