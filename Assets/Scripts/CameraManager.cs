using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class CameraManager : MonoBehaviour
    {
        public Transform target;

        public Transform p1;
        //Waypoint 1
        public Transform p2;
        //Waypoint 2

        public bool isFollowing;

        public static CameraManager singleton;

        public void Awake()
        {
            singleton = this;
        }

        private void Update()
        {
            if (isFollowing)
            {
                Vector3 p = GetClosestPointOnFiniteLine(target.position, p1.position,p2.position);
                p.z = transform.position.z;
                transform.position = p;
            }
        }

        // For finite lines:
        Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
        {
            Vector3 line_direction = line_end - line_start;
            float line_length = line_direction.magnitude;
            line_direction.Normalize();
            float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
            return line_start + line_direction * project_length;
        }

    }

}

