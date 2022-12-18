using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class DamageCollider : MonoBehaviour
    {
        UnitController owner;
        private void Start()
        {
            owner = GetComponentInParent<UnitController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            UnitController u = other.GetComponentInParent<UnitController>();

            if (u != null)
            {
                if (u!= owner)
                {
                    if (!u.isInteracting)
                    {
                        u.PlayAnimation("hurt 1");
                    }
                }

            }
        }
    }
}

