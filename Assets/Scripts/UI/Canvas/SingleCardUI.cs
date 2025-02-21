using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleCardUI : UIBase
{
    [SerializeField] private Image cardColorArea;
    private Card _card;

    public Card Card => _card;

    private Transform parentAfterDrag;
    
    //Texts
    [SerializeField] private TextMeshProUGUI pointsText;
    public void SetCardUI(Card card, UIController uiController)
    {
        _card = card;
        cardColorArea.color = card.AppartmentReference.AppartmentColor;
        pointsText.text = card.AppartmentReference.AppartmentName;
    }


    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
