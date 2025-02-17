using System;using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
[Serializable]
public class Card: ICloneable
{
    private AppartmentSO appartmentReference;
    private Item sender;

    public Item Sender
    {
        get => sender;
        set => sender = value;
    }

    public AppartmentSO AppartmentReference => appartmentReference;

    public Card(AppartmentSO appartmentReference, Item sender = null)
    {
        this.appartmentReference = appartmentReference;
        RuntimePoints = this.appartmentReference.BasePoints;
        this.sender = sender;
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
