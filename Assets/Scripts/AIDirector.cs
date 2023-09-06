using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public class AIDirector : MonoBehaviour
	{
		public List<AIHandler> aIHandlers = new List<AIHandler>();
		public UnitController player;
		public D_TargetPosition[] attackPositions;
		public D_TargetPosition[] retreatPositions;

		float decisionTime;

		private void Update()
		{
			if (decisionTime > 0)
			{
				decisionTime -= Time.deltaTime;
				return;
			}

			decisionTime = 1;
			float realTime = Time.realtimeSinceStartup;

			foreach (var item in attackPositions)
			{
				var ai = GetFirstFree();
				if (ai != null)
				{
					if (ai.hasRetreatPosition)
					{
						ai.ClearTargetPosition_Director();
					}
				}

				if (item.owner != null)
				{
					if (realTime - item.lastChange > 2)
					{
						int r = Random.Range(0, 11);
						if (r < 6)
						{
							item.owner.ClearTargetPosition_Director();
							item.owner = null;
						}
						else
						{
							continue;
						}
					}
					else
					{
						continue;
					}
				}

				if (ai != null)
				{
					item.lastChange = realTime;
					ai.AssignTargetPosition_Director(item, false);
				}
			}

			foreach (var ai in aIHandlers)
			{
				if (!ai.hasAttackPosition && !ai.hasRetreatPosition)
				{
					ai.AssignTargetPosition_Director(GetFirstFree(retreatPositions), true);
				}
			}
		}

		D_TargetPosition GetFirstFree(D_TargetPosition[] l)
		{
			foreach (var item in l)
			{
				if (item.owner == null)
					return item;
			}

			return null;
		}

		AIHandler GetFirstFree()
		{
			for (int i = aIHandlers.Count - 1; i >= 0; i--)
			{
				var item = aIHandlers[i];

				if (item == null)
				{
					aIHandlers.RemoveAt(i);
					continue;
				}

				if (!item.hasAttackPosition)
					return item;
			}

			if (aIHandlers.Count == 0)
			{
				enabled = false;
			}

			return null;
		}
	}

	[System.Serializable]
	public class D_TargetPosition
	{
		public Vector3 offset;
		public AIHandler owner;
		public float lastChange;
	}
}
