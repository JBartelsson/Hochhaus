using UnityEngine.UI;

public class CustomHorizontalContainer : CustomUIComponent
{
    private HorizontalLayoutGroup layoutGroup;
    
    public override void SetUp()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    public override void Configure()
    {
        layoutGroup.padding = view.padding;
        layoutGroup.spacing = view.spacing;
    }
}