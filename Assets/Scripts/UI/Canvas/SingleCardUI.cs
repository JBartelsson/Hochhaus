using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleCardUI : UIBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image cardColorArea;
    private Card _card;
    private UIController uiController;
    private int originalSiblingIndex;

    public Card Card => _card;

    private Transform parentAfterDrag;
    [SerializeField] private Transform parentDuringDrag;
    
    //Texts
    [SerializeField] private TextMeshProUGUI pointsText;
    public void SetCardUI(Card card, Transform canvas, UIController uiController)
    {
        _card = card;
        cardColorArea.color = card.AppartmentReference.AppartmentColor;
        parentDuringDrag = canvas.transform;
        pointsText.text = card.AppartmentReference.AppartmentName;
        this.uiController = uiController;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        

    }

    public void OnDrop(PointerEventData eventData)
    {
     
        
    }

    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
