using System;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class TowerManager : MonoBehaviour, IResetHandler
{
    private List<TowerRoom> _towerRooms = new List<TowerRoom>();

    
    public List<TowerRoom> TowerRooms => _towerRooms;

    private Environment env;

    public void SetEnvironment(Environment environment)
    {
        env = environment;
    }


    public event Action<TowerManager> OnInit; 
    public event Action<TowerManager, int, TowerRoom> OnAddedAppartment; 
 
    // Start is called before the first frame update
    public void InitTower()
    {
        OnInit?.Invoke(this);
    }

    private void UpdateAllFields()
    {
        foreach (var boardField in _towerRooms)
        {
            boardField.Update();
        }
    }

    public void CreateTower(Card card)
    {
        

        // OnAddedAppartment?.Invoke(this, _towerAppartments.Count - 1, newRoom);
    }

    public void Reset()
    {
        foreach (var boardField in _towerRooms)
        {
            boardField.Reset();
        }
    }
}