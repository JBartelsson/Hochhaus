using System;
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
            if (target.transform.position.y >= 540f)
            {
                float y = Mathf.Lerp(transform.position.y, target.transform.position.y, Time.deltaTime * speed);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
        }
    }
}