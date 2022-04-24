using UnityEngine;

namespace AF
{
	public class MovementState : BaseGameState
	{
		private CameraManager cameraManager;

		public override void Awake()
		{
			base.Awake();
			cameraManager = FindObjectOfType<CameraManager>();
		}

		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			SetUIElementsStatus(true);
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			SetUIElementsStatus(false);
		}

		private void SetUIElementsStatus(bool status)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UIJoysticksMoveJoystick.gameObject.SetActive(status);
				MainScreen.ScreenView.UILeftBarMenuImageHeightJoystick.gameObject.SetActive(status);
				var rotationJoystickStatus = cameraManager.IsCurrentCamera<TopViewCameraController>() ? false : status;
				MainScreen.ScreenView.UIJoysticksRotationJoystick.gameObject.SetActive(rotationJoystickStatus);
			}
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UILeftBarMenuImageHolderMovementButtonText.color = color;
			}
		}
	}
}
