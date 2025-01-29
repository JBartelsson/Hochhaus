using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorMixingDatabase", menuName = "ColorSystem/ColorMixingDatabase", order = 2)]
public class ColorMixingDatabase : ScriptableObject
{
    [SerializeField] private CustomColor black; // List of all color mixing rules

    [Header("Mixing Rules")] public List<MixEntry> mixingEntries;

    [System.Serializable]
    public class MixEntry
    {
        public CustomColor color1; // First color in the mix
        public CustomColor color2; // Second color in the mix
        public CustomColor result; // Resulting color
    }

    // Function to get a mix result from the database
    private CustomColor GetMixResult(CustomColor c1, CustomColor c2)
    {
        if (c1.RuntimeCMYK.IsUnset()) c1.RuntimeCMYK = c1.CmykColor;
        if (c2.RuntimeCMYK.IsUnset()) c2.RuntimeCMYK = c2.CmykColor;

        if (c1.RuntimeCMYK.Mix(c2.RuntimeCMYK).IsBlack()) return black;
    
        foreach (var entry in mixingEntries)
        {
            // Check both (c1 + c2) and (c2 + c1) for symmetry
            if ((entry.color1 == c1 && entry.color2 == c2) || (entry.color1 == c2 && entry.color2 == c1))
            {
                entry.result.RuntimeCMYK = c1.RuntimeCMYK.Mix(c2.RuntimeCMYK);
                return entry.result;
            }
        }

        return null; // No mix found
    }

    public Card MixCards(Card card1, Card card2)
    {
        CustomColor customColor = GetMixResult(card1.ColorReference, card2.ColorReference);
        if (customColor == null) return null;
        Card newCard = new Card(customColor);
        if (customColor != black)
        {
            newCard.RuntimePoints = card1.RuntimePoints + card2.RuntimePoints;
            Debug.Log($"{newCard} runtime Points are {newCard.RuntimePoints}");
        }
        else
        {
            newCard.RuntimePoints = black.Points;
        }

        Debug.Log($"New Card {newCard.RuntimePoints}");
        return newCard;
    }

    // Singleton instance
    private static ColorMixingDatabase _instance;

    /// <summary>
    /// Access the singleton instance of the ColorMixingDatabase.
    /// </summary>
    public static ColorMixingDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                // Load the database if it hasn't been assigned
                _instance = Resources.Load<ColorMixingDatabase>(
                    "Assets/ScriptableObjects/ColorDatabase/ColorMixingDatabase.asset");
                if (_instance == null)
                {
                    Debug.LogError(
                        "No ColorMixingDatabase found in Resources! Please create one and place it in a Resources folder.");
                }
            }

            return _instance;
        }
    }
}