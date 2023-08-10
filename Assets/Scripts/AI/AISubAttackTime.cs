using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName ="AI Sub Logics/ Attack Over Time")]
	public class AISubAttackTime : AISubLogic
	{
		public float attackRate = 2;
		
		[System.NonSerialized]
		float _timer = 2;
		
		public string targetAnimation;

		public override void Tick(float delta, AIHandler h)
		{
			_timer -= delta;
			if (_timer > 0)
				return;

			_timer = attackRate;
			h.unitController.animatorHook.PlayAnimation(targetAnimation);
		}
	}
}
