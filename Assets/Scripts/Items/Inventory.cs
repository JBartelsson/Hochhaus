using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class Inventory : IGameEventReceivable
    {
        List<Item> items = new();
        public bool IsInShop { get; set; }


        public List<Item> Items
        {
            get => items;
            set => items = value;
        }

        private ItemLibrary _itemLibrary;


        public Inventory(ItemLibrary itemLibrary, bool isShop = false)
        {
            this._itemLibrary = itemLibrary;
            IsInShop = isShop;
        }

        // Add an ArtPerson to the list
        public void AddItem(Item item)
        {
            item.IsInShop = IsInShop;
            items.Add(item);
        }

        // Remove an ArtPerson from the list
        public void RemoveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "ArtPerson cannot be null.");
            }

            if (items.Remove(item))
            {
            }
        }

        // Clear all ArtPersons from the list
        public void ClearItems()
        {
            items.Clear();
        }

        // Get the count of ArtPersons
        public int GetCount()
        {
            return items.Count;
        }

        // Retrieve a copy of the current list
        // public List<ArtPerson> GetArtPersons()
        // {
        //     return new List<ArtPerson>(artPersons);
        // }

        public Context GameUpdate(Environment.GameStateType gameStateType, Context context)
        {
            foreach (var item in items)
            {
                context.CurrentItem = item;
                context = item.GameUpdate(gameStateType, context);
                Debug.Log($"Scoring context on {item}");
            }

            return context;
        }
    }
}