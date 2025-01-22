using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;using UnityEngine.EventSystems;

public class BoardDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] LayerMask boardLayer;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if (!eventData.pointerDrag.TryGetComponent<SingleCardUI>(out SingleCardUI singleCardUI))
        {
            return;
        }
        Debug.Log("Drop has SingleCardUI");

        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out RaycastHit hit, 200f,boardLayer))
        {
            Debug.Log("Raycast Hit");

            if (hit.transform.TryGetComponent<BoardVisual>(out BoardVisual boardVisual))
            {
                BoardFieldVisual boardFieldVisual = boardVisual.ReturnBoardFieldVisual(hit.point );
                if (boardFieldVisual == null) return;
                EnvironmentManager.Instance.GetActiveEnvironment().AddCardColorToFace(singleCardUI.Card, boardFieldVisual.BoardField);
            }
        }
        
    }
}

