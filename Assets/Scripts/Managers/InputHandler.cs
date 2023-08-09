using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class InputHandler : MonoBehaviour
    {
        public UnitController unitController;
        public InputFrame inputFrame;

        private void Update()
        {

            inputFrame.attack = false;
            inputFrame.jump = false;
            inputFrame.left = false;
            inputFrame.right = false;
            inputFrame.up = false;
            inputFrame.down = false;


            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            inputFrame.attack = Input.GetKeyDown(KeyCode.Z);
            inputFrame.jump = Input.GetKeyDown(KeyCode.Space);
                        //Try getButton next

            if (h > 0.2f)
            {   
                inputFrame.right = true;
            }

            if (v < -.2f)
            {
                inputFrame.left = true;
            }

            if (v > .2f)
            {
                inputFrame.up = true;
            }

            if (v < -.2f)
            {
                inputFrame.down = true;
            }

            Vector3 targetDirection = Vector3.zero;
            targetDirection.x = h;
            targetDirection.y = v;

            if (unitController.isInteracting)
            {
                if (unitController.canDoCombo)
                {
                    if (inputFrame.attack)
                    {
                        unitController.isCombo();
                    }
                }

                unitController.UseRootMotion(Time.deltaTime);
            }
            else
            {

                if (targetDirection.x != 0)
                {
                    unitController.HandleRotation(targetDirection.x < 0);
                }
            
                unitController.TickPlayer(Time.deltaTime, targetDirection);
                unitController.DetectAction(inputFrame);

             /*   if (Input.GetKeyDown(KeyCode.X))
                {
                        unitController.PlayAction(unitController.actions[1]);

                }   
            */

            } 

        }

        [System.Serializable]
        public class InputFrame 
        {
            public bool left;
            public bool right;
            public bool up;
            public bool down;
            public bool attack;
            public bool jump;
        }

    }   
}

