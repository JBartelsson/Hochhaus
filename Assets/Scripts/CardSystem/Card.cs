using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Card: ICloneable
{
    private CustomColor colorReference;

    public CustomColor ColorReference => colorReference;

    public Card(CustomColor colorReference)
    {
        this.colorReference = colorReference;
        if (colorReference.IsPrimaryColor)
        this.RuntimePoints = this.colorReference.Points;
    }


    public override string ToString()
    {
        return colorReference.ColorName;
    }

    public void ChangeColor(CustomColor color)
    {
        this.colorReference = color;
    }

    public object Clone()
    {
        return new Card(colorReference);
    }

    public int RuntimePoints { get; set; }
}
