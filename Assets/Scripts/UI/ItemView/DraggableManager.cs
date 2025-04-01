using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using CommandSystem.AnimationCommands;
using Items;
using UI;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Object = System.Object;

public abstract class DraggableManager : UIBase
{
    [SerializeField] protected DraggableItem itemPrefab; // Das Prefab für die Items
    [SerializeField] protected bool vertical;
    [SerializeField] protected bool inverted;
    [SerializeField] protected bool invertedRows;
    [SerializeField] protected int rows = 1;
    [SerializeField] protected bool reorderable;

    public bool Reorderable => reorderable;

    private HorizontalLayoutGroup layoutGroup; // Das Layout Group
    protected List<DraggableItem> items = new List<DraggableItem>();


    private float minSpacing = 0f; // Abstand wenn es zu viele Items gibt
    private float animationDuration = 0.3f;
    private bool firstUpdate = true;

    public List<DraggableItem> Items => items;

    //Get Iitem by Index
    public DraggableItem GetItemByIndex(int index)
    {
        foreach (var draggableItem in items)
        {
            if (draggableItem.Index == index)
            {
                return draggableItem;
            }
        }

        return null;
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

    protected DraggableItem AddDraggableItem(int index, bool isPinned = false)
    {
        DraggableItem itemObject = Instantiate(itemPrefab, transform);
        items.Add(itemObject);
        itemObject.name = "Item " + items.Count;
        itemObject.Init(this, index, isPinned);
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
        float spacingX;
        float spacingY;
        float prefixX = inverted ? -1 : 1;
        float prefixY = invertedRows ? -1 : 1;
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;
        if (!vertical)
        {
            spacingX = Mathf.Max(minSpacing, width * rows / (items.Count + 1)) ;
            spacingY = Mathf.Max(minSpacing, height / (rows));
        }
        else
        {
            spacingX = Mathf.Max(minSpacing, width / (rows));
            spacingY = Mathf.Max(minSpacing, height / (items.Count + 1));
        }


        // Falls die Items zu viele für den Platz sind, Spacing reduzieren (Überlappung)
        if (items.Count == 1)
        {
            spacingX = vertical ? height / 2f : width / 2f;
        }

        int rowItemAmount = Mathf.CeilToInt((float)items.Count / rows);
        int row = 0;
        int column = 1;
        for (int i = 0; i < items.Count; i++)
        {
            float spacingOffsetX = (column) * spacingX;
            float spacingOffsetY = (row) * spacingY;
            Vector3 newPosition;
            if (!vertical)
            {
                newPosition = new Vector3(prefixX * spacingOffsetX, prefixY * spacingOffsetY, 0);
            }
            else
            {
                newPosition = new Vector3(prefixX * spacingOffsetY, prefixY * spacingOffsetX, 0);

            }

            items[i].SetTargetPos(newPosition);
            items[i].MoveToTargetPos();
            if (column == rowItemAmount)
            {

                row++;
                column = 1;
            }
            else
            {
                column++;
            }
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

           
            return;
        }

        float minDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Index == draggableItem.Index) continue;
            if (items[i].IsPinned) continue;

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

    public void RemoveItem(int index)
    {
        for (var i = 0; i < items.Count; i++)
        {

            DraggableItem draggableItem = items[i];
            if (draggableItem.Index == index)
            {
                items.Remove(draggableItem);
                Destroy(draggableItem.gameObject);
                // UpdateItemPositions();
                foreach (var item in items)
                {
                    if (item.Index > index)
                    {
                        item.Index--;
                    }
                }

                return;
            }
        }
    }


    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}