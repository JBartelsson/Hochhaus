using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using Utility;
using Random = UnityEngine.Random;

[Serializable]
public class CardSystem : IResetHandler, IInitHandler
{
    List<Card> drawPile;
    List<Card> discardPile;
    private List<Card> fullDeck;
    private List<Card> hand;

    public List<Card> DrawPile => drawPile;

    public List<Card> DiscardPile => discardPile;

    public List<Card> FullDeck => fullDeck;


    public List<Card> Hand
    {
        get => hand;
        set => hand = value;
    }

    private Environment _env;

    public event EventHandler<CardSystem> OnCardSystemChanged;


    public CardSystem(Environment env)
    {
        drawPile = new List<Card>();
        hand = new List<Card>();
        discardPile = new List<Card>();
        fullDeck = new List<Card>();
        _env = env;
        Init();
    }

    // Shuffle the Current Deck of cards
    public void Shuffle()
    {
        for (int i = drawPile.Count - 1; i > 0; --i)
        {
            int j = Random.Range(0, i + 1);
            Card card = drawPile[j];
            drawPile[j] = drawPile[i];
            drawPile[i] = card;
        }
    }


    // Return a list of drawn Cards from deck
    public void Draw(int numberToDraw = 1, bool putBack = false)
    {
        if (numberToDraw > drawPile.Count)
            numberToDraw = drawPile.Count;
        List<Card> drawnCards = new List<Card>();
        drawnCards.Add(drawPile[0]);
        if (!putBack)
        {
            for (int i = 0; i < numberToDraw; ++i)
            {
                drawPile.RemoveAt(0);
            }
        }

        hand.AddRange(drawnCards);
        OnCardSystemChanged?.Invoke(this, this);
    }

    public void DrawRandom(int numberToDraw = 1)
    {
        if (numberToDraw > drawPile.Count)
            numberToDraw = drawPile.Count;
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < numberToDraw; ++i)
        {
            DrawRandomCardCommand drawRandomCardCommand = new DrawRandomCardCommand(_env, null);
            _env.CommandInvoker.Execute(drawRandomCardCommand);
        }
    }

    private void PutHandBackToDeck()
    {
    }

    public void DrawFullHand(bool putBack = true)
    {
        // Debug.Log(_env.PlayerStats.Stats.HandSize);
        for (int i = hand.Count; i < _env.PlayerStats.Stats.HandSize; i++)
        {
            DrawRandom(1);
        }
    }

    public void DrawNewHand()
    {
        hand.Clear();
        DrawFullHand();
    }

    // Return a list of drawn Cards from discard
    public List<Card> DrawDiscard(int numberToDraw = 1)
    {
        if (numberToDraw > discardPile.Count)
            numberToDraw = discardPile.Count;
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < numberToDraw; ++i)
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

    public void ReturnDiscardPile(bool shuffle = false)
    {
        foreach (Card OneCard in discardPile)
        {
            drawPile.Add(OneCard);
        }

        discardPile.Clear();
        if (shuffle)
            Shuffle();
    }

    public void Init()
    {
        StartDeck startDeck = _env.EnvSettings.StartDeck;
        foreach (var colorEntry in startDeck.Deck)
        {
            for (int i = 0; i < colorEntry.Amount; i++)
            {
                AddCard(new Card(colorEntry.Color, _env));
            }
        }

        Reset();
    }

    public void AddCard(Card card)
    {
        fullDeck.Add(card);
        OnCardSystemChanged?.Invoke(this, this);
    }


    public void BuildHand()
    {
        if (!_env.PlayerStats.Stats.CanDraw())
        {
            return;
        }
        
        UpdateGameStatCommand updateGameStatCommand = new UpdateGameStatCommand(_env, null, PlayerStats.PlayerStat.FABRIC, -_env.PlayerStats.Stats.DrawCost, true);
        _env.CommandInvoker.Execute(updateGameStatCommand);
        
        Debug.Log("HANDS UPDATE YEAH!");
        UpdateGameStatCommand updateHands = new UpdateGameStatCommand(_env, null, PlayerStats.PlayerStat.HANDS_TOTAL, 1);
        _env.CommandInvoker.Execute(updateHands);
        
        ReorderItemsCommand reorderItemsCommand = new ReorderItemsCommand(_env, null);
        _env.CommandInvoker.Execute(reorderItemsCommand);
        List<Card> handCopy = new List<Card>(_env.CardSystem.Hand);
        foreach (var card in handCopy)
        {
            CreateRoomCommand createRoomCommand = new CreateRoomCommand(_env, card);
            Context newCtx = new Context(_env)
            {
                NextCommand = createRoomCommand
            };
            Debug.Log($"NEXT CMMMAND IS: {newCtx.NextCommand}");
            _env.GameUpdate(Environment.GameStateType.BUILD_ROOM_START, newCtx);
            _env.CommandInvoker.Execute(createRoomCommand);
            RemoveCardFromHandCommand removeCardFromHandCommand = new RemoveCardFromHandCommand(_env, null, card);
            _env.CommandInvoker.Execute(removeCardFromHandCommand);
        }

        PutHandBackToDeckCommand putHandBackToDeckCommand = new PutHandBackToDeckCommand(_env, null);
        _env.CommandInvoker.Execute(putHandBackToDeckCommand);
        

        DrawNewHand();
    }



    public override string ToString()
    {
        string s = "";
        s += "FULL DECK:";
        foreach (var card in fullDeck)
        {
            s += card.ToString() + ", ";
        }

        s += "--DRAW PILE:";
        foreach (var card in drawPile)
        {
            s += card.ToString() + ", ";
        }

        s.Remove(s.Length - 2, 2);

        s += "--HAND:";
        foreach (var card in hand)
        {
            s += card.ToString() + ", ";
        }

        s.Remove(s.Length - 2, 2);

        s += "--DISCARD PILE:";
        foreach (var card in discardPile)
        {
            s += card.ToString() + ", ";
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