using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "AI Sub Logics/ Get To Target Position")]

	public class AISubGetToTargetPosition : AISubLogic
	{
		public Vector3 targetOffset;
		public bool fromPlayer;

		public override void Tick(float delta, AIHandler h)
		{
			Vector3 targetPosition = Vector3.zero;

			if (fromPlayer)
			{ 
				targetPosition = h.GetPositionCloseToPlayer(targetOffset); 
			}
			else
			{
				Vector3 tp = CameraManager.singleton.transform.position;
				tp.x += targetOffset.x;
				tp.y += targetOffset.y;
				targetPosition = tp;
			}

			h.MoveToPosition(targetPosition, OnReachTarget);
		}

		void OnReachTarget(bool result)
		{
			if (result)
			{

			}
		}
	}
}
