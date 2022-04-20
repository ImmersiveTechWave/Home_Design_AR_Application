using UnityEngine;

namespace AF
{
	public class TopViewCameraController : BaseCameraController
	{
		private const float CAMERA_MOVEMENT_SPEED = 10f;
		private const float MIN_NEW_POSITION = 2f;
		private const float MAX_NEW_POSITION = 50f;
		private const float MIN_NEW_ORTOGRAPHIC_SIZE = 4f;
		private const float MAX_NEW_ORTOGRAPHIC_SIZE = 30f;

		private void Update()
		{
			if (GameStateManager.IsCurrentState<MovementState>() && ThisIsTheMainCamera)
			{
				ConsumeUserMovementInput();
			}
		}

		private void ConsumeUserMovementInput()
		{
			var newPosition = transform.position;
			var newOrthographicSize = BaseCamera.orthographicSize;
			if (Input.GetKey(KeyCode.W))
			{
				newPosition += Vector3.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				newPosition += -Vector3.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				newPosition += Vector3.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				newPosition += -Vector3.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				newOrthographicSize += CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				newOrthographicSize += -CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			transform.position = newPosition;
			newOrthographicSize = Mathf.Clamp(newOrthographicSize, MIN_NEW_ORTOGRAPHIC_SIZE, MAX_NEW_ORTOGRAPHIC_SIZE);
			BaseCamera.orthographicSize = newOrthographicSize;
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
				MainScreen.ScreenView.UIMovementButtonsTopViewButtonText.color = color;
			}
		}
	}
}
