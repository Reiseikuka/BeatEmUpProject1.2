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
        public Phase myPhase;
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
        public float stopDistance = 1;
        public float rotateDistance = 2;
        
        public float verticalThreshold = .1f;
        public float rotationThreshold = .5f;

        public float forceStopDistance = .3f;
        public bool forceStop;


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
            unitController.onDeath = UnRegisterMe;

            if (myPhase != null)
            {
                myPhase.RegisterUnit(this);
            }
        }

        void UnRegisterMe()
        {
            if (myPhase != null)
            {
                myPhase.UnRegisterUnit(this);
            }
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
                unitController.UseRootMotion(delta);
                return;
            }
            

            if (deadTime > 0)
            {
                unitController.debugState = "Waiting"  + "\n" + deadTime.ToString();
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

            Vector3 targetDirection = Vector3.zero;


            if (!forceStop && !closeToEnemy_NoVertical && !isCloseToTargetPosition)
            {
                
                targetDirection = targetPosition - transform.position;
                targetDirection.Normalize();
                Debug.DrawLine(transform.position, targetDirection);
                
                unitController.debugState = "Moving to Target";

                if (!closeToEnemy_General)
                {
                    unitController.HandleRotation(targetDirection.x < 0);
                }
                else
                {
                    unitController.HandleRotation(directionToTarget.x < 0); 
                }

            }
            else
            {
                unitController.HandleRotation(directionToTarget.x < 0);

                if (attackTime > 0)
                {
                    unitController.debugState = "Waiting to Attack" + "\n" + attackTime.ToString();


                    if (!forceStop)
                        attackTime -= delta;
                }else
                {
                    unitController.debugState = "<color=red>Attacking</color>";

                    unitController.PlayAction(unitController.actionDataHolder.actions[0].actions[0]);
                    attackTime = attackRate;    
                    deadTime = getDeadTimeRate;                    
                }
            }

            unitController.TickPlayer(delta, targetDirection);
        }

        public bool IsCloseToTargetPosition(Vector3 p1, Vector3 p2)
        {
            float distance = Vector3.Distance(p1, p2);
            return distance < stopDistance;
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

