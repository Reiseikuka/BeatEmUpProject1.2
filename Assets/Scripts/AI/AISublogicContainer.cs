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
			for (int i = 0; i < sublogics.Length; i++)
			{
				if (sublogics[i].instanceSublogic)
				{
					sublogics[i] = Instantiate(sublogics[i]);
				}
			}

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