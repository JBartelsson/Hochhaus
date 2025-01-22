using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColor", menuName = "ColorSystem/CustomColor", order = 1)]
public class CustomColor : ScriptableObject
{
    [Header("Color Properties")]
    [SerializeField] private string colorName; // Name of the color
    [SerializeField] private Color rgbColor;
    [SerializeField] private bool isPrimaryColor = false;// RGB representation of the color


    [SerializeField] private CMYKColor cmykColor;

    [SerializeField] private int points = 0;

    public bool IsPrimaryColor => isPrimaryColor;
    public int Points => points;

    public CMYKColor CmykColor => cmykColor;

    private CMYKColor runtimeCMYK;// RGB representation of the color

    public CMYKColor RuntimeCMYK
    {
        get => runtimeCMYK;
        set => runtimeCMYK = value;
    }

    public string ColorName => colorName;

    public Color RGBColor => rgbColor;
}