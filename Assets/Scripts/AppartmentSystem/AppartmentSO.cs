using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewAppartment", menuName = "ApparmentSystem/ApparmentSO", order = 1)]
public class AppartmentSO : ScriptableObject
{
    [FormerlySerializedAs("height")] [Header("Base Stats")] [SerializeField]
    private float basePoints;
    public float BasePoints => basePoints;
    
    [SerializeField] private string appartmentName;
    public string AppartmentName => appartmentName;
    
    [SerializeField] private Color appartmentColor;
    
    public Color AppartmentColor => appartmentColor;

}