using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public class AIWaypoints : MonoBehaviour
	{
		public AWaypoint[] aWaypoints;

	}

	[System.Serializable]
	public class AWaypoint {

		public Transform p1;
		public Transform p2;

	}
}
