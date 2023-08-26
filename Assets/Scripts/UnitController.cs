using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SA
{
    public class UnitController : MonoBehaviour
    {
        public int health = 100;
        public int team;
        public AnimatorHook animatorHook;
        public Transform holder;

        public float horizontalSpeed = .8f;
        public float verticalSpeed = .65f;
        public bool isLookingLeft;
        public bool isAI;
        public bool hasBackHit;
        public bool isDead;
        public LayerMask walkLayer;

        public delegate void OnDeath();
        public OnDeath onDeath;

        public GameObject hitParticle;

        public Vector3 position {
            get {
                return transform.position;
            }
        }

        public bool canDoCombo {
            get {
                return animatorHook.canEnableCombo;
            }
        }

        public int actionsIndex;
        public ActionDataHolder actionDataHolder;

        ActionsContainer currentActionData {
            get {
                return actionDataHolder.GetActions(actionsIndex);
            }
        }


        public bool isInteracting {
            get {
                return animatorHook.isInteracting;
            }
        }

        DebugTextHandler debugText;
        public string debugState;
        Camera cam;

		private void Start()
		{
            animatorHook = GetComponentInChildren<AnimatorHook>();
            //cam = GetComponent<Camera>();

            if (isAI)
            {
                GameObject go = UIManager.singleton.CreateDebugTextObj();
                debugText = go.GetComponentInChildren<DebugTextHandler>();
                debugText.target = this.transform;
                go.SetActive(true);
            }
		}

		public void TickPlayer(float delta, Vector3 direction)
        {
            direction.x *= horizontalSpeed *delta;
            direction.y *= verticalSpeed * delta;
            bool isMoving = direction.sqrMagnitude > 0;
           
            animatorHook.Tick(isMoving);

            Vector3 targetPosition = transform.position + direction;
            MoveOnPosition(targetPosition);
        }

        public void UseRootMotion(float delta)
        {
            Vector3 targetPosition = transform.position + animatorHook.deltaPosition * delta;
            MoveOnPosition(targetPosition);
        }

        void MoveOnPosition(Vector3 targetPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(targetPosition,walkLayer);
            bool isValid = false;

			foreach (var item in colliders)
			{
                if (!isAI)
                {
                    TBlockMovement block = item.GetComponent<TBlockMovement>();
                    if (block != null)
                    {
                        isValid = false;
                        break;
                    }
                }

                TWalkable w = item.GetComponent<TWalkable>();
                if (w != null) {
                    if (isAI)
                    {
                        isValid = true;
                    }
                    else
                    {
                        if (w.isPlayer)
                        {
                            isValid = true;
                        }
                    }
                }
			}


            Debug.Log(Camera.main);

            if (!isAI) 
            {

                //Vector2 viewport = cam.WorldToViewportPoint(targetPosition);
                Vector2 viewport = Camera.main.WorldToViewportPoint(targetPosition);
                if (viewport.x < 0 || viewport.x > 1 || viewport.y < 0 || viewport.y > 1)
                    {
                        isValid = false;
                    }
            }
            //If player wants to get outside of the camera length, do not allow it. Making position valid if is on the Camera space
               //Is not working as one would want

            if (isValid || isAI && !isInteracting)
            { 
                transform.position = targetPosition;
            }
        }

        public void HandleRotation(bool looksLeft)
        {
            Vector3 eulers = Vector3.zero;
            isLookingLeft = false;
            if (looksLeft)
            {
                eulers.y = 180;
                isLookingLeft = true;
            }
            holder.localEulerAngles = eulers;
        }

        ActionData storedAction;
        public ActionData getLastAction {
            get {
                return storedAction;
            }
        }

        public void DetectAction(InputHandler.InputFrame f)
        {
            if (currentActionData == null)
                return;

            if (f.attack == false && f.jump == false && f.isDashLeft == false && f.isDashRight == false)
                return;

            if (f.isDashLeft || f.isDashRight) 
            {
                    if (currentActionData.canDash)
                    {
                        HandleRotation(f.isDashLeft);
                        PlayAnimation("dash");
                    }

                    f.isDashLeft = false;
                    f.isDashRight = false;
                return;
            }

			foreach (var a in currentActionData.actions)
			{
                if (a.isDeterministic)
                {
                    if (a.inputs.attack == f.attack
                        && a.inputs.down == f.down &&
                        a.inputs.left == f.left &&
                        a.inputs.right == f.right &&
                        a.inputs.up == f.up &&
                        a.inputs.jump == f.jump)
                    {
                        PlayAction(a);
                        break;
                    }
                }
                else {

                    if (a.inputs.attack == f.attack
                        || a.inputs.jump == f.jump)
                    {
                        PlayAction(a);
                        break;
                    }
                }
			}
        }

        public void PlayAction(ActionData actionData)
        {
            PlayAnimation(actionData.actionAnim);
            storedAction = actionData;
        }

        public void PlayAnimation(string animName, float crossfadeTime = 0)
        {
            animatorHook.PlayAnimation(animName, crossfadeTime);
        }

        public void SetIsDead()
        {
            debugState = "<color=green>Dead</color>";
            animatorHook.SetIsDead();
            isDead = true;
            onDeath?.Invoke();
        }

        

        public void OnHit(ActionData actionData, bool hitterLooksLeft, UnitController attacker)
        {
            if (isDead)
                return;

            if (actionData == null)
                return;

            if (attacker.hitParticle != null)
            {
                GameObject go = Instantiate(attacker.hitParticle);
                go.transform.position = transform.position;
                if (!isLookingLeft)
                {
                    go.transform.eulerAngles = new Vector3(0, 0, 0);
                }

                go.SetActive(true);
            }

            bool isFromBehind = false;

            if (isLookingLeft && hitterLooksLeft
                || !hitterLooksLeft && !isLookingLeft)
            {
                isFromBehind = true;
            }

            debugState = "<color=green> Hit </color>";

            DamageType damageType = actionData.damageType;
            health -= actionData.damage;
            if (health <= 0)
            {
                if(damageType != DamageType.bounce)
                    damageType = DamageType.heavy;

                SetIsDead();
            }

            if (!hasBackHit)
            {
                if (isFromBehind)
                {
                    HandleRotation(!hitterLooksLeft);
                }

                isFromBehind = false;
            }

			switch (damageType)
			{
				case DamageType.light:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_light_back");
                    }
                    else
                    { 
                        PlayAnimation("hit_light_front");
                    }
                    break;
				case DamageType.mid:
                    if (isFromBehind)
                    {
                        PlayAnimation("hit_light_back");
                    }
                    else
                    {
                        PlayAnimation("hit_light_front");
                    }
                    break;
				case DamageType.heavy:
                    if (isFromBehind)
                    {
                        PlayAnimation("knockdown_back");
                    }
                    else
                    {
                        PlayAnimation("knockdown_front");
                    }
                    break;
                case DamageType.bounce:
                    PlayAnimation("bounce",0.1f);
                    break;
				default:
					break;
			}

            if (actionData.onHitOverrideMyAnimation)
            {
                attacker.PlayAnimation(
                    actionData.myOverrideAnimation,
                    actionData.crossfadeTime);
            }
		}

        public void isCombo()
        {
            animatorHook.SetIsCombo();
        }

        public void LoadActionData(int index)
        {
            actionsIndex = index;
        }

        public void ResetActionData()
        {
            actionsIndex = 0;
        }

		private void LateUpdate()
		{
            if (debugText != null)
            {
                debugText.text.text = debugState;
            }
		}

        public void VanishObject()
        {
            if (debugText != null)
            {
                Destroy(debugText.gameObject);
                Destroy(this.gameObject);
            }
        }

        public void SpawnObject(string id)
        {
            //TODO implement an object puller
            GameObject prefab = Resources.Load(id) as GameObject;
            GameObject go = Instantiate(prefab);
            go.transform.position = transform.position;
            go.transform.rotation = holder.rotation;

            DamageCollider dc = go.GetComponentInChildren<DamageCollider>();
            if (dc != null)
            {
                dc.AssignOwner(this);

                Projectile p = go.GetComponentInChildren<Projectile>();
                if (p != null)
                {
                    storedAction = p.action;
                }

            }

        }
	}
}