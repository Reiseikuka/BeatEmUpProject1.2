using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AIHandler : MonoBehaviour
    {
        public UnitController unitController;

        public Transform target;

        public float minDeadTime;
        public float maxDeadTime;

        float getDeadTimeRate
        {
            get
            {
                float v = Random.Range(minDeadTime, maxDeadTime);
                return v;
            }
        }
        
        float deadTime;
        float attackTime = 1;

        public float attackRate = 1.5f;

        public float attackDistance = 2;

        float verticalSpeed
        {
            get
            {
                float v = unitController.agent.speed - .1f;
                return v;
            }
        }

        public bool isInteracting
        {
            get
            {
                return unitController.isInteracting;
            }
        }

        private void Start()
        {
            unitController.isAI = true;
        }

    
        private void Update()
        {
            if (target == null)
                return;

          
            float delta = Time.deltaTime;

            if (isInteracting || unitController.isDead)
            {
                unitController.UseRootMotion();
                return;
            }

            if (deadTime > 0)
            {
                deadTime -= delta;
                return;
            }

            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.Normalize();
            directionToTarget.z = 0;

            Vector3 targetPosition = target.position +  (directionToTarget * -1) * attackDistance;

            Debug.DrawRay(targetPosition + Vector3.up, Vector3.forward, Color.red);

            //unitController.agent.SetDestination(target.position);
            
            float distance = Vector3.Distance(transform.position, targetPosition);
            
            if (distance > unitController.agent.stoppingDistance)
            {
                unitController.agent.isStopped = false;
                unitController.agent.SetDestination(targetPosition);
                
            
                Vector3 v = unitController.agent.velocity;
                v.z  = Mathf.Clamp(v.z, -verticalSpeed, verticalSpeed);
                unitController.agent.velocity = v;

                if (Mathf.Abs(v.x) > .2f)
                {

                    unitController.HandleRotation(v.x < 0);
                }
            }
            else
            {
                unitController.agent.isStopped = true;
                unitController.HandleRotation(directionToTarget.x < 0);

                if (attackTime > 0)
                {
                    attackTime -= delta;
                }else
                {
                    unitController.PlayAction(unitController.actions[0]);
                    attackTime = attackRate;    
                    deadTime = getDeadTimeRate;                    
                }
            }

            unitController.TickPlayer(delta, unitController.agent.desiredVelocity);
        }
    }
}

