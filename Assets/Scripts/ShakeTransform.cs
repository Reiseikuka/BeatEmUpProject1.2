using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.Utilities
{
    public class ShakeTransform : MonoBehaviour
    {
        public int steps = 2;
        public float multiplier = 1;
        public float shakeSpeed = 6;
        public AnimationCurve shakeCurve;
        float shakeT;
        int _steps;
        bool isShaking;

        Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.localPosition;
        }

        public void Shake()
        {
            shakeT = 0;
            _steps = steps;
            isShaking = true;
        }

        public bool debugShake;

        private void Update()
        {
            if (debugShake)
            {
                debugShake = false;
                Shake();
            }

            if (!isShaking)
                return;

            bool isDone = false;

            shakeT += Time.deltaTime * shakeSpeed;
            if (shakeT > 1)
            {
                shakeT = 1;
                isDone = true;
            }

            float y = shakeCurve.Evaluate(shakeT);
            Vector3 lp = startPosition; 
            lp.y += y;
            transform.localPosition = lp;

            if (isDone)
            {
                _steps--;
                if(_steps <= 0)
                {
                    isShaking = false;
                }
            }

        }
    }
}

