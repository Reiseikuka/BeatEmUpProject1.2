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
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool isKeyZ= Input.GetKeyDown(KeyCode.Z);


            Vector3 targetDirection = Vector3.zero;
            targetDirection.x = h;
            targetDirection.z = v;

            if (unitController.canDoCombo)
            {
                if (isKeyZ)
                {
                    unitController.isCombo();
                }
            }

            if (unitController.isInteracting)
            {
                unitController.UseRootMotion();
            }
            else
            {

                if (targetDirection.x != 0)
                {
                    unitController.HandleRotation(targetDirection.x < 0);
                }
            
                unitController.TickPlayer(Time.deltaTime, targetDirection);

                if (isKeyZ)
                {
                        unitController.PlayAction(unitController.actions[0]);
                }

             /*   if (Input.GetKeyDown(KeyCode.X))
                {
                        unitController.PlayAction(unitController.actions[1]);

                }   
            */

            } 

        }

    }   
}

