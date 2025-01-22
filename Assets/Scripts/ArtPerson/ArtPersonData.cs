using UnityEngine;

namespace Art
{
    [CreateAssetMenu(fileName = "ArtPerson", menuName = "ArtPerson", order = 1)]

    public class ArtPersonData : ScriptableObject
    {
        [Header("General Settings")]

        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] ArtPersonType artPersonType;

        [Header("Effect Settings")] 
        [SerializeField] private float pointEffect;
        [SerializeField] private float multEffect;
        [SerializeField] private float pointGain;
        [SerializeField] private float multGain;
        [SerializeField] private CustomColor effectColor1;
        [SerializeField] private CustomColor effectColor2;
        [SerializeField] private CustomColor effectColor3;
        [SerializeField] private float floatValue;


        public string DisplayName => displayName;

        public string Description => description;

        public ArtPersonType ArtPersonType => artPersonType;

        public float PointEffect => pointEffect;

        public float MultEffect => multEffect;

        public float PointGain => pointGain;

        public float MultGain => multGain;

        public CustomColor EffectColor1 => effectColor1;

        public CustomColor EffectColor2 => effectColor2;

        public CustomColor EffectColor3 => effectColor3;
        public float FloatValue => floatValue;
    }
}
