using UnityEngine;

namespace UI
{
    public abstract class UIBase : MonoBehaviour, ISubscriber
    {
        private UIController _uiController;
        public UIController UIController => _uiController;


        public virtual void InitSubscriptions(UIController uiController)
        {
            _uiController = uiController;
        }

        public abstract void ResetSubscriptions();

    }
}