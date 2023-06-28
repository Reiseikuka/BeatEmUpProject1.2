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
		AILogic _waitState;

		public AILogic exitLoopLogic;
		bool isWaiting;

		public override void Exit(AIHandler h)
		{
			h.AssignState(exitLoopLogic);
		}

		public override void Init(AIHandler h)
		{
			iterations = maxIterations;
			h.GetRandomPosition();
			_waitState = Instantiate(exitState);
			_waitState.exitState = this;
			Debug.Log("random");
		}

		public override bool Tick(float delta, AIHandler h)
		{
			if (isWaiting)
			{
				if (!_waitState.Tick(delta, h))
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
				_waitState.Init(h);
			}

			if (iterations == 0)
			{
				return true;
			}

			return false;
		}
	}
}