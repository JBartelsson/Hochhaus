using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ScoreUI : UIBase
{
    [SerializeField] TextMeshProUGUI scoreText;

    public TextMeshProUGUI ScoreText => scoreText;


    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnAddedAppartment += TowerManagerOnOnAddedAppartment;
    }

    private void TowerManagerOnOnAddedAppartment(TowerManager towerManager, int index, TowerRoom arg3)
    {
        

    }

    private void ScoreOnOnScoreChanged(object sender, PlayerStats e)
    {
    }


    public override void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().TowerManager.OnAddedAppartment -= TowerManagerOnOnAddedAppartment;


    }
}
