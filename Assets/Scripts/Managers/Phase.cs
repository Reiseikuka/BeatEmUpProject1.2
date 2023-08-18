using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SA
{
	public class Phase : MonoBehaviour
	{
		public List<AIHandler> currentUnits = new List<AIHandler>();

		public UnityEvent onPhaseStart;
		public UnityEvent onPhaseEnded;

		public void AssignPhaseToManager()
		{
			PhaseManager.singleton.AssignPhase(this);
		}

		public void CameraFollowStatus(bool status)
		{
			CameraManager.singleton.FollowStatus(status);
		}

		public void RegisterUnit(AIHandler a)
		{
			currentUnits.Add(a);
		}

		public void UnRegisterUnit(AIHandler a)
		{
			currentUnits.Remove(a);

			if (currentUnits.Count == 0)
			{
				onPhaseEnded.Invoke();
			}
		}

	}
}
