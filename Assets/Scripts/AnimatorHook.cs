using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;

        public Vector3 deltaPosition;
        UnitController owner;

        public UnityEvent[] myEvents;

        public void TriggerEvent(int i)
        {
            if (i > myEvents.Length - 1)
                return;

            myEvents[i].Invoke();
        }

        public bool canEnableCombo {
            get {
                return anim.GetBool("canEnableCombo");
            }
        }

        public bool isInteracting {
            get {
                return anim.GetBool("isInteracting");
            }
        }

		private void Awake()
		{
            anim = GetComponent<Animator>();
            owner = GetComponentInParent<UnitController>();
        }
        public void Tick(bool isMoving)
        {
            float v = (isMoving) ? 1 : 0;
            anim.SetFloat("move", v);
        }

        public void PlayAnimation(string animName, float crossfadeTime = 0)
        {
            anim.CrossFadeInFixedTime(animName, crossfadeTime);
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

        public void SetIsCombo()
        { 
            anim.SetBool("isCombo", true);
        }

        public void CloseAgent()
        {
            //owner.agent.enabled = false;
        }

        public void OpenAgent()
        {
        //    owner.agent.enabled = true;
        }

        public void SetIsInteracting(int status)
        {
            anim.SetBool("isInteracting", status == 1);
        }

        public void LoadActionData(int actionIndex)
        {
            owner.LoadActionData(actionIndex);
        }

        public void Vanish()
        {
            owner.VanishObject();
        }

        public void ShakeCamera()
        {
            CameraManager.singleton.ShakeCamera();
        }

        public void SpawnObject(string id)
        {
            owner.SpawnObject(id);
        }
    }
}
