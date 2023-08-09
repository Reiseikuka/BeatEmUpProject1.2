using System.Collections;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class AISublogicContainer : AILogic
	{
		public AISubLogic[] sublogics;

		public override void Exit(AIHandler h)
		{
			h.AssignState(exitState);
		}

		public override void Init(AIHandler h)
		{
		}

		public override bool Tick(float delta, AIHandler h)
		{
			foreach (var s in sublogics)
			{
				s.Tick(delta, h);
			}
			
			return false;
		}

		
	}
}

/*We will use this Script to create unique AI Logics without the need to use SubLogics*/