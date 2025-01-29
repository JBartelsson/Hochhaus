using System;
using UnityEngine;
using Utility;

namespace WeekSystem
{
    public class WeekManager : MonoBehaviour, IInitHandler, IResetHandler
    {
        [SerializeField] WeekData weekData;
        private int currentWeek = 0;

        public int CurrentWeek => currentWeek;

        public EventHandler<WeekManager> OnWeekChanged;
        
        public void Init()
        {
            Reset();
        }

        public bool IsLevelSuccessful(Score roundScore)
        {
            return weekData.GetScoreAt(currentWeek) <= roundScore.TotalScore;
        }

        public void NextWeek()
        {
            currentWeek++;
            OnWeekChanged?.Invoke(this, this);

        }

        public int GetCurrentWeekInfo()
        {
            return weekData.GetScoreAt(currentWeek);
        }

        public void Reset()
        {
            currentWeek = 0;
            OnWeekChanged?.Invoke(this, this);
        }
    }
}