namespace UI
{
    public interface ISubscriber
    {
        public void InitSubscriptions(UIController uiController);
        public void ResetSubscriptions();
    }
}