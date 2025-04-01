//
//     using System;
//     using System.Collections.Generic;
//     using System.Linq;
//     using Items;
//     using UnityEngine;
//     using Utility;
// [Serializable]
//     public class Score: ICloneable
//     {
//         private readonly float _baseMultiplier = 1f;
//         // The current points and multiplier
//         private float points;
//         
//
//         
//         
//
//         
//
//         // Triggered when total score changes
//
//         // Constructor to initialize default values
//         public Score()
//         {
//             points = 0;
//         }
//
//         // Public getter for total score
//         
//
//         private float totalScore;
//         public float TotalScore => totalScore;
//
//
//         
//         
//         public void CalculateScore()
//         {
//             totalScore += RoomScore;
//             //log all the score modifiers
//             Debug.Log($"Room is worth {RoomScore} and total Score is {totalScore}");
//         }
//
//         // Reset the score and multiplier
//         public void ResetRoomScore()
//         {
//             scoreModifiers.Clear();
//             Debug.Log("Resetting Score");
//             // Notify all listeners about the reset
//         }
//
//         public void ResetTotalScore()
//         {
//             totalScore = 0;
//             ResetRoomScore();
//         }
//
//         public object Clone()
//         {
//             Score newScore = (Score)this.MemberwiseClone();
//             newScore.scoreModifiers = new List<StatModifier>(scoreModifiers);
//             return newScore;
//         }
//
//
//         public override string ToString()
//         {
//             return $"Points: , Room Score: {RoomScore}, Total Score: {totalScore}";
//         }
//     }
