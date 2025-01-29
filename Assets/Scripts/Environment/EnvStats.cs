using System;
[Serializable]
public class EnvStats: ICloneable
{
    public int AmountOfStrokes;
    public int AmountOfDiscards;
    public int AmountOfPaintings = 4;
    public int PaintingStartSize = 5;
    public int HandSize;
    
    public EventHandler<EnvStats> UpdateStats;

    // Add function (field-wise)
    public void Add(
        int? amountOfStrokes = null, 
        int? amountOfDiscards = null, 
        int? amountOfPaintings = null, 
        int? paintingStartSize = null, 
        int? handSize = null)
    {
        if (amountOfStrokes.HasValue) AmountOfStrokes += amountOfStrokes.Value;
        if (amountOfDiscards.HasValue) AmountOfDiscards += amountOfDiscards.Value;
        if (amountOfPaintings.HasValue) AmountOfPaintings += amountOfPaintings.Value;
        if (paintingStartSize.HasValue) PaintingStartSize += paintingStartSize.Value;
        if (handSize.HasValue) HandSize += handSize.Value;

        // Trigger the UpdateStats event
        UpdateStats?.Invoke(this, this);
    }

    // Subtract function (field-wise)
    public void Subtract(
        int? amountOfStrokes = null, 
        int? amountOfDiscards = null, 
        int? amountOfPaintings = null, 
        int? paintingStartSize = null, 
        int? handSize = null)
    {
        if (amountOfStrokes.HasValue) AmountOfStrokes -= amountOfStrokes.Value;
        if (amountOfDiscards.HasValue) AmountOfDiscards -= amountOfDiscards.Value;
        if (amountOfPaintings.HasValue) AmountOfPaintings -= amountOfPaintings.Value;
        if (paintingStartSize.HasValue) PaintingStartSize -= paintingStartSize.Value;
        if (handSize.HasValue) HandSize -= handSize.Value;

        // Trigger the UpdateStats event
        UpdateStats?.Invoke(this, this);
    }

    // Reset function
    public void Reset(EnvStats startStats)
    {
        AmountOfStrokes = startStats.AmountOfStrokes;
        AmountOfDiscards = startStats.AmountOfDiscards;
        AmountOfPaintings = startStats.AmountOfPaintings;
        PaintingStartSize = startStats.PaintingStartSize;
        HandSize = startStats.HandSize;

        // Trigger the UpdateStats event
        UpdateStats?.Invoke(this, this);
    }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}