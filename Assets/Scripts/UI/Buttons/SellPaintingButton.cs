namespace UI.Buttons
{
    public class SellPaintingButton : BaseButton
    {
        protected override void Call()
        {
            EnvironmentManager.Instance.GetActiveEnvironment().SellCurrentPainting();
        }
    }
}