using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

public class Environment : MonoBehaviour, IInitHandler
{
    [SerializeField] ColorMixingDatabase colorMixingDatabase;
    [SerializeField] private EnvSettings envSettings;
    [SerializeField] private BoardManager boardManager;

    public BoardManager BoardManager
    {
        get => boardManager;
        set => boardManager = value;
    }

    private CardSystem _cardSystem;

    public CardSystem CardSystem => _cardSystem;

    //Events

    public void StartEnvironment()
    {
        _cardSystem.Shuffle();
        _cardSystem.DrawFullHand();
        
        boardManager.InitBoard();
    }

    public void MixHandCards(Card topCard, Card bottomCard)
    {
        Debug.Log(topCard);
        Debug.Log(bottomCard);
        Debug.Log(topCard.ColorReference);
        Debug.Log(bottomCard.ColorReference);

        _cardSystem.MixHandCards(topCard, bottomCard);
    }

    public void AddCardColorToFace(Card card, BoardField boardField)
    {
        boardField.AddCard(card);
        _cardSystem.DiscardCard(card);
    }

    public void Init()
    {
        _cardSystem = new CardSystem(colorMixingDatabase, envSettings);
    }
}