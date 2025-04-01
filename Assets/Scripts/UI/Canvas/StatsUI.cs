using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using WeekSystem;

public class StatsUI : UIBase, ISubscriber
{
   [SerializeField] TextMeshProUGUI canvasText;
   [SerializeField] TextMeshProUGUI weekText;
    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);

    }

    private void OnWeekChanged(object sender, WeekManager e)
    {
        weekText.text = (e.CurrentWeek + 1).ToString();
    }

    private void UpdateStats(object sender, EnvStats e)
    {
    }

    public override void ResetSubscriptions()
    {


    }
}
