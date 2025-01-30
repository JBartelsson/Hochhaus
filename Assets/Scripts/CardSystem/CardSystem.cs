using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utility;
using Random = UnityEngine.Random;

[Serializable]
public class CardSystem : IResetHandler, IInitHandler
{
    private Environment _environment;
    List<Card> drawPile;
    List<Card> discardPile;
    private List<Card> fullDeck;
    private List<Card> hand;
    private ColorMixingDatabase _colorMixingDatabase;

    public List<Card> DrawPile => drawPile;

    public List<Card> DiscardPile => discardPile;

    public List<Card> FullDeck => fullDeck;

    public List<Card> Hand => hand;

    private Environment _env;
    
    public event EventHandler<CardSystem> OnCardSystemChanged; 


    public CardSystem(ColorMixingDatabase colorMixingDatabase, Environment env)
    {
        drawPile = new List<Card>();
        hand = new List<Card>();
        discardPile = new List<Card>();
        fullDeck = new List<Card>();
        _env = env;
        _colorMixingDatabase = colorMixingDatabase;
        Init();
    }


    // Shuffle the Current Deck of cards
    public void Shuffle()
    {
        for (int i = drawPile.Count - 1; i > 0;--i)
        {
            int j = Random.Range(0, i + 1);
            Card card = drawPile[j];
            drawPile[j] = drawPile[i];
            drawPile[i] = card;
        }
    }

    public bool MixHandCards(Card topCard, Card bottomCard)
    {
        Card newCard = _colorMixingDatabase.MixCards(topCard, bottomCard);
        if (newCard == null) return false;
        hand.Remove(topCard);
        int bottomCardIndex = hand.IndexOf(bottomCard);
        Debug.Log($"BOTTOM CARD INDEX: " + bottomCardIndex);
        hand.Remove(bottomCard);
        hand.Insert(bottomCardIndex, newCard);
        OnCardSystemChanged?.Invoke(this, this);
        Draw();
        return true;
    }

    // Return a list of drawn Cards from deck
    public void Draw(int numberToDraw =  1)
    {
        if (numberToDraw  > drawPile.Count)
            numberToDraw = drawPile.Count;
        List<Card> drawnCards = new List<Card>();
        for (int i =0;i<numberToDraw;++i)
        {
            drawnCards.Add(drawPile[0]);
            drawPile.RemoveAt(0);
        }
        hand.AddRange(drawnCards);
        _env.GameUpdate(Environment.GameEventType.DRAW_CARDS, _env.Ctx);
        OnCardSystemChanged?.Invoke(this, this);

    }

    public void DrawFullHand()
    {
        for (int i = hand.Count; i < _env.RoundStats.Stats.HandSize; i++)
        {
            Draw();
        }
    }

    // Return a list of drawn Cards from discard
    public List<Card> DrawDiscard(int numberToDraw =  1)
    {
        if (numberToDraw  > discardPile.Count)
            numberToDraw = discardPile.Count;
        List<Card> drawnCards = new List<Card>();
        for (int i =0;i<numberToDraw;++i)
        {
            drawnCards.Add(discardPile[0]);
            discardPile.RemoveAt(0);
        }
        return drawnCards;
    }


    // Put a card back, default is top  set bool to false to put it on the bottom
    public void ReturnCard(Card card, bool onTop = true)
    {
        if (onTop)
            drawPile.Insert(0, card);
        else
            drawPile.Add(card);
    }

    public void DiscardCard(Card card)
    {
        discardPile.Insert(0, card);
        hand.Remove(card);
        OnCardSystemChanged?.Invoke(this, this);

    }

    public void ShuffleDiscard()
    {
        foreach(Card OneCard in discardPile)
        {
            drawPile.Add(OneCard);
        }
        discardPile.Clear();
        Shuffle();
    }

    public void Init()
    {
        StartDeck startDeck = _env.EnvSettings.StartDeck;
        foreach (var colorEntry in startDeck.Deck)
        {
            for (int i = 0; i < colorEntry.Amount; i++)
            {
                AddCard(new Card(colorEntry.Color));
            }
        }
        Reset();
    }

    public void AddCard(Card card)
    {
        fullDeck.Add(card);
        OnCardSystemChanged?.Invoke(this, this);

    }
    
    
    public override string ToString()
    {
        string s = "";
        s += "FULL DECK:";
        foreach (var card in fullDeck)
        {
            s+= card.ToString() + ", ";
        }
        s += "--DRAW PILE:";
        foreach (var card in drawPile)
        {
            s+= card.ToString() + ", ";
        }
        s.Remove(s.Length - 2, 2);
        
        s += "--HAND:";
        foreach (var card in hand)
        {
            s+= card.ToString() + ", ";
        }
        s.Remove(s.Length - 2, 2);
        
        s += "--DISCARD PILE:";
        foreach (var card in discardPile)
        {
            s+= card.ToString() + ", ";
        }
        s.Remove(s.Length - 2, 2);
        return s;
    }

    public void Reset()
    {
        drawPile.Clear();
        discardPile.Clear();
        hand.Clear();
        drawPile.AddRange(fullDeck);
        Shuffle();
        OnCardSystemChanged?.Invoke(this, this);
    }
}