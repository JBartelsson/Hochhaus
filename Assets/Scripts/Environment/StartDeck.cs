using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewColor", menuName = "ColorSystem/StartDeck", order = 1)]
public class StartDeck : ScriptableObject
{
    [Serializable]
    public class ColorEntry
    {
        public CustomColor Color;
        public int Amount;
    }
    [SerializeField] private List<ColorEntry> _deck;

    public List<ColorEntry> Deck => _deck;
}