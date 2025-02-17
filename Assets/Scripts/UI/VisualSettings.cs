using UnityEngine;

namespace UI
{
    
        [CreateAssetMenu(fileName = "NewVisualSettings", menuName = "CustomUI/VisualSettings", order = 1)]
        public class VisualSettings : ScriptableObject
        {
            public float roomPxPerUnit;
            public float roomPxWidth;

            public float RoomPxPerUnit => roomPxPerUnit;

            public float RoomPxWidth => roomPxWidth;
        }
}