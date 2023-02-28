using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {
            Animator anim;

            public Vector3 deltaPosition;
            
            public bool isInteracting
            {
                get
                {
                    return anim.GetBool("isInteracting");
                }
            }
            private void Start()
            {
                 anim = GetComponent<Animator>();
            }

            public void Tick(bool isMoving)
            {
                float v = (isMoving) ? 1 : 0;
                anim.SetFloat("move", v);
            }
            public void  PlayAnimation(string animName)
            {
                anim.Play(animName);
                anim.SetBool("isInteracting", true);
            }

            private void OnAnimatorMove()
            {
                deltaPosition = anim.deltaPosition / Time.deltaTime;
            }

            public void SetIsDead()
            {
                anim.SetBool("isDead", true);
            }
    }
}

