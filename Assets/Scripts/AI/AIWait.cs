using System.Collections;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class AIWait : AILogic
	{
		public float minTime= 1.2f;
		public float maxTime = 3f;

		float waitTime;

		public override void Exit(AIHandler h)
		{
			h.AssignState(exitState);
		}

		public override void Init(AIHandler h)
		{
			waitTime = Random.Range(minTime, maxTime);
			Debug.Log("wait");
		}

		public override bool Tick(float delta, AIHandler h)
		{
			waitTime -= delta;
			if (waitTime < 0)
			{
				return true;
			}

			return false;
		}
	}
}