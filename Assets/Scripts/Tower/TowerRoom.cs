using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class TowerRoom : IResetHandler
{
    public PlacedCard _PlacedCard;
    private PlayerStats lastStats;
    public PlayerStats LastStats => lastStats;
    
    
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

    public TowerRoom(Card card, PlayerStats lastStats)
    {
        _PlacedCard = new PlacedCard(card);
        this.lastStats = lastStats;
    }
   
    public void Reset()
    {
        _PlacedCard = null;
        Update();
    }

    public override string ToString()
    {
        return _PlacedCard.CardCopy.AppartmentReference.AppartmentName;
    }
}