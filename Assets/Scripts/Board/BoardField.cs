using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardField
{
    public Face DCELFace;
    public List<Card> paintStack = new List<Card>();

    public event EventHandler OnUpdate;

    public void Update()
    {
        OnUpdate?.Invoke(this, EventArgs.Empty);
    }
    public BoardField(Face dcelFace)
    {
        DCELFace = dcelFace;
    }

    public void AddCard(Card card)
    {
        paintStack.Add(card);
        Debug.Log($"{paintStack.ToFormattedString()}");
        Update();
    }
}