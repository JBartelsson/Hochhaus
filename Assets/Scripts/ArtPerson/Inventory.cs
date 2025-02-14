using System;
using System.Collections.Generic;

namespace Art
{
    public class Inventory : IGameEventReceivable
    {
        List<ArtPerson> items = new ();

        public List<ArtPerson> Items => items;

        private ItemLibrary _itemLibrary;
        
        // Events to notify changes to the list
        public event EventHandler<Inventory> OnArtPersonAdded;    // Triggered when an ArtPerson is added
        public event EventHandler<Inventory> OnArtPersonRemoved;  // Triggered when an ArtPerson is removed
        public event EventHandler OnArtPersonsCleared;            // Triggered when all ArtPersons are cleared

        public Inventory(ItemLibrary itemLibrary)
        {
            this._itemLibrary = itemLibrary;
        }

        // Add an ArtPerson to the list
        public void AddArtPerson(ArtPerson artFunction)
        {
            if (artFunction == null)
            {
                throw new ArgumentNullException(nameof(artFunction), "ArtPerson cannot be null.");
            }

            if (!items.Contains(artFunction))
            {
                items.Add(artFunction);
                OnArtPersonAdded?.Invoke(this, this); // Raise event
            }
        }

        // Remove an ArtPerson from the list
        public void RemoveArtPerson(ArtPerson artFunction)
        {
            if (artFunction == null)
            {
                throw new ArgumentNullException(nameof(artFunction), "ArtPerson cannot be null.");
            }

            if (items.Remove(artFunction))
            {
                OnArtPersonRemoved?.Invoke(this, this); // Raise event
            }
        }

        // Clear all ArtPersons from the list
        public void ClearArtPersons()
        {
            items.Clear();
            OnArtPersonsCleared?.Invoke(this, EventArgs.Empty); // Raise event
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

        public Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            foreach (var item in items)
            {
                context = item.GameUpdate(gameEventType, context);
            }

            return context;
        }
    }
}