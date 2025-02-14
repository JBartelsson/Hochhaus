using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ArtistCardUI : UIBase
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetCardUI(Items.Item item)
    {
        nameText.text = item.ItemData.DisplayName;
        descriptionText.text = item.ItemData.Description;
    }

    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
