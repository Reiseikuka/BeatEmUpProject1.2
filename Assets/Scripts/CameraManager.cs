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
        private void Update()
        {
            Vector3 p = FindNearestPointOnLine(p1.position,p2.position, transform.position);
            p.z = transform.position.z;
            transform.position = p;

            //Vector3 tp = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 6);
            //tp.y = transform.position.y;
            //tp.z = transform.position.z;
            //transform.position = tp;
        }

        public Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 end, Vector2 point)
        {
            //Get heading
            Vector2 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            Vector2 lhs = point - origin;
            float dotP = Vector2.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return origin + heading * dotP;
        }
    }
}

