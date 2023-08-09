using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class AISubAttackWhenClose : AISubLogic
	{
		public float attackDis_Y = 0.05f;
		[Header("X should be higher than target offset & stopping distance!")]
		public float attackDis_X = 0.3f;
		public ActionData action;

		public override void Tick(float delta, AIHandler h)
		{
			float d_x = h.GetDistance(h.transform.position, h.enemy.transform.position, false);
			float d_y = h.GetDistance(h.transform.position, h.enemy.transform.position, true);

			if (d_y < attackDis_Y && d_x <= attackDis_X)
			{
				h.unitController.PlayAction(action);
			}
		}
	}
}
