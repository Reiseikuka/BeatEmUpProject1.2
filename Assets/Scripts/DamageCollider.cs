using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DamageCollider : MonoBehaviour
    {
        UnitController owner;
        public bool IsAHit = false;
        /*When hitting ememies, it should become true so HitCounter 
        Script starts counting how many hits the player delivered to the enemies*/
        
        private void Start()
        {
            owner = GetComponentInParent<UnitController>();

            IsAHit = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            UnitController u = other.GetComponentInParent<UnitController>();
            if (u != null)
            {
                if (u != owner)
                {
                    if (owner.getLastAction == null)
                        return;

                    if (u.team != owner.team || owner.getLastAction.canHitAllies)
                    {
                        u.OnHit(owner.getLastAction, owner.isLookingLeft, owner);
                        IsAHit = true;
                    }
                }
                
            }
        }
    }
}

