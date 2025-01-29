using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeekSystem", menuName = "WeekSystem/WeekSystem", order = 1)]
public class WeekData : ScriptableObject
{
    [SerializeField] private List<int> levelTargetScores;
    [SerializeField] private List<Boss> availableBosses;
    
    // Gets the score at a specified index
    public int GetScoreAt(int index)
    {
        if (levelTargetScores != null && index >= 0 && index < levelTargetScores.Count)
        {
            return levelTargetScores[index];
        }

        Debug.LogWarning("Invalid index for getting score.");
        return -1; // Return a default value or handle error
    }

    // Returns the total number of scores
    public int GetScoreCount()
    {
        return levelTargetScores?.Count ?? 0;
    }
}
