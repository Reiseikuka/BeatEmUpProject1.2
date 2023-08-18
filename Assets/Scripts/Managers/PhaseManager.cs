using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	public class PhaseManager : MonoBehaviour
	{
		Phase currentPhase;

		public Phase debugPhase;
		public bool debugStart;

		public static PhaseManager singleton;
		private void Awake()
		{
			singleton = this;
		}

		private void Update()
		{
			if (debugStart)
			{
				debugStart = false;
				AssignPhase(debugPhase);
			}
		}

		public void AssignPhase(Phase p)
		{
			currentPhase = p;
			currentPhase.onPhaseStart.Invoke();
		}

		public void CameraFollowStatus(bool status) {
			CameraManager.singleton.FollowStatus(status);
		}

	}
}