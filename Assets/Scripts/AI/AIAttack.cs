using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA
{
    [CreateAssetMenu]
    public class AIAttack : AILogic
    {
        public override void Exit(AIHandler h)
        {
            h.AssignState(exitState);
        }

        public override void Init(AIHandler h)
        {
            h.PlayActionFromHolder();
            Debug.Log("attack");
        }

        public override bool Tick(float delta, AIHandler h)
        {
            return !h.unitController.animatorHook.isInteracting;
        }
        /*Waiting for the Tick not interacting before leaving the State*/
    }
}
