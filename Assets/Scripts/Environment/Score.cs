
    using System;
    using UnityEngine;

    public class Score
    {
        // The current points and multiplier
        private int points;
        private float mult;

        // Events to notify UI or other systems
        public event EventHandler<int> OnPointsChanged;    // Triggered when points change
        public event EventHandler<float> OnMultiplierChanged; // Triggered when multiplier changes
        public event EventHandler<float> OnScoreChanged;     // Triggered when total score changes

        // Constructor to initialize default values
        public Score()
        {
            points = 0;
            mult = 1f; // Multiplier starts at 1 to avoid 0 scores
        }

        // Public getter for total score
        public float TotalScore => points * mult;

        // Add points to the current score
        public void AddPoints(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative points!");
                return;
            }

            points += amount;
            OnPointsChanged?.Invoke(this, points);
            OnScoreChanged?.Invoke(this, TotalScore); // Notify score update
        }
        
        // Remove points to the current score
        public void RemovePoints(int amount)
        {
            if (amount > 0)
            {
                Debug.LogWarning("Cannot remove negative points!");
                return;
            }

            points -= amount;
            OnPointsChanged?.Invoke(this, points);
            OnScoreChanged?.Invoke(this, TotalScore); // Notify score update
        }

        // Add to the multiplier
        public void AddMultiplier(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative multiplier!");
                return;
            }

            mult += amount;
            OnMultiplierChanged?.Invoke(this,mult);
            OnScoreChanged?.Invoke(this, TotalScore); // Notify score update
        }

        // Reset the score and multiplier
        public void ResetScore()
        {
            points = 0;
            mult = 1;

            // Notify all listeners about the reset
            OnPointsChanged?.Invoke(this, points);
            OnMultiplierChanged?.Invoke(this, mult);
            OnScoreChanged?.Invoke(this, TotalScore);
        }
    }
