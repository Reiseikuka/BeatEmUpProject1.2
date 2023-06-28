using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public abstract class AILogic : ScriptableObject
	{
		public AILogic exitState;

		public abstract void Init(AIHandler h);

		public abstract bool Tick(float delta,AIHandler h);

		public abstract void Exit(AIHandler h);
	}
}
