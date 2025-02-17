using TMPro;
using UI;
using UnityEngine;

[CreateAssetMenu(fileName = "NewText", menuName = "CustomUI/TextSO", order = 1)]
public class TextSO : ScriptableObject
{
    //Add Color Theme
    public ThemeSO theme;
    public TMP_FontAsset font;
    public float size;
}