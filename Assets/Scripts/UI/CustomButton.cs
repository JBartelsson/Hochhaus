using System.Collections; using System.Collections.Generic; using UnityEngine;
using UnityEngine.UI; using TMPro;
using UI;
using UnityEngine.Events;

public class CustomButton : CustomUIComponent
{
    public UI.UIStyle style;
    public UnityEvent onClick;
    private Button button;
    private TextMeshProUGUI buttonText;

    public override void SetUp()
    {
        button = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Configure()
    {
        ColorBlock cb = button.colors;
        cb.normalColor = theme.GetBackgroundColor(style);
        button.colors = cb;
        buttonText.color = theme.GetTextColor(style);
        
    }
    
    public void OnClick()
    {
        onClick.Invoke();
    }
}


