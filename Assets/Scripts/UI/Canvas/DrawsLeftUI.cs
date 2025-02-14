using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class DrawsLeftUI : UIBase
{
    [SerializeField] TextMeshProUGUI drawsLeftText;


    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);
        EnvironmentManager.Instance.GetActiveEnvironment().CardSystem.OnCardSystemChanged += CardSystemOnOnCardSystemChanged;
    }

    private void CardSystemOnOnCardSystemChanged(object sender, CardSystem e)
    {
        int drawsLeft = Mathf.Max(0, e.Draws);
        drawsLeftText.text = drawsLeft.ToString();
    }

    public override void ResetSubscriptions()
    {
        EnvironmentManager.Instance.GetActiveEnvironment().CardSystem.OnCardSystemChanged -= CardSystemOnOnCardSystemChanged;
    }
}
