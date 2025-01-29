using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private void Start()
        {
            _button.onClick.AddListener(Call);
        }

        protected abstract void Call();

    }
}