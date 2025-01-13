using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private CustomColor colorReference;

    public CustomColor ColorReference => colorReference;

    public Card(CustomColor colorReference)
    {
        this.colorReference = colorReference;
    }

    public override string ToString()
    {
        return colorReference.ColorName;
    }
}
