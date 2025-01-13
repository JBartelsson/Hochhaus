using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleCardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image cardColorArea;
    private Card _card;
    private UIController uiController;

    public Card Card => _card;

    private Transform parentAfterDrag;
    [SerializeField] private Transform parentDuringDrag;
    public void SetCardUI(Card card, Transform canvas, UIController uiController)
    {
        _card = card;
        cardColorArea.color = card.ColorReference.RGBColor;
        parentDuringDrag = canvas.transform;
        this.uiController = uiController;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        transform.SetParent(parentDuringDrag);
        parentAfterDrag = transform.parent;
        cardColorArea.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        cardColorArea.raycastTarget = true;

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        SingleCardUI droppedCardUI = droppedObject.GetComponent<SingleCardUI>();
        uiController.TryMixCards(droppedCardUI, this);
    }
}
