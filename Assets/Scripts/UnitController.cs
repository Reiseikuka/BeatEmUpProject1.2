using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class UnitController : MonoBehaviour
    {
       public  NavMeshAgent agent;
       AnimatorHook animatorHook;
       public Transform holder;

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


        public void PlayAnimation(string animName)
        {
            animatorHook.PlayAnimation(animName);
        }

    }
}


