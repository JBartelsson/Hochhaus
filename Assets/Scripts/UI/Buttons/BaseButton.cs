using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;

namespace UI.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CustomText _buttonText;
        [SerializeField] private UnityEvent _unityAction;

        private void Start()
        {
            _button.onClick.AddListener(()=>_unityAction?.Invoke());
        }

    }
}