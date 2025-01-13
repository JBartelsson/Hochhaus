using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Environment : MonoBehaviour
{
    [SerializeField] ColorMixingDatabase colorMixingDatabase;
    [SerializeField] private EnvSettings envSettings;
    
    private CardSystem _cardSystem;
    
    //Events
    public event EventHandler<CardSystem> OnCardSystemChanged; 

    private void Start()
    {
        _cardSystem = new CardSystem(colorMixingDatabase, envSettings);
        _cardSystem.Shuffle();
        _cardSystem.DrawFullHand();
        OnCardSystemChanged?.Invoke(this, _cardSystem);
    }

    public void MixHandCards(Card card1, Card card2)
    {
        Debug.Log(card1);
        Debug.Log(card2);
        Debug.Log(card1.ColorReference);
        Debug.Log(card2.ColorReference);

        _cardSystem.MixHandCards(card1, card2);
        OnCardSystemChanged?.Invoke(this, _cardSystem);

    }
}