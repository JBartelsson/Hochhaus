using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorMixingDatabase", menuName = "ColorSystem/ColorMixingDatabase", order = 2)]
public class ColorMixingDatabase : ScriptableObject
{
    [Header("Mixing Rules")]
    public List<MixEntry> mixingEntries; // List of all color mixing rules

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
        foreach (var entry in mixingEntries)
        {
            // Check both (c1 + c2) and (c2 + c1) for symmetry
            if ((entry.color1 == c1 && entry.color2 == c2) || (entry.color1 == c2 && entry.color2 == c1))
            {
                return entry.result;
            }
        }
        return null; // No mix found
    }

    public Card MixCards(Card card1, Card card2)
    {
        CustomColor customColor = GetMixResult(card1.ColorReference, card2.ColorReference);
        return new Card(customColor);
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
                _instance = Resources.Load<ColorMixingDatabase>("Assets/ScriptableObjects/ColorDatabase/ColorMixingDatabase.asset");
                if (_instance == null)
                {
                    Debug.LogError("No ColorMixingDatabase found in Resources! Please create one and place it in a Resources folder.");
                }
            }
            return _instance;
        }
    }
}