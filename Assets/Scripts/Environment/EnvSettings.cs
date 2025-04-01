using System;
using UnityEngine;
using Utility;

[CreateAssetMenu(fileName = "EnvSettings", menuName = "EnvSettings", order = 1)]
[Serializable]
public class EnvSettings : ScriptableObject, IInitHandler
{
        public StartDeck StartDeck;
        public EnvStats startEnvStats;
        public void Init()
        {
                startEnvStats.Init();
        }
}