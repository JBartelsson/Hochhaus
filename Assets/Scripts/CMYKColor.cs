using System;
using UnityEngine;

[Serializable]
public class CMYKColor
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

    public CMYKColor(float c, float m, float y, float k)
    {
        this.c = c;
        this.m = m;
        this.y = y;
        this.k = k;
    }
    
    public bool IsUnset()
    {
        return C==0 && M==0 && Y==0;
    }

    public CMYKColor Mix(CMYKColor other)
    {
        return new CMYKColor((this.c + other.c) / 2f, (this.m + other.m) / 2f, (this.y + other.y) / 2f, 0);
    }

    public override string ToString()
    {
        return $"C:{C}, M:{M}, Y:{Y}, K:{K}";
    }

    public bool IsBlack()
    {
        return this.c > 0 && this.m > 0 && this.y > 0;
    }
}