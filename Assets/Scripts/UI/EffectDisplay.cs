using System;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EffectDisplay : UIBase
{
    public Image Image => image;

    public TextMeshProUGUI PointsText => pointsText;
    public TextMeshProUGUI MultText => multText;
    public TextMeshProUGUI FabricText => fabricText;
    
    public TextMeshProUGUI FabricMultText => fabricMultText;

    public CanvasGroup CanvasGroup => canvasGroup;

    //image as serialized field
    [SerializeField] private Image image;

    //text as serialized field
    [FormerlySerializedAs("pointsText")] [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI multText;
    [SerializeField] private TextMeshProUGUI fabricText;
    [SerializeField] private TextMeshProUGUI fabricMultText;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject imageContainer;

    public GameObject ImageContainer => imageContainer;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        image.sprite = null;
    }

    public override void ResetSubscriptions()
    {
    }


    
}