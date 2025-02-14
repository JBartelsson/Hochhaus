using System;
[Serializable]
public class EnvStats: ICloneable
{
    public float dollarPerStory = 100;
    public int HandSize = 2; 
    public int startDraws = 20; 
    public int startDollars = 0;  
    
    public EventHandler<EnvStats> UpdateStats;

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
}