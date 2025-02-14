using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ArtistCardUI : UIBase
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetCardUI(Art.ArtPerson artPerson)
    {
        nameText.text = artPerson.ArtPersonData.DisplayName;
        descriptionText.text = artPerson.ArtPersonData.Description;
    }

    public override void ResetSubscriptions()
    {
        throw new System.NotImplementedException();
    }
}
