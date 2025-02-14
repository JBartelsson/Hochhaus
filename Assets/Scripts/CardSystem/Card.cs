using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Card: ICloneable
{
    private AppartmentSO appartmentReference;

    public AppartmentSO AppartmentReference => appartmentReference;

    public Card(AppartmentSO appartmentReference)
    {
        this.appartmentReference = appartmentReference;
        RuntimePoints = this.appartmentReference.Height;
    }

    public Card(AppartmentSO appartmentReference, float runtimePoints)
    {
        this.appartmentReference = appartmentReference;
        this.RuntimePoints = runtimePoints;
    }


    public override string ToString()
    {
        return appartmentReference.AppartmentName;
    }


    public object Clone()
    {
        return new Card(appartmentReference, this.RuntimePoints);
    }

    public float RuntimePoints { get; set; }
}
