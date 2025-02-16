using UnityEngine.UI;

public class CustomGridContainer : CustomUIComponent
{
    private GridLayoutGroup layoutGroup;
    
    public override void SetUp()
    {
        layoutGroup = GetComponent<GridLayoutGroup>();
    }

    public override void Configure()
    {
        layoutGroup.padding = view.padding;
    }
}