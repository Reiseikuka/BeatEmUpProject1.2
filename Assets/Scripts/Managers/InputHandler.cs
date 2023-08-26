using System.Collections;
using UnityEngine;

namespace SA
{
	public class InputHandler : MonoBehaviour
	{
		public UnitController unitController;

		public bool isPlayer2;
		public InputFrame inputFrame;

		public PlayerInputs inputEvents;



		private void Awake()
		{
			inputEvents = new PlayerInputs();

			if (isPlayer2)
			{
				SetupPlayer2();
			}
			else
			{
				SetupPlayer1();
			}

			inputEvents.Enable();
		}

		void SetupPlayer1()
		{
			inputEvents.Player1.Up.started += u => inputFrame.up = true;
			inputEvents.Player1.Up.canceled += u => inputFrame.up = false;
			inputEvents.Player1.Down.started += u => inputFrame.down = true;
			inputEvents.Player1.Down.canceled += u => inputFrame.down = false;
			inputEvents.Player1.Left.started += u => inputFrame.left = true;
			inputEvents.Player1.Left.started += u => HandleDash(true);
			inputEvents.Player1.Left.canceled += u => inputFrame.left = false;
			inputEvents.Player1.Right.started += u => inputFrame.right = true;
			inputEvents.Player1.Right.started += u => HandleDash(false);
			inputEvents.Player1.Right.canceled += u => inputFrame.right = false;

			inputEvents.Player1.Attack1.started += u => inputFrame.attack = true;
			inputEvents.Player1.Attack1.canceled += u => inputFrame.attack = false;

			inputEvents.Player1.Jump.started += u => inputFrame.jump = true;
			inputEvents.Player1.Jump.canceled += u => inputFrame.jump = false;
		}

		void SetupPlayer2()
		{
			inputEvents.Player2.Up.started += u => inputFrame.up = true;
			inputEvents.Player2.Up.canceled += u => inputFrame.up = false;
			inputEvents.Player2.Down.started += u => inputFrame.down = true;
			inputEvents.Player2.Down.canceled += u => inputFrame.down = false;
			inputEvents.Player2.Left.started += u => inputFrame.left = true;
			inputEvents.Player2.Left.started += u => HandleDash(true);
			inputEvents.Player2.Left.canceled += u => inputFrame.left = false;
			inputEvents.Player2.Right.started += u => inputFrame.right = true;
			inputEvents.Player2.Right.started += u => HandleDash(false);
			inputEvents.Player2.Right.canceled += u => inputFrame.right = false;
			inputEvents.Player2.Attack1.started += u => inputFrame.attack = true;
			inputEvents.Player2.Attack1.canceled += u => inputFrame.attack = false;
			inputEvents.Player2.Jump.started += u => inputFrame.jump = true;
			inputEvents.Player2.Jump.canceled += u => inputFrame.jump = false;
		}


		float lastLeftTime;
		float lastRightTime;
		void HandleDash(bool isLeft)
		{
			float timeNow = Time.realtimeSinceStartup;

			if (isLeft)
			{
				if (timeNow - lastLeftTime < .4f)
				{
					inputFrame.isDashLeft = true;
				}

				lastLeftTime = timeNow;
			}
			else
			{
				if (timeNow - lastRightTime < .4f)
				{
					inputFrame.isDashRight = true;
				}

				lastRightTime = timeNow;
			}
		}


		private void Update()
		{
			Vector3 targetDirection = Vector3.zero;

			if (inputFrame.up)
			{
				targetDirection.y = 1;
			}
			if (inputFrame.down)
			{
				targetDirection.y = -1;
			}
			if (inputFrame.left)
			{
				targetDirection.x = -1;
			}
			if (inputFrame.right)
			{
				targetDirection.x = 1;
			}

			if (unitController.isInteracting)
			{
				if (unitController.canDoCombo)
				{
					if (inputFrame.attack)
					{
						unitController.isCombo();
					}
				}

				unitController.UseRootMotion(Time.deltaTime);
			}
			else
			{

				if (targetDirection.x != 0)
				{
					unitController.HandleRotation(targetDirection.x < 0);
				}

				unitController.TickPlayer(Time.deltaTime, targetDirection);
				unitController.DetectAction(inputFrame);
			}

		}



		[System.Serializable]
		public class InputFrame
		{
			public bool left;
			public bool right;
			public bool up;
			public bool down;
			public bool attack;
			public bool jump;
			public bool isDashRight;
			public bool isDashLeft;
		}
	}
}