using System;
using UnityEngine;

[Serializable]
public class CYMKColor
{

    public float C
    {
        get => c;
        set => c = value;
    }

    public float M
    {
        get => m;
        set => m = value;
    }

    public float Y
    {
        get => y;
        set => y = value;
    }

    public float K
    {
        get => k;
        set => k = value;
    }
    [Range(0,1f)]
    [SerializeField] private float c;
    [Range(0,1f)]
    [SerializeField] private float m;
    [Range(0,1f)]
    [SerializeField] private float y;
    [Range(0,1f)]
    [SerializeField] private float k;

    public CYMKColor(float c, float m, float y, float k)
    {
        this.c = c;
        this.m = m;
        this.y = y;
        this.k = k;
    }
}