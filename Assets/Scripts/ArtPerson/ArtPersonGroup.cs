using System;
using System.Collections.Generic;

namespace Art
{
    public class ArtPersonGroup : IGameEventReceivable
    {
        List<ArtPerson> artPersons = new ();

        public List<ArtPerson> ArtPersons => artPersons;

        private ArtPersonLibrary artPersonLibrary;
        
        // Events to notify changes to the list
        public event EventHandler<ArtPersonGroup> OnArtPersonAdded;    // Triggered when an ArtPerson is added
        public event EventHandler<ArtPersonGroup> OnArtPersonRemoved;  // Triggered when an ArtPerson is removed
        public event EventHandler OnArtPersonsCleared;            // Triggered when all ArtPersons are cleared

        public ArtPersonGroup(ArtPersonLibrary artPersonLibrary)
        {
            this.artPersonLibrary = artPersonLibrary;
        }

        // Add an ArtPerson to the list
        public void AddArtPerson(ArtPerson artFunction)
        {
            if (artFunction == null)
            {
                throw new ArgumentNullException(nameof(artFunction), "ArtPerson cannot be null.");
            }

            if (!artPersons.Contains(artFunction))
            {
                artPersons.Add(artFunction);
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

            if (artPersons.Remove(artFunction))
            {
                OnArtPersonRemoved?.Invoke(this, this); // Raise event
            }
        }

        // Clear all ArtPersons from the list
        public void ClearArtPersons()
        {
            artPersons.Clear();
            OnArtPersonsCleared?.Invoke(this, EventArgs.Empty); // Raise event
        }

        // Get the count of ArtPersons
        public int GetCount()
        {
            return artPersons.Count;
        }

        // Retrieve a copy of the current list
        // public List<ArtPerson> GetArtPersons()
        // {
        //     return new List<ArtPerson>(artPersons);
        // }

        public Context GameUpdate(Environment.GameEventType gameEventType, Context context)
        {
            foreach (var artPerson in artPersons)
            {
                context = artPerson.GameUpdate(gameEventType, context);
            }

            return context;
        }
    }
}