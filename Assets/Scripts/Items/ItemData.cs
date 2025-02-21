using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "ItemData", order = 1)]

    public class ItemData : ScriptableObject
    {
        
        
        [Header("General Settings")]

        [SerializeField] private string displayName;

        [SerializeField] private Sprite image;



        [SerializeField] private string description;
        [FormerlySerializedAs("artPersonType")] [SerializeField] ItemType itemType;
        public string DisplayNameEditor
        {
            set => displayName = value;
        }
        public Sprite Image => image;

        public ItemType ItemTypeEditor
        {
            set => itemType = value;
        }
    [SerializeField] private Rarity rarity;

    public Rarity Rarity => rarity;

    [Header("Effect Settings")] 
        [SerializeField] private float pointEffect;
        [SerializeField] private float multEffect;
        [SerializeField] private float pointGain;
        [SerializeField] private float multGain;
        [SerializeField] private float multMult;


        [FormerlySerializedAs("effectColor1")] [SerializeField] private AppartmentSO effectRoom1;
        [SerializeField] private AppartmentSO effectColor2;
        [SerializeField] private AppartmentSO effectColor3;
        [SerializeField] private float floatValue;
        [SerializeField] private int intValue;

        public int INTValue => intValue;
        public float MultMult => multMult;


        public string DisplayName => displayName;

        public string Description => description;

        public ItemType ItemType => itemType;

        public float PointEffect => pointEffect;

        public float MultEffect => multEffect;

        public float PointGain => pointGain;

        public float MultGain => multGain;

        public AppartmentSO EffectRoom1 => effectRoom1;

        public AppartmentSO EffectColor2 => effectColor2;

        public AppartmentSO EffectColor3 => effectColor3;
        public float FloatValue => floatValue;
    }
}
