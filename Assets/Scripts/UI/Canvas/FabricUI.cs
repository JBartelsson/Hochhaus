using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class FabricUI : UIBase
{
    [FormerlySerializedAs("drawsLeftText")] [SerializeField] TextMeshProUGUI fabricText;
    [SerializeField] TextMeshProUGUI drawCostText;
    [SerializeField] TextMeshProUGUI drawIncreaseText;

    public TextMeshProUGUI FabricText => fabricText;

    public TextMeshProUGUI DrawCostText => drawCostText;
    
    public TextMeshProUGUI DrawIncreaseText => drawIncreaseText;

    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
