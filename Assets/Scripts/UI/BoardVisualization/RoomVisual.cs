using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class RoomVisual : UIBase
{
    [SerializeField] private SpriteRenderer sprite;
    

    private TowerRoom _towerRoom;

    private Vector3 ogScale;

    public float Height
    {
        get
        {
            return transform.localScale.y * UI.VisualSettings.roomPxPerUnit;
        }
    }


    

    public TowerRoom TowerRoom => _towerRoom;


    public void Init(TowerRoom towerRoom, float scale = 1f)
    {
        this._towerRoom = towerRoom;
        sprite.color = towerRoom._PlacedCard.CardCopy.AppartmentReference.AppartmentColor;
        ogScale = transform.localScale;
        this.transform.localScale = new Vector2(transform.localScale.x, towerRoom._PlacedCard.CardCopy.AppartmentReference.BasePoints);
        _towerRoom.OnUpdate += TowerRoomOnOnUpdate;
    }

    public bool IsBaseHeight()
    {
        Debug.Log($"Room visual height is {Height} and card height is {_towerRoom._PlacedCard.CardCopy.AppartmentReference.BasePoints * ogScale.y}");
        return Mathf.Approximately(Height, _towerRoom._PlacedCard.CardCopy.AppartmentReference.BasePoints * ogScale.y * UI.VisualSettings.roomPxPerUnit);
    }

    private void TowerRoomOnOnUpdate(object sender, EventArgs e)
    {
    }


    public override void ResetSubscriptions()
    {
        
    }
}