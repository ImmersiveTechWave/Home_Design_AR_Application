using UnityEngine;

namespace AF
{
	public class TopViewCameraController : BaseCameraController
	{
		private const float CAMERA_MOVEMENT_SPEED = 10f;

		private void Update()
		{
			if (ThisIsTheMainCamera)
			{
				ConsumeUserMovementInput();
			}
		}

		private void ConsumeUserMovementInput()
		{
			if (Input.GetKey(KeyCode.W))
			{
				transform.position += Vector3.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				transform.position += -Vector3.forward * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.position += Vector3.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				transform.position += -Vector3.right * CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				BaseCamera.orthographicSize += CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				BaseCamera.orthographicSize += -CAMERA_MOVEMENT_SPEED * Time.deltaTime;
			}
		}

		public override void EnterState()
		{
			base.EnterState();
		}

		public override void ExitState()
		{
			base.ExitState();
		}
	}
}
