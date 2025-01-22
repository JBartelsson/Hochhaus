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
    [SerializeField] private Transform _boardVisualParent;
    [SerializeField] private float scale = 4f;


    List<BoardFieldVisual> boardFieldVisuals = new List<BoardFieldVisual>();
   
    private void BoardManagerOnInit(object sender, BoardManager e)
    {
        for (var i = 0; i < e.BoardFields.Count; i++)
        {
            BoardFieldVisual boardFieldVisual = Instantiate(_boardFieldVisual, _boardVisualParent);
            boardFieldVisual.Init(e.BoardFields[i], scale);
            boardFieldVisual.gameObject.name = $"BoardFieldVisual{i}";
            e.BoardFields[i].Update();
            boardFieldVisuals.Add(boardFieldVisual);  
        }
            
    }

    public BoardFieldVisual ReturnBoardFieldVisual(Vector3 worldPosition)
    {
        worldPosition = worldPosition - _boardVisualParent.transform.position;
        Vector2 boardPosition = new Vector2(worldPosition.x, worldPosition.z) / scale;
        foreach (var boardFieldVisual in boardFieldVisuals)
        {
            if (boardFieldVisual.BoardField.DCELFace.IsPointInFace(boardPosition))
            {
                return boardFieldVisual;
            }
        }
        return null;
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