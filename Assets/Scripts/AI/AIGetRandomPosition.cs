using System.Collections;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class AIGetRandomPosition : AILogic
	{
		public int maxIterations = 3;
		[System.NonSerialized]
		int iterations;

		bool isWaiting;
		float waitTime;
		public float minWaitTime = .8f;
		public float maxWaitTime = 1.5f;
		public float rotateDis = 2f;

		public override void Exit(AIHandler h)
		{
			h.AssignState(exitState);
		}

		public override void Init(AIHandler h)
		{
			iterations = maxIterations;
			h.GetRandomPosition();
			waitTime = Random.Range(minWaitTime, maxWaitTime);
		}


		public override bool Tick(float delta, AIHandler h)
		{
			
			if (h.GetDistanceFromEnemy() < .4f)
			{
				return true;
			}

			h.HandleAimingToEnemy(rotateDis);

			if (isWaiting)
			{
				waitTime -= delta;
				if (waitTime > 0) 
				{
					return false;
				}

				h.GetRandomPosition();
				isWaiting = false;
			}

			bool isDone = h.MoveToPosition(delta);

			if (isDone)
			{
				iterations--;
				isWaiting = true;
				waitTime = Random.Range(minWaitTime, maxWaitTime);
			}

			if (iterations == 0)
			{
				return true;
			}

			return false;
		}
	}
}