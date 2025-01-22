using System;
using System.Collections;
using System.Collections.Generic;
using Art;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class ArtistCardArea : MonoBehaviour, ISubscriber
{
    List<ArtistCardUI> artistCardUis = new List<ArtistCardUI>();

    [FormerlySerializedAs("cardUIPrefab")] [SerializeField] private ArtistCardUI artistCardUIPrefab;
    [SerializeField] private Transform cardsSpawnTarget;
    
    public void InitSubscriptions()
    {
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.ArtPersonGroup.OnArtPersonAdded += ArtPersonGroupOnOnArtPersonAdded;
        env.ArtPersonGroup.OnArtPersonRemoved += ArtPersonGroupOnOnArtPersonRemoved;
        env.ArtPersonGroup.OnArtPersonsCleared += ArtPersonGroupOnOnArtPersonsCleared;
    }

    private void ArtPersonGroupOnOnArtPersonsCleared(object sender, EventArgs e)
    {
        UpdateArtistCards(null);
    }

    private void ArtPersonGroupOnOnArtPersonRemoved(object sender, ArtPersonGroup artPersonGroup)
    {
        UpdateArtistCards(artPersonGroup);

    }

    private void ArtPersonGroupOnOnArtPersonAdded(object sender, ArtPersonGroup artPersonGroup)
    {
        UpdateArtistCards(artPersonGroup);

    }

    private void UpdateArtistCards(ArtPersonGroup artPersonGroup)
    {
        foreach (var singleCardUI in artistCardUis)
        {
            Destroy(singleCardUI.gameObject);
        }
        artistCardUis.Clear();
        if (artPersonGroup == null) return;
        for (int i = 0; i < artPersonGroup.GetCount(); i++)
        {
            ArtistCardUI singleCardUI = Instantiate(artistCardUIPrefab, cardsSpawnTarget);
            singleCardUI.SetCardUI(artPersonGroup.ArtPersons[i]);
            artistCardUis.Add(singleCardUI);
        }
    }

    public void ResetSubscriptions()
    {
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.ArtPersonGroup.OnArtPersonAdded -= ArtPersonGroupOnOnArtPersonAdded;
        env.ArtPersonGroup.OnArtPersonRemoved -= ArtPersonGroupOnOnArtPersonRemoved;
        env.ArtPersonGroup.OnArtPersonsCleared -= ArtPersonGroupOnOnArtPersonsCleared;
    }
}
