using System;
using UnityEngine;
using Utility;

namespace WeekSystem
{
    public class WeekManager : MonoBehaviour, IInitHandler, IResetHandler
    {
        [SerializeField] WeekData weekData;
        [SerializeField] private Environment env;
        private int currentWeek = 0;

        public int CurrentWeek => currentWeek;

        public EventHandler<WeekManager> OnWeekChanged;
        
        public void Init()
        {
            Reset();
        }

        public bool IsLevelSuccessful(PlayerStats roundScore)
        {
            return weekData.GetScoreAt(currentWeek) <= roundScore.Stats.ScoreTotal;
        }

        public void NextWeek()
        {
            currentWeek++;
            env.CardSystem.Reset();
            env.CardSystem.DrawFullHand();
            env.PlayerStats.Reset();
            env.TowerManager.Reset();
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