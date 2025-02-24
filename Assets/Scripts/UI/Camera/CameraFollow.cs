using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Camera
{
    public class CameraFollow : UIBase
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;

        public override void ResetSubscriptions()
        {
        }

        private void Update()
        {
            if (UnityEngine.Camera.main.WorldToScreenPoint(target.position).y > Screen.height / 2f)
            {
                transform.DOMove(new Vector3(transform.position.x, target.transform.position.y, transform.position.z),
                    .2f).SetEase(Ease.InOutSine);
            }
        }
    }
}