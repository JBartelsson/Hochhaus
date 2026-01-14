using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace UI
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "CustomUI/ThemeSO", fileName = "Theme")]
    public class ThemeSO : ScriptableObject
    {
        [Header("Primary")] 
        public Color primary_bg;
        public Color primary_text;
        [Header("Secondary")] 
        public Color secondary_bg;
        public Color secondary_text;
        [Header("Tertiary")] 
        public Color tertiary_bg;
        public Color tertiary_text;
        [Header("Effects")] public Color points;
        public Color mult;
        public Color fabric;
        [Header("Other")] 
        public Color disable;
        
       
        
        public Color GetBackgroundColor(UIStyle style)
        {
            switch (style)
            {
                case UIStyle.Primary:
                    return primary_bg;
                case UIStyle.Secondary:
                    return secondary_bg;
                case UIStyle.Tertiary:
                    return tertiary_bg;
                default:
                    return Color.clear; // Return a default color if the style is not recognized
            }
        }

        public Color GetTextColor(UIStyle style)
        {
            switch (style)
            {
                case UIStyle.Primary:
                    return primary_text;
                case UIStyle.Secondary:
                    return secondary_text;
                case UIStyle.Tertiary:
                    return tertiary_text;
                case UIStyle.Score_Points:
                    return points;
                case UIStyle.Score_Mult:
                    return mult;
                case UIStyle.Score_Fabric:
                    return fabric;
                default:
                    return Color.clear; // Return a default color if the style is not recognized
            }
        }
    }
}