using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomVerticalContainer : CustomUIComponent
{
    private VerticalLayoutGroup layoutGroup;
    
    
    public override void SetUp()
    {
        layoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    public override void Configure()
    {
        layoutGroup.padding = view.padding;
        layoutGroup.spacing = view.spacing;
    }
}