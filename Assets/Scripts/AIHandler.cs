using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class AIHandler : MonoBehaviour
    {
        public UnitController unitController;

        public UnitController enemy;

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
        public float rotateDistance = 2;
        public float verticalThreshold = .1f;
        public float rotationThreshold = .5f;
        public float forceStopDistance = .3f;
        public bool forceStop;

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
            if (enemy == null)
                return;

          
            float delta = Time.deltaTime;
            Vector3 myPosition = transform.position;
            Vector3 enemyPosition = enemy.position;

            if (isInteracting || unitController.isDead)
            {
                unitController.UseRootMotion();
                return;
            }

            unitController.agent.enabled = true;

            if (deadTime > 0)
            {
                deadTime -= delta;
                return;
            }

            Vector3 directionToTarget = enemyPosition - myPosition;
            directionToTarget.Normalize();
            directionToTarget.z = 0;

            Vector3 targetPosition = enemyPosition +  (directionToTarget * -1) * attackDistance;
        
            bool isCloseToTargetPosition = IsCloseToTargetPosition(myPosition, targetPosition);
            bool closeToEnemy_NoVertical =  isCloseToEnemy_NoVertical(myPosition, enemyPosition); 
            bool closeToEnemy_General =     isCloseToEnemy_General(myPosition, enemyPosition);

            Collider[] colliders = Physics.OverlapSphere(transform.position, forceStopDistance);
            forceStop = false;

            foreach (var item in colliders)
            {
                AIHandler a = item.transform.GetComponentInParent<AIHandler>();
                if (a != null)
                {
                    if (a != this)
                    {
                        if (!a.forceStop)
                        {
                            forceStop = true;
                        }
                    }
                }
            }

            if (!forceStop && !closeToEnemy_NoVertical && !isCloseToTargetPosition)
            {
                unitController.agent.isStopped = false;
                unitController.agent.SetDestination(targetPosition);
                                
                Vector3 v = unitController.agent.velocity;
                v.z  = Mathf.Clamp(v.z, -verticalSpeed, verticalSpeed);
                unitController.agent.velocity = v;

               // NavMeshHit navHit;
               // if( NavMesh.Raycast(transform.position, 
               //     unitController.agent.desiredVelocity, out navHit, NavMesh.AllAreas))
               //     {
               //         unitController.agent.isStopped = true;
               //      }


                if (!closeToEnemy_General)
                {
                    unitController.HandleRotation(unitController.agent.velocity.x < 0);
                }else
                {
                    unitController.HandleRotation(directionToTarget.x < 0);
                }

            }
            else
            {

                unitController.agent.isStopped = true;
                unitController.HandleRotation(directionToTarget.x < 0);

                if (attackTime > 0)
                {
                    if (!forceStop)
                        attackTime -= delta;
                }else
                {
                    //unitController.PlayAction(unitController.defaultActions[0]);
                    attackTime = attackRate;    
                    deadTime = getDeadTimeRate;                    
                }
            }

            unitController.TickPlayer(delta, unitController.agent.desiredVelocity);
        }

        public bool IsCloseToTargetPosition(Vector3 p1, Vector3 p2)
        {
            float distance = Vector3.Distance(p1, p2);
            return distance < unitController.agent.stoppingDistance;
        }

        public bool isCloseToEnemy_NoVertical(Vector3 p1, Vector3 p2)
        {
            float dif = p1.z - p2.z;
            if (Mathf.Abs(dif) < verticalThreshold)
            {
                return Vector3.Distance(p1, p2) < attackDistance;
            } else
            {
                return false;
            }
            
        }
        public bool isCloseToEnemy_General(Vector3 p1, Vector3 p2)
        {
            return Vector3.Distance(p1, p2) < rotateDistance;
        }
    }
}

