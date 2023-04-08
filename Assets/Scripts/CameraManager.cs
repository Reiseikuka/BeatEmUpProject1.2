using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CameraManager : MonoBehaviour
    {
        public Transform target;

        private void Update()
        {
            Vector3 tp = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 6);
            tp.y = transform.position.y;
            tp.z = transform.position.z;
            transform.position = tp;
        }
    }
}

