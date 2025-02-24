using System;
using UnityEngine.Serialization;

[Serializable]
public class EnvStats: ICloneable
{
    public float dollarPerStory = 100;
    public int HandSize = 2;
    public int Fabric;
    public int DrawCost = 1;
    
    public EventHandler<EnvStats> UpdateStats;

    public EnvStats()
    {
        dollarPerStory = 0;
        HandSize = 0;
        Fabric = 0;
        DrawCost = 0;
    }

    public void AddToDollarPerStory(float amount)
    {
        dollarPerStory += amount;
        UpdateStats?.Invoke(this, this);
    }

    public void SubtractFromDollarPerStory(float amount)
    {
        dollarPerStory -= amount;
        UpdateStats?.Invoke(this, this);
    }

    public void AddToHandSize(int amount)
    {
        HandSize += amount;
        UpdateStats?.Invoke(this, this);
    }

    public void SubtractFromHandSize(int amount)
    {
        HandSize -= amount;
        UpdateStats?.Invoke(this, this);
    }

    public void ResetStats()
    {
        dollarPerStory = 100;
        HandSize = 2;
        UpdateStats?.Invoke(this, this);
    }
   
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public bool CanDraw()
    {
        return Fabric - DrawCost >= 0;
    }
}