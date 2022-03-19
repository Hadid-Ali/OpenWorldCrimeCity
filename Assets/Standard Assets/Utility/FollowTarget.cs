using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);

        public Vector3 rotOffset;
        public bool lookAt = false;


        private void LateUpdate()
        {
            transform.position = target.position + offset;

            if(this.lookAt)
            {
                this.transform.LookAt(this.target.transform);
                Vector3 v = this.transform.eulerAngles;
                v += this.rotOffset;
                this.transform.eulerAngles = v;
            }
        }
    }
}
