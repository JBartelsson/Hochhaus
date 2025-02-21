using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using CommandSystem.AnimationCommands;
using Items;
using UI;
using Unity.VisualScripting;
using Object = System.Object;

public abstract class DraggableManager : UIBase
{
    [SerializeField] protected DraggableItem itemPrefab; // Das Prefab für die Items
    [SerializeField] protected bool vertical;
    [SerializeField] protected bool inverted;
    private HorizontalLayoutGroup layoutGroup; // Das Layout Group
    protected List<DraggableItem> items = new List<DraggableItem>();


    private float minSpacing = 0f; // Abstand wenn es zu viele Items gibt
    private float animationDuration = 0.3f;
    private bool firstUpdate = true;

    public List<DraggableItem> Items => items;

    //Get Iitem by Index
    public DraggableItem GetItem(int index)
    {
        return items[index];
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();
        // Layout Group einmal die Positionen setzen lassen
        UpdateItemPositions();
    }

    private void Update()
    {
        if (firstUpdate)
        {
            UpdateItemPositions();

            firstUpdate = false;
        }
    }

    private void DisableLayoutGroup()
    {
        layoutGroup.enabled = false; // Deaktivieren, damit wir eigene Positionen setzen können
        UpdateItemPositions();
    }

    protected DraggableItem AddItem()
    {
        DraggableItem itemObject = Instantiate(itemPrefab, transform);
        items.Add(itemObject);
        itemObject.name = "Item " + items.Count;
        itemObject.Init(this, items.Count - 1);
        UpdateItemPositions();
        return itemObject;
    }

    public void UpdateItemPositions()
    {
        if (items.Count == 0)
        {
            return;
        }

        float totalSpace;
        float prefix = inverted ? -1 : 1;
        if (!vertical)
        {
            
         totalSpace = GetComponent<RectTransform>().rect.width;
        }
        else
        {
            totalSpace = GetComponent<RectTransform>().rect.height;
        }
        Debug.Log(totalSpace);
        float itemWidth = items[0].RectTransform.rect.width;
        Debug.Log(itemWidth);


        // Falls die Items zu viele für den Platz sind, Spacing reduzieren (Überlappung)
        float spacing = Mathf.Max(minSpacing, totalSpace / (items.Count + 1));
        if (items.Count == 1)
        {
            spacing = totalSpace / 2f;
        }

        for (int i = 0; i < items.Count; i++)
        {
            float spacingOffset = (i + 1) * spacing;
            Vector3 newPosition;
            if (!vertical)
            {
                newPosition = new Vector3(prefix * spacingOffset, 0, 0);
            }
            else
            {
                newPosition = new Vector3(0, prefix * spacingOffset, 0);
            }
            items[i].SetTargetPos(newPosition);
            items[i].MoveToTargetPos();
        }
    }


    public void CalculateItemDrag(DraggableItem draggableItem, bool isDragging = false)
    {
        if (!isDragging)
        {
            UIReorderItemsCommand reorderItemsCommand = new UIReorderItemsCommand(UI);
            Debug.Log($"ui: {UI}");
            UI.AnimationCommandInvoker.QueueAnimation(reorderItemsCommand);
            UI.AnimationCommandInvoker.StartPlaying();

            draggableItem.transform.SetSiblingIndex(draggableItem.Index);
            draggableItem.MoveToTargetPos();
            return;
        }

        float minDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Index == draggableItem.Index) continue;

            float distance = Vector3.Distance(draggableItem.RectTransform.localPosition,
                items[i].TargetPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        if (minDistance >=
            Vector3.Distance(draggableItem.RectTransform.localPosition, GetTargetPosition(draggableItem)))
        {
            return;
        }

        if (closestIndex != -1)
        {
            SwapItems(draggableItem, items[closestIndex], isDragging);
        }
    }

    private void SwapItems(DraggableItem d1, DraggableItem d2, bool isDragging = false)
    {
        Debug.Log("Swapping " + d1.Index + " with " + d2.Index);
        int index1 = d1.Index;
        d1.Index = d2.Index;
        d2.Index = index1;
        Vector3 targetPos1 = GetTargetPosition(d1);
        d1.SetTargetPos(d2.TargetPosition);
        d2.SetTargetPos(targetPos1);
        d2.MoveToTargetPos();
        d2.transform.SetSiblingIndex(d2.Index);
    }

    public void Clear()
    {
        foreach (var draggableItem in items)
        {
            Destroy(draggableItem.gameObject);
        }
        items.Clear();
    }

    public Vector3 GetTargetPosition(DraggableItem item)
    {
        return item.TargetPosition;
    }

    public Vector3 GetTargetPosition(int itemIndex)
    {
        return items[itemIndex].TargetPosition;
    }


    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}