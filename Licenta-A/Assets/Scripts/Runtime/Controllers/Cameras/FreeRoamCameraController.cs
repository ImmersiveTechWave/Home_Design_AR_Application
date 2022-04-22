using UnityEngine;

namespace AF
{
	public class FreeRoamCameraController : BaseCameraController
	{
		private const float CAMERA_MOVEMENT_SPEED = 10f;
		private const float CAMERA_ROTATION_SPEED_MULTIPLICATOR = 0.5f;
		private const float MIN_NEW_POSITION = 2f;
		private const float MAX_NEW_POSITION = 50f;

		private bool mouseIsDown;
		private Vector3 startMousePosition = Vector3.zero;

		private void Update()
		{
			if (GameStateManager.IsCurrentState<MovementState>() && ThisIsTheMainCamera)
			{
				ConsumeUserMovementInput();
				ConsumeUserRotationInput();
			}
		}

		private void ConsumeUserMovementInput()
		{
			var newPosition = transform.position;
			if (Input.GetKey(KeyCode.W))
			{
				newPosition += transform.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				newPosition += -transform.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				newPosition += transform.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				newPosition += -transform.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				newPosition += Vector3.up * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				newPosition += -Vector3.up * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			transform.position = newPosition;
		}

		private void ConsumeUserRotationInput()
		{
			if (Input.GetMouseButtonDown(1))
			{
				mouseIsDown = true;
				startMousePosition = Input.mousePosition;
			}

			if (Input.GetMouseButton(1) && mouseIsDown)
			{
				var difference = Input.mousePosition - startMousePosition;
				var offset = new Vector2(difference.x / Screen.width, difference.y / Screen.height) * CAMERA_ROTATION_SPEED_MULTIPLICATOR;
				transform.Rotate(0.0f, offset.x, 0.0f, Space.World);
				transform.Rotate(offset.y, 0.0f, 0.0f, Space.Self);
			}

			if (Input.GetMouseButtonUp(1))
			{
				mouseIsDown = false;
			}
		}

		public override void EnterState()
		{
			base.EnterState();
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
		}

		public override void ExitState()
		{
			base.ExitState();
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UIMovementButtonsFreeRoamButtonText.color = color;
			}
		}
	}
}
