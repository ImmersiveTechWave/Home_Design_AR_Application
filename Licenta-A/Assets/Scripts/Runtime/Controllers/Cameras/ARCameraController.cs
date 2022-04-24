using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

namespace AF
{
	public class ARCameraController : BaseCameraController
	{
		private const float DESKTOP_CAMERA_MOVEMENT_SPEED = 10f;
		private const float DESKTOP_CAMERA_ROTATION_SPEED = 0.5f;
		private const float MOBILE_CAMERA_MOVEMENT_SPEED = 5f;
		private const float MOBILE_CAMERA_ROTATION_SPEED = 20f;

		private bool mouseIsDown;
		private Vector3 startMousePosition = Vector3.zero;
		private ARSessionOrigin aRSessionOrigin;

		public override void Awake()
		{
			base.Awake();
			aRSessionOrigin = GetComponentInParent<ARSessionOrigin>();
		}

		internal struct NullablePose
		{
			internal Vector3? position;
			internal Quaternion? rotation;
		}

		static readonly internal List<XRNodeState> nodeStates = new List<XRNodeState>();

		private void Update()
		{
			/*var updatedPose = GetNodePoseData(XRNode.CenterEye);
			if (updatedPose.position.HasValue)
			{
				Debug.Log("Position: " + updatedPose.position.Value);
			}
			if (updatedPose.rotation.HasValue)
			{
				Debug.Log("Rotation: " + updatedPose.rotation.Value);
			}*/
			if (GameStateManager.IsCurrentState<MovementState>() && ThisIsTheMainCamera)
			{
				ConsumeDesktopUserMovementInput(DESKTOP_CAMERA_MOVEMENT_SPEED, aRSessionOrigin.transform);
				ConsumeDesktopUserRotationInput(DESKTOP_CAMERA_ROTATION_SPEED, aRSessionOrigin.transform);
				ConsumeMobileUserInput();
			}
		}

		private void ConsumeMobileUserInput()
		{
			var newPosition = aRSessionOrigin.transform.position;
			var heightInput = InputController.MoveCamera.MoveHeight.ReadValue<Vector2>();
			var moveInput = InputController.MoveCamera.Move.ReadValue<Vector2>();
			var rotationInput = InputController.MoveCamera.Rotate.ReadValue<Vector2>();

			newPosition.x += moveInput.x * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			newPosition.y += heightInput.y * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			newPosition.z += moveInput.y * MOBILE_CAMERA_MOVEMENT_SPEED * Time.deltaTime;

			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			aRSessionOrigin.transform.position = newPosition;
			aRSessionOrigin.transform.Rotate(0.0f, rotationInput.x * MOBILE_CAMERA_ROTATION_SPEED * Time.deltaTime, 0.0f, Space.World);
			aRSessionOrigin.transform.Rotate(rotationInput.y * MOBILE_CAMERA_ROTATION_SPEED * Time.deltaTime, 0.0f, 0.0f, Space.Self);
		}

		static internal NullablePose GetNodePoseData(XRNode currentNode)
		{
			var resultPose = new NullablePose();
			InputTracking.GetNodeStates(nodeStates);
			foreach (var nodeState in nodeStates)
			{
				if (nodeState.nodeType == currentNode)
				{
					var pose = Pose.identity;
					var positionSuccess = nodeState.TryGetPosition(out pose.position);
					var rotationSuccess = nodeState.TryGetRotation(out pose.rotation);

					if (positionSuccess)
					{
						resultPose.position = pose.position;
					}

					if (rotationSuccess)
					{
						resultPose.rotation = pose.rotation;
					}

					return resultPose;
				}
			}
			return resultPose;
		}

		public override void EnterState()
		{
			base.EnterState();
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			if (GameStateManager.IsCurrentState<CreateWallState>())
			{
				GameStateManager.SwitchState<MovementState>();
			}
			if (GameStateManager.IsCurrentState<MovementState>())
			{
				MainScreen.ScreenView.UIJoysticksRotationJoystick.gameObject.SetActive(true);
			}

			MainScreen.ScreenView.UILeftBarMenuImageHolderCreateWallButton.gameObject.SetActive(false);
		}

		public override void ExitState()
		{
			base.ExitState();
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImageHolderCreateWallButton.gameObject.SetActive(true);
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UIMovementButtonsARButtonText.color = color;
			}
		}
	}
}
