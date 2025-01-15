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

    public void StartEnvironment()
    {
        _cardSystem = new CardSystem(colorMixingDatabase, envSettings);
        _cardSystem.Shuffle();
        _cardSystem.DrawFullHand();
        OnCardSystemChanged?.Invoke(this, _cardSystem);
    }

    public void MixHandCards(Card topCard, Card bottomCard)
    {
        Debug.Log(topCard);
        Debug.Log(bottomCard);
        Debug.Log(topCard.ColorReference);
        Debug.Log(bottomCard.ColorReference);

        _cardSystem.MixHandCards(topCard, bottomCard);
        OnCardSystemChanged?.Invoke(this, _cardSystem);

    }
}