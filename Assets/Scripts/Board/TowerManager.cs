using System;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class TowerManager : MonoBehaviour, IResetHandler
{
    private List<TowerRoom> _towerAppartments = new List<TowerRoom>();

    
    public List<TowerRoom> TowerAppartments => _towerAppartments;

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
        foreach (var boardField in _towerAppartments)
        {
            boardField.Update();
        }
    }

    public void CreateTower(Card card)
    {
        TowerRoom newRoom = new TowerRoom(card, (Score)env.PlayerStats.Score.Clone());
        _towerAppartments.Add(newRoom);
        AddPointsCommand addPoints = new AddPointsCommand(env, newRoom.Score.Points);
        env.CommandInvoker.ExecuteAndRecord(addPoints);
        env.CalculateRoom(Environment.GameEventType.BUILD_ROOM);

        OnAddedAppartment?.Invoke(this, _towerAppartments.Count - 1, newRoom);
    }

    public void Reset()
    {
        foreach (var boardField in _towerAppartments)
        {
            boardField.Reset();
        }
    }
}