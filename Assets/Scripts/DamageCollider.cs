using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SA
{
	public class DamageCollider : MonoBehaviour
	{
		UnitController owner;
		public bool isProjectile;
		public UnityEvent onHit;

		public void AssignOwner(UnitController o)
		{
			owner = o;
		}


		private void Start()
		{
			if(!isProjectile)
				owner = GetComponentInParent<UnitController>();
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
						onHit.Invoke();
					}
				}
			}
		}
	}
}
