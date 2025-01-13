using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColor", menuName = "ColorSystem/CustomColor", order = 1)]
public class CustomColor : ScriptableObject
{
    [Header("Color Properties")]
    public string colorName; // Name of the color
    public Color rgbColor;   // RGB representation of the color

}