using System;
using UnityEngine;

namespace UI
{
    public abstract class UIBase : MonoBehaviour, ISubscriber
    {
        private UIController _ui;
        public UIController UI => _ui;

       


        public virtual void InitSubscriptions(UIController uiController)
        {
            _ui = uiController;
        }

        public abstract void ResetSubscriptions();

    }
}