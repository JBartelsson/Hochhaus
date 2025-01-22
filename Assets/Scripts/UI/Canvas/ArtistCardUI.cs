using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtistCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void SetCardUI(Art.ArtPerson artPerson)
    {
        nameText.text = artPerson.ArtPersonData.DisplayName;
        descriptionText.text = artPerson.ArtPersonData.Description;
    }
}
