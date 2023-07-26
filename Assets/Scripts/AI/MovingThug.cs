using System.Collections;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu]
	public class MovingThug : AILogic
	{
		AWaypoint waypoint;
		float wayT;
		public float speed = 4;
		float speedActual;
        //constant speed for the moving enemy

		bool isOpposite;


		public override void Exit(AIHandler h)
		{
			h.AssignState(exitState);
		}

		public override void Init(AIHandler h)
		{
			int ran = Random.Range(0, h.waypoints.aWaypoints.Length);
			waypoint = h.waypoints.aWaypoints[ran];
			wayT = 0;

			float dis = Vector3.Distance(waypoint.p1.position, waypoint.p2.position);
			speedActual = speed / dis;

			h.transform.position = waypoint.p1.position;

			int r_op = Random.Range(0, 11);
			isOpposite = r_op < 5;

			h.unitController.HandleRotation(isOpposite);

			if (!isOpposite)
			{
				h.unitController.transform.GetComponentInChildren<AnimatorHook>().PlayAnimation("jump_in");
			}


		}

		public override bool Tick(float delta, AIHandler h)
		{
			wayT += delta * speedActual;
			bool isDone = false;
			

			if (wayT > 1)
			{
				isDone = true;
				wayT = 1;
			}

			Vector3 p1 = (!isOpposite) ? waypoint.p1.position : waypoint.p2.position;
			Vector3 p2 = (!isOpposite) ? waypoint.p2.position : waypoint.p1.position;

			Vector3 tp = Vector3.Lerp(p1, p2, wayT);
			h.transform.position = tp;

			if (isDone)
			{
				return true;
			}

			return false;
		}
	}
}