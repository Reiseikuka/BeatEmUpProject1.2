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
            Vector3 p = FindNearestPointOnLine(target.position, p1.position,p2.position);
            p.z = transform.position.z;
            transform.position = p;

            //Vector3 tp = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 6);
            //tp.y = transform.position.y;
            //tp.z = transform.position.z;
            //transform.position = tp;
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


        // For infinite lines:
        Vector3 GetClosestPointOnInfiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
        {
            return line_start + Vector3.Project(point - line_start, line_end - line_start);
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

