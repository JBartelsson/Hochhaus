using UI;
using UnityEngine;

public abstract class CustomUIComponent : MonoBehaviour
{
    [SerializeField] protected ViewSO view;
    [SerializeField] protected ThemeSO theme;
    private void Awake()
    {
        Init();
    }

    public abstract void SetUp();
    public abstract void Configure();
        
    private void Init()
    {
        SetUp();
        Configure();
    }

    private void OnValidate()
    {
        Init();
    }
}