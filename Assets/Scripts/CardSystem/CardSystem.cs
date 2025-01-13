using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardSystem {
    List<Card> drawPile;
    List<Card> discardPile;
    private List<Card> fullDeck;
    private List<Card> hand;
    private ColorMixingDatabase _colorMixingDatabase;

    public List<Card> DrawPile => drawPile;

    public List<Card> DiscardPile => discardPile;

    public List<Card> FullDeck => fullDeck;

    public List<Card> Hand => hand;

    private EnvSettings _envSettings;

    public CardSystem(ColorMixingDatabase colorMixingDatabase, EnvSettings envSettings)
    {
        drawPile = new List<Card>();
        hand = new List<Card>();
        discardPile = new List<Card>();
        fullDeck = new List<Card>();
        _envSettings = envSettings;
        _colorMixingDatabase = colorMixingDatabase;
        Reset();
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

    public void MixHandCards(Card card1, Card card2)
    {
        Card newCard = _colorMixingDatabase.MixCards(card1, card2);
        hand.Remove(card1);
        hand.Remove(card2);
        hand.Add(newCard);
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
    }

    public void DrawFullHand()
    {
        for (int i = hand.Count; i < _envSettings.HandSize; i++)
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

    public void Reset()
    {
        StartDeck startDeck = _envSettings.StartDeck;
        foreach (var colorEntry in startDeck.Deck)
        {
            for (int i = 0; i < colorEntry.Amount; i++)
            {
                AddCard(new Card(colorEntry.Color));
                Debug.Log("Adding Card");
            }
        }
        drawPile.AddRange(fullDeck);
    }

    public void AddCard(Card card)
    {
        fullDeck.Add(card);
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
}