using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemData", order = 1)]

    public class ItemData : ScriptableObject
    {
        [Header("General Settings")]

        [SerializeField] private string displayName;


        [SerializeField] private string description;
        [FormerlySerializedAs("artPersonType")] [SerializeField] ItemType itemType;
        public string DisplayNameEditor
        {
            set => displayName = value;
        }

        public ItemType ItemTypeEditor
        {
            set => itemType = value;
        }

        [Header("Effect Settings")] 
        [SerializeField] private float pointEffect;
        [SerializeField] private float multEffect;
        [SerializeField] private float pointGain;
        [SerializeField] private float multGain;
        [SerializeField] private AppartmentSO effectColor1;
        [SerializeField] private AppartmentSO effectColor2;
        [SerializeField] private AppartmentSO effectColor3;
        [SerializeField] private float floatValue;
        [SerializeField] private int intValue;

        public int INTValue => intValue;


        public string DisplayName => displayName;

        public string Description => description;

        public ItemType ItemType => itemType;

        public float PointEffect => pointEffect;

        public float MultEffect => multEffect;

        public float PointGain => pointGain;

        public float MultGain => multGain;

        public AppartmentSO EffectColor1 => effectColor1;

        public AppartmentSO EffectColor2 => effectColor2;

        public AppartmentSO EffectColor3 => effectColor3;
        public float FloatValue => floatValue;
    }
}
