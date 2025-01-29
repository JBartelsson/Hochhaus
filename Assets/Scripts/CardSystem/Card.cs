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

    public Card(CustomColor colorReference, int runtimePoints)
    {
        this.colorReference = colorReference;
        this.RuntimePoints = runtimePoints;
    }


    public override string ToString()
    {
        return colorReference.ColorName;
    }

    public void ChangeColor(CustomColor color)
    {
        this.colorReference = color;
        this.RuntimePoints = this.colorReference.Points;
    }

    public object Clone()
    {
        return new Card(colorReference, this.RuntimePoints);
    }

    public int RuntimePoints { get; set; }
}
