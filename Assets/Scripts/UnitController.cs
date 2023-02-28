using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class UnitController : MonoBehaviour
    {
       public int health = 100;
       public  NavMeshAgent agent;
       AnimatorHook animatorHook;
       public Transform holder;

        public float horizontalSpeed = .8f;
        public float verticalSpeed = .65f;
        public bool isAI;

        public ActionData[] actions;

        public bool isInteracting
        {
            get
            {
                return animatorHook.isInteracting;
            }
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorHook = GetComponentInChildren<AnimatorHook>();

            agent.updateRotation = false;
        }

        public void TickPlayer(float delta, Vector3 direction)
        {
            if (isInteracting)
            {
                agent.velocity = animatorHook.deltaPosition;
                return;
            }

            direction.x *= horizontalSpeed;
            direction.z *= verticalSpeed;

            bool isMoving = direction.sqrMagnitude > 0;

            agent.velocity = direction; // * delta
            
            animatorHook.Tick(direction.sqrMagnitude > 0);

            if (isMoving)
            {
                Vector3 eulers = Vector3.zero;
                if(direction.x < 0)
                    eulers.z = 180;

                holder.localEulerAngles = eulers;
            }

        }
        ActionData storedAction;

        public ActionData getLastAction
        {
            get
            {
                return storedAction;
            }
        }

        public void PlayAction(ActionData actionData)
        {
            PlayAnimation(actionData.actionAnim);
            storedAction = actionData;
        }

        public void PlayAnimation(string animName)
        {
            animatorHook.PlayAnimation(animName);
        }

        public void OnHit(ActionData actionData, Vector3 hitter)
        {
            Vector3 direction = hitter - transform.position;
            bool isFromBehind = direction.x < 0;
            if (isAI)
                isFromBehind = false;
                
            switch (actionData.damageType)
            {
                case DamageType.light:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_light_back");
                    }else
                    {
                        PlayAnimation("hit_light_front");             
                    }
                    break;
                case DamageType.mid:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_light_back");
                    }else
                    {
                        PlayAnimation("hit_light_front");             
                    }
                    break;
                case DamageType.heavy:
                    if (isFromBehind)
                    {
                        PlayAnimation("knockdown_back");
                    }else
                    {
                        PlayAnimation("knockdown_front");             
                    }
                    break;
                default:
                    break;
            }
        }

    }
}


