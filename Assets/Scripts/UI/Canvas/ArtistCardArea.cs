using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class ArtistCardArea : UIBase, ISubscriber
{
    List<ArtistCardUI> artistCardUis = new List<ArtistCardUI>();

    [FormerlySerializedAs("cardUIPrefab")] [SerializeField] private ArtistCardUI artistCardUIPrefab;
    [SerializeField] private Transform cardsSpawnTarget;
    
    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.Inventory.OnArtPersonAdded += InventoryOnOnArtPersonAdded;
        env.Inventory.OnArtPersonRemoved += InventoryOnOnArtPersonRemoved;
        env.Inventory.OnArtPersonsCleared += InventoryOnOnArtPersonsCleared;
    }

    private void InventoryOnOnArtPersonsCleared(object sender, EventArgs e)
    {
        UpdateArtistCards(null);
    }

    private void InventoryOnOnArtPersonRemoved(object sender, Inventory inventory)
    {
        UpdateArtistCards(inventory);

    }

    private void InventoryOnOnArtPersonAdded(object sender, Inventory inventory)
    {
        UpdateArtistCards(inventory);

    }

    private void UpdateArtistCards(Inventory inventory)
    {
        foreach (var singleCardUI in artistCardUis)
        {
            Destroy(singleCardUI.gameObject);
        }
        artistCardUis.Clear();
        if (inventory == null) return;
        for (int i = 0; i < inventory.GetCount(); i++)
        {
            ArtistCardUI singleCardUI = Instantiate(artistCardUIPrefab, cardsSpawnTarget);
            singleCardUI.SetCardUI(inventory.Items[i]);
            artistCardUis.Add(singleCardUI);
        }
    }

    public override void ResetSubscriptions()
    {
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.Inventory.OnArtPersonAdded -= InventoryOnOnArtPersonAdded;
        env.Inventory.OnArtPersonRemoved -= InventoryOnOnArtPersonRemoved;
        env.Inventory.OnArtPersonsCleared -= InventoryOnOnArtPersonsCleared;
    }
}
