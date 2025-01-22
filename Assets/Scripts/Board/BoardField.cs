using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardField
{
    public Face DCELFace;
    public List<PaintedCard> paintStack = new();
    public PaintedCard lastPaintedCard;

    public class PaintedCard
    {
        public Card CardCopy;

        public PaintedCard(Card card)
        {
            CardCopy = (Card)card.Clone();
        }
    }

    public PaintedCard TopPaintedCard
    {
        get
        {
            if (paintStack.Count == 0) return null;
            return paintStack.Last();
        }
    }

    public event EventHandler OnUpdate;

    public void Update()
    {
        OnUpdate?.Invoke(this, EventArgs.Empty);
    }

    public BoardField(Face dcelFace)
    {
        DCELFace = dcelFace;
        DCELFace.BoardField = this;
    }

    public void AddCard(Card card)
    {
        paintStack.Add(new PaintedCard(card));
        lastPaintedCard = paintStack.Last();
        Update();
    }

    public void ChangeTopColor(CustomColor color)
    {
        paintStack.Last()?.CardCopy.ChangeColor(color);
        Update();
    }

    public List<BoardField> GetAllNeighbors()
    {
        return FilterNeighbors((x) => true);
    }

    public List<BoardField> FilterNeighbors(Func<BoardField, bool> filter)
    {
        List<BoardField> neighbors = new();
        List<HalfEdge> faceEdges = DCELFace.GetEdgesFromFace();
        foreach (var faceEdge in faceEdges)
        {
            if (faceEdge.Twin.Face == null) continue;
            Debug.Log(faceEdge.Twin.Face.GetOriginsFromFace().ToFormattedString());
            BoardField neighborBoardField = faceEdge.Twin.Face.BoardField;
            if (!filter(neighborBoardField)) continue;
            
            neighbors.Add(faceEdge.Twin.Face.BoardField);
        }

        return neighbors;
    }
}