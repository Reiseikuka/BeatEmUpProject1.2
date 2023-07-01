using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class AIHandler : MonoBehaviour
    {
        public UnitController unitController;

		public UnitController enemy;

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

		public bool isInteracting {
			get {
				return unitController.isInteracting;
			}
		}

		private void Start()
		{
			unitController.isAI = true;
			unitController.onDeath = UnRegisterMe;

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
				unitController.UseRootMotion(delta);
				return;
			}

			if (currentLogic == null || enemy == null)
				return;

			bool isDone = currentLogic.Tick(delta, this);

			if (isDone)
			{
				currentLogic.Exit(this);
			}


			return;
			
			Vector3 myPosition = transform.position;
			Vector3 enemyPosition = enemy.position;

			

			if (deadTime > 0)
			{
				unitController.debugState = "Waiting" + "\n" + deadTime.ToString("0.00"); ;
				deadTime -= delta;
				return;
			}

			Vector3 directionToTarget = enemyPosition - myPosition;
			directionToTarget.Normalize();
			directionToTarget.z = 0;

			Vector3 targetPosition = enemyPosition + (directionToTarget * -1) * attackDistance;

			bool isCloseToTargetPosition = IsCloseToTargetPosition(myPosition, targetPosition);
			bool closeToEnemy_NoVertical = isCloseToEnemy_NoVertical(myPosition, enemyPosition);
			bool closeToEnemy_General = isCloseToEnemy_General(myPosition, enemyPosition);

			Collider[] colliders = Physics.OverlapSphere(transform.position, forceStopDistance);
			forceStop = false;

			foreach (var item in colliders)
			{
				AIHandler a = item.transform.GetComponentInParent<AIHandler>();
				if (a != null)
				{
					if (a != this)
					{
						if (!a.forceStop)
						{
							forceStop = true;
						}
					}
				}
			}

			Vector3 targetDirection = Vector3.zero;


			if (!forceStop && !closeToEnemy_NoVertical && !isCloseToTargetPosition)
			{
				targetDirection = targetPosition - transform.position;
				targetDirection.Normalize();
				Debug.DrawLine(transform.position, targetPosition);

				unitController.debugState = "Moving to target";

				if (!closeToEnemy_General)
				{
					unitController.HandleRotation(targetDirection.x < 0);
				}
				else
				{
					unitController.HandleRotation(directionToTarget.x < 0);
				}

			}
			else
			{

				unitController.HandleRotation(directionToTarget.x < 0);

				if (attackTime > 0)
				{
					unitController.debugState = "Waiting to Attack" + "\n" + attackTime.ToString("0.00");

					if (!forceStop)
						attackTime -= delta;
				}
				else
				{
					unitController.debugState = "<color=red>Attacking</color>";


					unitController.PlayAction(unitController.actionDataHolder.actions[0].actions[0]);
					attackTime = attackRate;
					deadTime = getDeadTimeRate;
				}
			}

			unitController.TickPlayer(delta, targetDirection);
		}

		public void PlayActionFromHolder()
		{ 
			unitController.PlayAction(unitController.actionDataHolder.actions[0].actions[0]);
		}

		public bool IsCloseToTargetPosition(Vector3 p1, Vector3 p2)
		{
			float distance = Vector3.Distance(p1, p2);
			return distance < stopDistance;
		}

		public bool isCloseToEnemy_NoVertical(Vector3 p1, Vector3 p2)
		{
			float dif = p1.z - p2.z;
			if (Mathf.Abs(dif) < verticalThreshold)
			{
				return Vector3.Distance(p1, p2) < attackDistance;
			}
			else
			{
				return false;
			}
		}

		public bool isCloseToEnemy_General(Vector3 p1, Vector3 p2)
		{ 
				return Vector3.Distance(p1, p2) < rotateDistance;

		}

		public float GetDistance(Vector3 p1, Vector3 p2)
		{
			p1.z = 0;
			p2.z = 0;

			return Vector3.Distance(p1, p2);
		}

		public float GetDistanceFromEnemy()
		{
			return GetDistance(transform.position, enemy.position);
		}
		public void HandleAimingToEnemy(float rotateDistance)
		{
			float dis =  GetDistanceFromEnemy();

			if (dis < rotateDistance)
			{
				Vector3 direction = enemy.position - transform.position;

				unitController.HandleRotation(direction.x < 0);
			}
			else
			{

			}

		}

		int randomPositionSteps;

		public void GetRandomPosition()
		{

			Vector3 randomPosition = Random.insideUnitCircle;
			Vector3 tp = enemy.position + randomPosition;

			bool result = isValidPosition(ref tp);

			if (result)
			{
				hasMovePosition = true;
				currentPath = GridManager.singleton.GetPath(
					transform.position, tp);
			} 
			else
			{
				if (randomPositionSteps < 5)
				{
					GetRandomPosition();
					randomPositionSteps++;
				}
			}

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

			if(!result)
			{
				col = Physics2D.OverlapCircle(tp, 5, unitController.walkLayer);

				RaycastHit2D hit2D = Physics2D.Linecast(tp, col.transform.position,unitController.walkLayer);
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

		public bool GetPositionCloseToPlayer(Vector3 offset)
		{
			Vector3 dir = enemy.position - transform.position;
			if (dir.x > 0)
			{
				offset.x = -offset.x;
			}

			Vector3 tp = enemy.position;
			tp += offset;

			bool isValid = isValidPosition(ref tp);

			if (isValid)
			{
				currentPath = GridManager.singleton.GetPath(
						transform.position, tp);

				hasMovePosition = true;
				return true;
			}

			return false;

		}

		IEnumerator GetRandomPositionRoutine()
		{
			List<Node> flowmap = GridManager.singleton.GetFlowmap(
				enemy.position, 10,5);
			int ran = Random.Range(0, flowmap.Count);

			currentPath = GridManager.singleton.GetPath(
				transform.position, flowmap[ran].worldPosition);

			foreach (var item in currentPath)
			{
				Debug.DrawRay(item.worldPosition, Vector3.up * .1f, Color.red, 5);
			}

			hasMovePosition = true;
			yield return null;
		}


		List<Node> currentPath;
		Vector3 targetPosition;
		bool hasMovePosition;

		public bool MoveToPosition(float delta)
		{
			if (currentPath == null || currentPath.Count == 0)
			{
				if (hasMovePosition)
				{
					unitController.TickPlayer(delta, Vector3.zero);
					//TODO change logic
					hasMovePosition = false;
					//Invoke("GetRandomPosition", 2);
				}
				return true;
			}

			targetPosition = currentPath[0].worldPosition;

			Vector3 p1 = targetPosition;
			p1.z = transform.position.z;

			Vector3 targetDirection = p1 - transform.position;
			targetDirection.Normalize();

			unitController.TickPlayer(delta, targetDirection);

			float disToTarget = Vector2.Distance(transform.position, currentPath[0].worldPosition);
			if (disToTarget < 0.03f)
			{
				currentPath.RemoveAt(0);
			}

			return false;
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
