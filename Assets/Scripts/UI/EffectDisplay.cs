using System;
using System.Collections;
using System.Collections.Generic;
using CommandSystem.Commands;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplay : UIBase
{
    public Image Image => image;

    public TextMeshProUGUI Text => text;

    public CanvasGroup CanvasGroup => canvasGroup;

    //image as serialized field
    [SerializeField] private Image image;

    //text as serialized field
    [SerializeField] private TextMeshProUGUI text;

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