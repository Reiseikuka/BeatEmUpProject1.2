using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PolyNav;

namespace SA
{
    public class AIHandler : MonoBehaviour
    {
        public UnitController unitController;

		public UnitController enemy;

		public PolyNavAgent agent;
		public Phase myPhase;

		public float minDeadTime;
		public float maxDeadTime;
		float getDeadTimeRate {
			get {
				float v = Random.Range(minDeadTime, maxDeadTime);
				return v;
			}
		}

		public void AssignState(AILogic exitState)
		{
			currentLogic = Instantiate(exitState);
			currentLogic.Init(this);
		}

		float deadTime;

		float attackTime = 1;
		public float attackRate = 1.5f;
		public float attackDistance = 2;
		public float stopDistance = 1;
		public float rotateDistance = 2;
		public float verticalThreshold = .1f;
		public float rotationThreshold = .5f;
		public float forceStopDistance = .3f;
		public bool forceStop;

		public AIWaypoints waypoints;

		public bool isInteracting {
			get {
				return unitController.isInteracting;
			}
		}

		private void Start()
		{
			unitController.isAI = true;
			unitController.onDeath = UnRegisterMe;
			agent = GetComponent<PolyNavAgent>();
			agent.map = LevelManager.singleton.navmesh;

			if (myPhase != null)
			{
				myPhase.RegisterUnit(this);
			}

			AssignState(currentLogic);
		}

		void UnRegisterMe()
		{
			if (myPhase != null)
			{
				myPhase.UnRegisterUnit(this);
			}
		}

		public AILogic currentLogic;
		

		private void Update()
		{
			float delta = Time.deltaTime;

			if (isInteracting || unitController.isDead)
			{
				agent.Stop();
				unitController.UseRootMotion(delta);
				return;
			}

			if (currentLogic != null)
			{
				bool isDone = currentLogic.Tick(delta, this);

				if (isDone)
				{
					currentLogic.Exit(this);
				}
			}

			Vector3 targetToEnemy = enemy.transform.position - unitController.transform.position;
			unitController.HandleRotation(targetToEnemy.x < 0);
			unitController.animatorHook.Tick(agent.remainingDistance > 0.01f);
		}

		public void PlayActionFromHolder()
		{ 
			unitController.PlayAction(unitController.actionDataHolder.actions[0].actions[0]);
		}

		public float GetDistance(Vector3 p1, Vector3 p2, bool isY)
		{
			if (isY)
			{
				p1.x = 0;
				p2.x = 0;

				return Vector2.Distance(p1, p2);
			}
			else
			{
				p1.y = 0;
				p2.y = 0;

				return Vector2.Distance(p1, p2);
			}
		}

		public float GetDistance(Vector3 p1, Vector3 p2)
		{
		
			return Vector2.Distance(p1, p2);
		}

		public float GetDistanceFromEnemy()
		{
			return GetDistance(transform.position, enemy.position);
		}

		public void HandleAimingToEnemy(float rotateDistance)
		{
			float dis = GetDistanceFromEnemy();

			if (dis < rotateDistance)
			{
				Vector3 direction = enemy.position - transform.position;

				unitController.HandleRotation(direction.x < 0);
			}
			else 
			{ 
				//unitController.HandleRotation(unitController.
			}

		}

		int randomPositionSteps;

		public void GetRandomPosition()
		{
			//Vector3 randomPosition = Random.insideUnitCircle;
			//Vector3 tp = enemy.position + randomPosition;

			//bool result = isValidPosition(ref tp);

			//if (result)
			//{
			//	hasMovePosition = true;
			//	currentPath = GridManager.singleton.GetPath(
			//		transform.position, tp);
			//}
			//else
			//{
			//	if (randomPositionSteps < 5)
			//	{
			//		GetRandomPosition();
			//		randomPositionSteps++;
			//	}
			//}

			//StartCoroutine(GetRandomPositionRoutine());
		}

		public bool isValidPosition(ref Vector3 tp)
		{
			bool result = false;

			Collider2D col = Physics2D.OverlapPoint(tp, unitController.walkLayer);

			if (col != null)
			{
				TWalkable w = col.gameObject.transform.GetComponentInParent<TWalkable>();
				if (w != null)
				{
					result = true;
				}

			}

			if (!result)
			{
				col = Physics2D.OverlapCircle(tp, 5, unitController.walkLayer);

				RaycastHit2D hit2D = Physics2D.Linecast(tp, col.transform.position, unitController.walkLayer);
				if (hit2D.transform != null)
				{
					TWalkable w = col.gameObject.transform.GetComponentInParent<TWalkable>();
					if (w != null)
					{
						result = true;
					}

					tp = hit2D.point;
				}
			}

			Node n = GridManager.singleton.GetNode(tp);
			if (n.isWalkable)
			{
				

				result = true;
			}
			
			return result;
		}

		public Vector3 GetPositionCloseToPlayer(Vector3 offset)
		{
			Vector3 dir = enemy.position - transform.position;
			if (dir.x > 0)
			{
				offset.x = -offset.x;
			}

			Vector3 tp = enemy.position;
			tp += offset;

			return tp;
		}

		IEnumerator GetRandomPositionRoutine()
		{
			//List<Node> flowmap = GridManager.singleton.GetFlowmap(
			//	enemy.position, 10,5);
			//int ran = Random.Range(0, flowmap.Count);

			//currentPath = GridManager.singleton.GetPath(
			//	transform.position, flowmap[ran].worldPosition);

			//foreach (var item in currentPath)
			//{
			//	Debug.DrawRay(item.worldPosition, Vector3.up * .1f, Color.red, 5);
			//}

			//hasMovePosition = true;
			yield return null;
		}



		public void MoveToPosition(Vector3 targetPosition, System.Action<bool> callback)
		{
			agent.SetDestination(targetPosition, callback);
		}

		Vector3 GetValidPosition(Vector3 o)
		{
			Vector3 retVal = o;

			bool isValid = false;
			Collider2D[] colliders = Physics2D.OverlapPointAll(o, unitController.walkLayer);

			foreach (var col in colliders)
			{
				TBlockMovement b = col.transform.GetComponentInParent<TBlockMovement>();
				if (b != null)
					continue;

				TWalkable w = col.transform.GetComponentInParent<TWalkable>();
				if (w != null)
				{
					isValid = true;
				}
				
			}

			if (!isValid)
			{
				retVal = GetClosestValidPosition(o);
			}

			return retVal;
		}


		Vector3 GetClosestValidPosition(Vector3 o)
		{
			Vector3 retVal = o;

			Collider2D[] colliders = Physics2D.OverlapCircleAll(o,20, unitController.walkLayer);

			foreach (var col in colliders)
			{
				TBlockMovement b = col.transform.GetComponentInParent<TBlockMovement>();
				if (b != null)
					continue;

				TWalkable w = col.transform.GetComponentInParent<TWalkable>();
				if (w != null)
				{
					Vector3 dir =  o - transform.position;
					dir.Normalize();
					RaycastHit2D hit2D = Physics2D.Raycast(o, dir, 100, unitController.walkLayer);

					if (hit2D.transform != null)
					{
						float dis = Vector2.Distance(o, hit2D.point);
						retVal = transform.position + dir * (dis * .5f);

						Debug.DrawLine(transform.position, dir * (dis * .9f), Color.green,20);
					}
				}
			}

			return retVal;
		}
	}
}
