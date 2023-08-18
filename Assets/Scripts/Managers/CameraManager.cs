using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SA
{
    public class CameraManager : MonoBehaviour
    {
        public Transform target;

		//public Transform p1;
		//public Transform p2;

        public bool isFollowing;
        public float speed = 6;

        public static CameraManager singleton;

        public float shakeTime = 0.5f;
        public float shakeAmplitude = 0.025f;
        float _shakeLife = 0;

		public void Awake()
		{
            singleton = this;

            noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

          
		}

        public void FollowStatus(bool status)
        {
            isFollowing = status;
        }


		private void Update()
		{
            float delta = Time.deltaTime;

            if (isFollowing)
            {
                transform.position = Vector3.Lerp(transform.position, target.position, delta * 6);
            }

            if (_shakeLife > 0)
            {
                _shakeLife -= delta;
            }
            else
            { 
                noise.m_AmplitudeGain = 0;
            }
        }

        Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
        {
            Vector3 line_direction = line_end - line_start;
            float line_length = line_direction.magnitude;
            line_direction.Normalize();
            float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
            return line_start + line_direction * project_length;
        }

        public CinemachineVirtualCamera vcam;
         CinemachineBasicMultiChannelPerlin noise;
        public void ShakeCamera()
        {
            noise.m_AmplitudeGain = shakeAmplitude;
            _shakeLife = shakeTime;
        }
    }
}
