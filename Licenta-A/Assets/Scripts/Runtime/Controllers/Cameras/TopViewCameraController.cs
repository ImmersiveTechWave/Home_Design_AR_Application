using UnityEngine;

namespace AF
{
	public class TopViewCameraController : BaseCameraController
	{
		private const float DESKTOP_CAMERA_MOVEMENT_SPEED = 10f;
		private const float MOBILE_CAMERA_MOVEMENT_SPEED = 5f;

		private bool initialRotationJoystickStatus = false;

		private void Update()
		{
			if (GameStateManager.IsCurrentState<MovementState>() && ThisIsTheMainCamera)
			{
				ConsumeDesktopUserMovementInput(DESKTOP_CAMERA_MOVEMENT_SPEED, transform);
				ConsumeMoblieUserMovementInput();
			}
		}

		private void ConsumeMoblieUserMovementInput()
		{
			var newPosition = transform.position;
			var newOrthographicSize = BaseCamera.orthographicSize;
			var heightInput = InputController.MoveCamera.MoveHeight.ReadValue<Vector2>();
			var moveInput = InputController.MoveCamera.Move.ReadValue<Vector2>();

			newOrthographicSize += heightInput.y * Time.deltaTime;
			newPosition.x += moveInput.x * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			newPosition.z += moveInput.y * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;

			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			transform.position = newPosition;
			newOrthographicSize = Mathf.Clamp(newOrthographicSize, MIN_NEW_ORTOGRAPHIC_SIZE, MAX_NEW_ORTOGRAPHIC_SIZE);
			BaseCamera.orthographicSize = newOrthographicSize;
		}

		protected override void ConsumeDesktopUserMovementInput(float cameraMovementSpeed, Transform camera)
		{
			var newPosition = camera.position;
			var newOrthographicSize = BaseCamera.orthographicSize;
			if (Input.GetKey(KeyCode.W))
			{
				newPosition += Vector3.forward * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				newPosition += -Vector3.forward * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				newPosition += Vector3.right * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				newPosition += -Vector3.right * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				newOrthographicSize += cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				newOrthographicSize += -cameraMovementSpeed * Time.deltaTime;
			}
			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			camera.position = newPosition;
			newOrthographicSize = Mathf.Clamp(newOrthographicSize, MIN_NEW_ORTOGRAPHIC_SIZE, MAX_NEW_ORTOGRAPHIC_SIZE);
			BaseCamera.orthographicSize = newOrthographicSize;
		}


		public override void EnterState()
		{
			base.EnterState();
			MainScreen.ScreenView.UIJoysticksRotationJoystick.gameObject.SetActive(false);
			MainScreen.ScreenView.UILeftBarMenuImageHolderEditWallButton.gameObject.SetActive(false);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
		}

		public override void ExitState()
		{
			base.ExitState();
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImageHolderEditWallButton.gameObject.SetActive(true);
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UIMovementButtonsTopViewButtonText.color = color;
			}
		}
	}
}
