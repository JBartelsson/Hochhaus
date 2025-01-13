using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColor", menuName = "ColorSystem/CustomColor", order = 1)]
public class CustomColor : ScriptableObject
{
    [Header("Color Properties")]
    [SerializeField] private string colorName; // Name of the color
    [SerializeField] private Color rgbColor;   // RGB representation of the color

    public string ColorName => colorName;

    public Color RGBColor => rgbColor;
}