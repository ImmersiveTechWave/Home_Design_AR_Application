using UnityEngine;

namespace AF
{
	public class CameraManager : MonoBehaviour
	{
		private const string TOP_VIEW_CAMERA_NAME = "TopViewCamera";
		private const string FREE_ROAM_CAMERA_NAME = "FreeRoamCamera";
		private const string AR_SESSION_ORIGIN_NAME = "AR Session Origin";
		private const string AR_CAMERA_NAME = "AR Camera";

		public BaseCameraController ActiveCamera { get; private set; }
		public TopViewCameraController TopViewCamera { get; private set; }
		public FreeRoamCameraController FreeRoamCamera { get; private set; }
		public ARCameraController ARCamera { get; private set; }

		private void Awake()
		{
			TopViewCamera = transform.Find(TOP_VIEW_CAMERA_NAME).GetComponent<TopViewCameraController>();
			FreeRoamCamera = transform.Find(FREE_ROAM_CAMERA_NAME).GetComponent<FreeRoamCameraController>();
			ARCamera = transform.Find(AR_SESSION_ORIGIN_NAME).Find(AR_CAMERA_NAME).GetComponent<ARCameraController>();
			ActiveCamera = TopViewCamera;
		}

		private void Start()
		{
			ChangeToTopViewCamera();
		}

		public void ChangeToARCamera()
		{
			ActiveCamera.ExitState();
			ARCamera.EnterState();
			ActiveCamera = ARCamera;
		}

		public void ChangeToTopViewCamera()
		{
			ActiveCamera.ExitState();
			TopViewCamera.EnterState();
			ActiveCamera = TopViewCamera;
		}

		public void ChangeToFreeRoamCamera()
		{
			ActiveCamera.ExitState();
			FreeRoamCamera.EnterState();
			ActiveCamera = FreeRoamCamera;
		}
	}
}
