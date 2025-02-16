
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
        
        public float XMult => xmult;

        private float xmult;

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
            xmult = 1f;
        }

        // Public getter for total score
        public float RoomScore => points * mult * xmult;

        private float totalScore;
        public float TotalScore => totalScore;


        public void UpdateScore(ScoreType scoreType, float value)
        {
            switch (scoreType)
            {
                case ScoreType.POINTS:
                    AddPoints(value);
                    break;
                case ScoreType.MULT:
                    AddMultiplier(value);
                    break;
                case ScoreType.xMULT:
                    MultiplyXMult(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scoreType), scoreType, null);
            }
        }
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
        
        public void AddXMult(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative xmult!");
                return;
            }

            xmult += amount;
            OnScoreChanged?.Invoke(this, this); // Notify score update
        }

        public void MultiplyXMult(float factor)
        {
            if (factor < 0)
            {
                Debug.LogWarning("Cannot multiply by a negative factor!");
                return;
            }

            xmult *= factor;
            OnScoreChanged?.Invoke(this, this); // Notify score update
        }

        public void CalculateScore()
        {
            totalScore += RoomScore;
            Debug.Log($"Painting is worth {points} x {mult} = {RoomScore} and total Score is {totalScore}");
        }

        // Reset the score and multiplier
        public void ResetScore()
        {
            points = 0;
            mult = 1f;
            xmult = 1f;
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

        public enum ScoreType
        {
            POINTS, MULT, xMULT
        }

        public override string ToString()
        {
            return $"Points: {points}, Multiplier: {mult}, xMultiplier: {xmult}, Room Score: {RoomScore}, Total Score: {totalScore}";
        }
    }
