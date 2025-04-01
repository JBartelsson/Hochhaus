using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using CommandSystem.AnimationCommands;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private RectTransform rectTransform;
    private Transform parentTransform;

    private Vector3 dragOffset;
    private Vector3 lastMousePosition;
    [SerializeField] private float rotationFactor = 2.5f; // Skaliert die Rotation
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float maxRotation = 25f;
    private DraggableManager _draggableManager;
    public RectTransform RectTransform => rectTransform;

    private Vector2 targetPosition;
    
    public bool IsPinned { get; set; }

    public Vector2 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }

    private int index;
    private int originalIndex;

    public int OriginalIndex
    {
        get => originalIndex;
        set => originalIndex = value;
    }

    public int Index
    {
        get => index;
        set => index = value;
    }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(DraggableManager draggableManager, int index, bool isPinned = false)
    {
        this._draggableManager = draggableManager;
        this.index = index;
        originalIndex = index;
        IsPinned = isPinned;
    }

    public void Start()
    {
        parentTransform = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        Debug.Log("Start DRAG!");

        dragOffset = transform.position - GetMouseWorldPosition();
        lastMousePosition = GetMouseWorldPosition();

        transform.SetAsLastSibling(); // Damit das Dragging-Item oben bleibt
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Bewege das Item mit Offset, nicht direkt zur Maus
        transform.position = GetMouseWorldPosition() + dragOffset;

        // Geschwindigkeit berechnen
        Vector3 velocity = (GetMouseWorldPosition() - lastMousePosition) / Time.deltaTime;
        lastMousePosition = GetMouseWorldPosition();

        // Rotation richtungsbasiert anpassen
        float rotationZ = Mathf.Clamp(velocity.x * rotationFactor, -maxRotation, maxRotation);
        // Statt direkt zu setzen, sanft animieren
        transform.DOKill();
        transform.DORotate(new Vector3(0, 0, rotationZ), smoothTime, RotateMode.Fast);
        if (_draggableManager.Reorderable)
            _draggableManager.CalculateItemDrag(this, true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggableManager.Reorderable)

            _draggableManager.CalculateItemDrag(this);
        Debug.Log("END DRAg");
        transform.SetSiblingIndex(Index);
        MoveToTargetPos();
    }

    public void SetTargetPos(Vector2 _targetPosition)
    {
        this.targetPosition = _targetPosition;
    }

    public void MoveToTargetPos()
    {
        // Sanfte Bewegung zur endgültigen Position
        rectTransform.DOLocalMove(targetPosition, 0.3f).SetEase(Ease.OutQuad);

        // Sanfte Rotation zurück auf 0
        transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutQuad);
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0; // Falls 3D-Umgebung, Z anpassen
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}