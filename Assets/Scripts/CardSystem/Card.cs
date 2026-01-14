using System;using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
[Serializable]
public class Card: EnvBase, ICloneable
{
    private AppartmentSO appartmentReference;
    private Item sender;
    private List<Item> mods = new List<Item>();
    
    public List<Item> Mods => mods;

    public Item Sender
    {
        get => sender;
        set => sender = value;
    }

    public AppartmentSO AppartmentReference => appartmentReference;

    public Card(AppartmentSO appartmentReference, Environment env, Item sender = null) : base(env)
    {
        this.appartmentReference = appartmentReference;
        RuntimePoints = this.appartmentReference.BasePoints;
        appartmentReference.Modifications.ForEach(mod =>
        {
            mods.Add(env.ItemLibrary.CreateItem(mod.ItemType));
            Debug.Log("Adding mod: " + mod + " to card: " + this);
        });
        this.sender = sender;
    }

    public override string ToString()
    {
        return appartmentReference.AppartmentName;
    }


    public object Clone()
    {
        return new Card(appartmentReference, env, null);
    }

    public float RuntimePoints { get; set; }
}
