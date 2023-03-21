using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class UnitController : MonoBehaviour
    {
       public int health = 100;
       public int team;
       public  NavMeshAgent agent;
       AnimatorHook animatorHook;
       public Transform holder;

        public float horizontalSpeed = .8f;
        public float verticalSpeed = .65f;
        public bool isLookingLeft;
        public bool isAI;
        public bool hasBackHit;
        public bool isDead;

        public Vector3 position
        {
            get
            {
                return transform.position;
            }
        }

        public bool canDoCombo
        {
            get
            {
                return animatorHook.canEnableCombo;
            }
        }

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

            direction.x *= horizontalSpeed;
            direction.z *= verticalSpeed;
            bool isMoving = direction.sqrMagnitude > 0;
            agent.velocity = direction; // * delta
            animatorHook.Tick(direction.sqrMagnitude > 0);

        }

        public void UseRootMotion()
        {
            agent.velocity = animatorHook.deltaPosition;
        }

        public void HandleRotation(bool looksLeft)
        {
            Vector3 eulers = Vector3.zero;
            isLookingLeft = false;
            if(looksLeft)
            {
                eulers.z = 180;
                isLookingLeft = true;
            }
            holder.localEulerAngles = eulers;
        }
        ActionData storedAction;

        public ActionData getLastAction
        {
            get
            {
                return storedAction;
            }
        }

        public void DetectAction(InputHandler.InputFrame f)
        {
            if (f.attack == false)
                return;
                
            foreach (var a in actions)
            {
                if (a.inputs.attack == f.attack 
                && a.inputs.down == f.down &&
                   a.inputs.left == f.left &&
                   a.inputs.right == f.right &&
                   a.inputs.up ==    f.up &&
                   a.inputs.jump == f.jump)
                {
                    PlayAction(a);
                    break;
                }
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

        public void SetIsDead()
        {
            animatorHook.SetIsDead();
            isDead = true;
        }

        public void OnHit(ActionData actionData, bool hitterLooksLeft)
        {   
            bool isFromBehind = false;

            if( isLookingLeft && hitterLooksLeft
                || !hitterLooksLeft && !isLookingLeft)
            {
                isFromBehind = true;
            }

            DamageType damageType = actionData.damageType;
            health -= actionData.damage;

            if (health <= 0)
            {
                damageType  = DamageType.heavy;
                SetIsDead();
            }

            if (!hasBackHit)
            {
                if (isFromBehind)
                {
                    HandleRotation(!hitterLooksLeft);
                }
            }
                isFromBehind = false;
                
            switch (damageType)
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

        public void isCombo()
        {
            animatorHook.SetIsCombo();
        }

    }
}


