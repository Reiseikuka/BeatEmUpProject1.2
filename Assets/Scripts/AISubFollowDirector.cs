using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class AISubFollowDirector : AISubLogic
	{
		public override void Tick(float delta, AIHandler h)
		{
			if (h.hasAttackPosition)
			{
				Vector3 offset = h.getStoredPosition.offset;
				Vector3 targetPosition = h.enemy.transform.position;
				targetPosition.x += offset.x;
				targetPosition.y += offset.y;

				h.MoveToPosition(targetPosition, OnReachTarget);
			}
		}

		private void OnReachTarget(bool result)
		{

		}
	}
}
