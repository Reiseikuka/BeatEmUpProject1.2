using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class AIHandler : MonoBehaviour
    {
        public UnitController unitController;

        public Transform target;
        
        float attackTime = 1;

        public float attackRate = 1.5f;

        public float attackDistance = 2;

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

          
            if (isInteracting || unitController.isDead)
            {
                unitController.UseRootMotion();
                return;
            }


            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.Normalize();
            directionToTarget.z = 0;

            Vector3 targetPosition = target.position +  (directionToTarget * -1) * attackDistance;

            //unitController.agent.SetDestination(target.position);
            
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > attackDistance)
            {
                unitController.agent.isStopped = false;
                unitController.agent.SetDestination(targetPosition);
                unitController.HandleRotation(unitController.agent.velocity.x < 0);
            }
            else
            {
                unitController.agent.isStopped = true;
                unitController.HandleRotation(directionToTarget.x < 0);

                if (attackTime > 0)
                {
                    attackTime -= Time.deltaTime;
                }else
                {
                    unitController.PlayAction(unitController.actions[0]);
                    attackTime = attackRate;                        
                }
            }

            unitController.TickPlayer(Time.deltaTime, unitController.agent.desiredVelocity);
        }
    }
}

