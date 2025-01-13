using Unity.VisualScripting;
using UnityEngine;
//Color Utility
public class ColorUtility : MonoBehaviourSingleton<ColorUtility>
{
    [SerializeField] ColorMixingDatabase colorMixingDatabase;
    
    public static CustomColor MixColors(CustomColor c1, CustomColor c2)
    {
        foreach (var entry in Instance.colorMixingDatabase.mixingEntries)
        {
            // Check both (c1 + c2) and (c2 + c1) for symmetry
            if ((entry.color1 == c1 && entry.color2 == c2) || (entry.color1 == c2 && entry.color2 == c1))
            {
                return entry.result;
            }
        }
        return null; // No mix found
    }
}