using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewBoss", menuName = "WeekSystem/Boss", order = 1)]
public class Boss : ScriptableObject
{
    [SerializeField] private float targetScoreMultiplier = 1f;

}
