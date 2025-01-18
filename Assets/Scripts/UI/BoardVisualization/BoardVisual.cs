using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class BoardVisual : MonoBehaviour, ISubscriber
{
    [SerializeField] BoardFieldVisual _boardFieldVisual;
    

    List<BoardFieldVisual> boardFieldVisuals = new List<BoardFieldVisual>();
   
    private void BoardManagerOnInit(object sender, BoardManager e)
    {
        foreach (var boardField in e.BoardFields)
        {
            Debug.unityLogger.Log(boardField.ToString());
            BoardFieldVisual boardFieldVisual = Instantiate(_boardFieldVisual, this.transform);
            boardFieldVisual.Init(boardField);
            boardField.Update();
            boardFieldVisuals.Add(boardFieldVisual);
            Debug.Log($"Face Vertices: {boardField.DCELFace.GetVerticesFromFace().ToFormattedString()}");
        }
    }


    public void InitSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().BoardManager.OnInit += BoardManagerOnInit;

    }

    public void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().BoardManager.OnInit -= BoardManagerOnInit;
    }
}