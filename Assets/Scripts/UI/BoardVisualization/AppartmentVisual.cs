using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class AppartmentVisual : UIBase
{
    [SerializeField] private SpriteRenderer sprite;

    private TowerRoom _towerRoom;

    public float Height
    {
        get
        {
            return transform.localScale.y * sprite.transform.localScale.y;
        }
    }


    

    public TowerRoom TowerRoom => _towerRoom;


    public void Init(TowerRoom towerRoom, float scale = 1f)
    {
        this._towerRoom = towerRoom;
        Debug.Log(towerRoom._PlacedCard.CardCopy.AppartmentReference.AppartmentColor);
        sprite.color = towerRoom._PlacedCard.CardCopy.AppartmentReference.AppartmentColor;
        this.transform.localScale = new Vector2(transform.localScale.x, towerRoom._PlacedCard.CardCopy.AppartmentReference.Height * transform.localScale.y);
        _towerRoom.OnUpdate += TowerRoomOnOnUpdate;
    }

    private void TowerRoomOnOnUpdate(object sender, EventArgs e)
    {
    }


    public override void ResetSubscriptions()
    {
        
    }
}