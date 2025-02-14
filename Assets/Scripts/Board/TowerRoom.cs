using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class TowerRoom : IResetHandler
{
    public PlacedCard _PlacedCard;
    
    private Score _score;
    public Score Score => _score;

    [FormerlySerializedAs("previousScore")] public Score lastScore;

    [Serializable]
    public class PlacedCard
    {
        public Card CardCopy;

        public PlacedCard(Card card)
        {
            CardCopy = (Card)card.Clone();
        }
    }

   

    public event EventHandler OnUpdate;

    public void Update()
    {
        OnUpdate?.Invoke(this, EventArgs.Empty);
    }

    public TowerRoom(Card card, Score lastScore)
    {
        _PlacedCard = new PlacedCard(card);
        _score = new Score();
        _score.AddPoints(card.RuntimePoints);
        this.lastScore = lastScore;
    }
   
    public void Reset()
    {
        _PlacedCard = null;
        Update();
    }
}