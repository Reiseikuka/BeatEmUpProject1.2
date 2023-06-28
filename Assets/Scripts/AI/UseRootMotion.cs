using System.Collections;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class UseRootMotion : AILogic
	{
		public override void Exit(AIHandler h)
		{
		}

		public override void Init(AIHandler h)
		{
		}

		public override bool Tick(float delta, AIHandler h)
		{
			h.unitController.UseRootMotion(delta);
			return false;
		}
	}
}