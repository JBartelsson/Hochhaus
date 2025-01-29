using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using WeekSystem;

public class StatsUI : MonoBehaviour, ISubscriber
{
   [SerializeField] TextMeshProUGUI canvasText;
   [SerializeField] TextMeshProUGUI weekText;
    public void InitSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().RoundStats.Stats.UpdateStats += UpdateStats;
        EnvironmentManager.Instance.GetActiveEnvironment().WeekManager.OnWeekChanged += OnWeekChanged;
    }

    private void OnWeekChanged(object sender, WeekManager e)
    {
        weekText.text = (e.CurrentWeek + 1).ToString();
    }

    private void UpdateStats(object sender, EnvStats e)
    {
        canvasText.text = e.AmountOfPaintings.ToString();
    }

    public void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().RoundStats.Stats.UpdateStats -= UpdateStats;
        EnvironmentManager.Instance.GetActiveEnvironment().WeekManager.OnWeekChanged -= OnWeekChanged;


    }
}
