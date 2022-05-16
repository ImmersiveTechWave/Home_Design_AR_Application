using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace AF
{
	public class FreeRoamCameraController : BaseCameraController
	{
		private const float DESKTOP_CAMERA_MOVEMENT_SPEED = 10f;
		private const float DESKTOP_CAMERA_ROTATION_SPEED = 0.5f;
		private const float MOBILE_CAMERA_MOVEMENT_SPEED = 5f;
		private const float MOBILE_CAMERA_ROTATION_SPEED = 20f;

		private void Update()
		{
			if (GameStateManager.IsCurrentState<MovementState>() && ThisIsTheMainCamera)
			{
				ConsumeDesktopUserMovementInput(DESKTOP_CAMERA_MOVEMENT_SPEED, transform);
				ConsumeDesktopUserRotationInput(DESKTOP_CAMERA_ROTATION_SPEED, transform);
				ConsumeMobileUserInput();
			}
		}

		private void ConsumeMobileUserInput()
		{
			var heightInput = InputController.MoveCamera.MoveHeight.ReadValue<Vector2>();
			var moveInput = InputController.MoveCamera.Move.ReadValue<Vector2>();
			var rotationInput = InputController.MoveCamera.Rotate.ReadValue<Vector2>();

			var newYPosition = transform.position.y + heightInput.y * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			transform.position += transform.forward * moveInput.y * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			transform.position += transform.right * moveInput.x * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;

			newYPosition = Mathf.Clamp(newYPosition, MIN_NEW_POSITION, MAX_NEW_POSITION);
			transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
			transform.Rotate(0.0f, rotationInput.x * MOBILE_CAMERA_ROTATION_SPEED * Time.deltaTime, 0.0f, Space.World);
			transform.Rotate(rotationInput.y * MOBILE_CAMERA_ROTATION_SPEED * Time.deltaTime, 0.0f, 0.0f, Space.Self);
		}

		public override void EnterState()
		{
			base.EnterState();
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			if (GameStateManager.IsCurrentState<MovementState>())
			{
				MainScreen.ScreenView.UIJoysticksRotationJoystick.gameObject.SetActive(true);
			}
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
