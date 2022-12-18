using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        public UnitController unitController;

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            
            Vector3 targetDirection = Vector3.zero;
            targetDirection.x = h;
            targetDirection.z = v;
            
            unitController.TickPlayer(Time.deltaTime, targetDirection);
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!unitController.isInteracting)
                {
                    unitController.PlayAnimation("attack 1");
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!unitController.isInteracting)
                {
                    unitController.PlayAnimation("attack 2");
                }
            }
        }

    }   
}

