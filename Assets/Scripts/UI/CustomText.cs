using System.Collections; using System.Collections.Generic; using UnityEngine;
using UnityEngine.UI; using TMPro;
using UI;
using UnityEngine.Events;

public class CustomText : CustomUIComponent
{
    public UI.UIStyle style;
    public TextSO textData;
    private TextMeshProUGUI text;

    public override void SetUp()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        text.color = theme.GetTextColor(style);
        text.font = textData.font;
        text.fontSize = textData.size;
    }
    
    public void OnClick()
    {
    }
}


