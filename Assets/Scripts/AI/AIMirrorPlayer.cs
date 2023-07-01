using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu]
    public class AIMirrorPlayer : AILogic
    {

        public Vector3 targetOffset;

        public override void Exit(AIHandler h)
        {
            h.AssignState(exitState);
        }

        public override void Init(AIHandler h)
        {
            
        }

        public override bool Tick(float delta, AIHandler h)
        {

            if (h.GetDistanceFromEnemy() < .4f)
            {
                return true;
            }

            bool hasPosition = h.GetPositionCloseToPlayer(targetOffset);
            
            if (!hasPosition)
            {
                //GO TO RANDOM
            }
            
            h.MoveToPosition(delta);
            h.HandleAimingToEnemy(10);

            return false;
        }
    }
}
