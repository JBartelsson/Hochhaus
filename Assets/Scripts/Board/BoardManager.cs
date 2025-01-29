using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BoardManager : MonoBehaviour, IResetHandler
{
    [SerializeField] private BoardLayout _boardLayout;
    DCEL _dcel;
    List<BoardField> _boardFields = new List<BoardField>();

    public DCEL Dcel
    {
        get => _dcel;
        set => _dcel = value;
    }

    public List<BoardField> BoardFields
    {
        get => _boardFields;
        set => _boardFields = value;
    }

    public event EventHandler<BoardManager> OnInit; 
 
    // Start is called before the first frame update
    public void InitBoard()
    {
        _dcel = _boardLayout.ConvertToDCEL();
        int scale = 4;
        foreach (var face in _dcel.Faces)
        {
            _boardFields.Add(new BoardField(face));
        }
        OnInit?.Invoke(this, this);
    }

    private void UpdateAllFields()
    {
        foreach (var boardField in _boardFields)
        {
            boardField.Update();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        foreach (var boardField in _boardFields)
        {
            boardField.Reset();
        }
    }
}