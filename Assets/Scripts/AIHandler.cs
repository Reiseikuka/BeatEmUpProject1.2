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

        private void Update()
        {
            if (target == null)
                return;

            //unitController.agent.SetDestination(target.position);
            
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > attackDistance)
            {
                unitController.agent.isStopped = false;
                unitController.agent.SetDestination(target.position);
            }
            else
            {
                unitController.agent.isStopped = true;

                if (attackTime > 0)
                {
                    attackTime -= Time.deltaTime;
                }else
                {
                    if(!unitController.isInteracting)
                    {
                        unitController.PlayAnimation("attack 1");
                        attackTime = attackRate;                        
                    }
                }
            }
            unitController.TickPlayer(Time.deltaTime, unitController.agent.desiredVelocity);
        }
    }
}

