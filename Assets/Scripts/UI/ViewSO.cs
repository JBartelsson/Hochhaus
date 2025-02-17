using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//write a scriptable object ViewSO with a RectOffset padding and spacing flaot

[CreateAssetMenu(fileName = "NewView", menuName = "CustomUI/ViewSO", order = 1)]
public class ViewSO : ScriptableObject
{
    //Add Color Theme
    public RectOffset padding;
    public float spacing;
}