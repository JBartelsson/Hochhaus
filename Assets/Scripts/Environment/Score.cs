
    using System;
    using UnityEngine;
    using Utility;
[Serializable]
    public class Score: ICloneable
    {
        // The current points and multiplier
        private float points;

        public float Points => points;

        public float Mult => mult;

        private float mult;

        // Events to notify UI or other systems
        public event EventHandler<Score> OnPointsChanged;    // Triggered when points change
        public event EventHandler<Score> OnMultiplierChanged; // Triggered when multiplier changes
        public event EventHandler<Score> OnScoreChanged; 
        
        // Triggered when total score changes

        // Constructor to initialize default values
        public Score()
        {
            points = 0;
            mult = 1f; // Multiplier starts at 1 to avoid 0 scores
        }

        // Public getter for total score
        public float StoryScore => points * mult;

        private float totalScore;
        public float TotalScore => totalScore;


        // Add points to the current score
        public void AddPoints(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative points!");
                return;
            }

            points += amount;
            OnPointsChanged?.Invoke(this, this);
            OnScoreChanged?.Invoke(this, this); // Notify score update
        }
        
        // Remove points to the current score
        public void RemovePoints(float amount)
        {
            if (amount > 0)
            {
                Debug.LogWarning("Cannot remove negative points!");
                return;
            }

            points -= amount;
            OnPointsChanged?.Invoke(this, this);
            OnScoreChanged?.Invoke(this, this); // Notify score update
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
            OnMultiplierChanged?.Invoke(this,this);
            OnScoreChanged?.Invoke(this, this); // Notify score update
        }

        public void CalculateScore()
        {
            totalScore += StoryScore;
            Debug.Log($"Painting is worth {points} x {mult} = {StoryScore} and total Score is {totalScore}");
        }

        // Reset the score and multiplier
        public void ResetScore()
        {
            points = 0;
            mult = 1f;
            Debug.Log("Resetting Score");
            // Notify all listeners about the reset
            OnPointsChanged?.Invoke(this, this);
            OnMultiplierChanged?.Invoke(this, this);
            OnScoreChanged?.Invoke(this, this);
        }

        public void ResetTotalScore()
        {
            totalScore = 0;
            ResetScore();
        }

        public void EndRoomBuilding()
        {
            CalculateScore();
            ResetScore();

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
