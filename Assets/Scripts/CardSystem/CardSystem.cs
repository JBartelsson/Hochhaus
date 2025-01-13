using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardSystem<T> {
    List<T> cards;
    List<T> discard;
    private List<T> hand;

    public CardSystem(List<T> cards)
    {
        this.cards = cards;
        discard = new List<T>();
    }

    // Shuffle the Current Deck of cards
    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0;--i)
        {
            int j = Random.Range(0, i + 1);
            T card = cards[j];
            cards[j] = cards[i];
            cards[i] = card;
        }
    }

    // Return a list of drawn Cards from deck
    public void Draw(int numberToDraw =  1)
    {
        if (numberToDraw  > cards.Count)
            numberToDraw = cards.Count;
        List<T> drawnCards = new List<T>();
        for (int i =0;i<numberToDraw;++i)
        {
            drawnCards.Add(cards[0]);
            cards.RemoveAt(0);
        }
        hand.AddRange(drawnCards);
    }

    // Return a list of drawn Cards from discard
    public List<T> DrawDiscard(int numberToDraw =  1)
    {
        if (numberToDraw  > discard.Count)
            numberToDraw = discard.Count;
        List<T> drawnCards = new List<T>();
        for (int i =0;i<numberToDraw;++i)
        {
            drawnCards.Add(discard[0]);
            discard.RemoveAt(0);
        }
        return drawnCards;
    }


    // Put a card back, default is top  set bool to false to put it on the bottom
    public void ReturnCard(T card, bool onTop = true)
    {
        if (onTop)
            cards.Insert(0, card);
        else
            cards.Add(card);
    }

    public void DiscardCard(T card)
    {
        discard.Insert(0, card);
        hand.Remove(card);
    }

    public void ShuffleDiscard()
    {
        foreach(T OneCard in discard)
        {
            cards.Add(OneCard);
        }
        discard.Clear();
        Shuffle();
    }
}