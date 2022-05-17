using AF.UI;
using UnityEngine;

namespace AF
{
	public abstract class BaseCameraController : MonoBehaviour
	{
		protected const float MIN_NEW_POSITION = 2f;
		protected const float MAX_NEW_POSITION = 50f;
		protected const float MIN_NEW_ORTOGRAPHIC_SIZE = 4f;
		protected const float MAX_NEW_ORTOGRAPHIC_SIZE = 30f;

		public Camera BaseCamera { get; private set; }
		public bool ThisIsTheMainCamera { get; set; }
		public GameStateManager GameStateManager { get; private set; }
		public InputController InputController { get; private set; }
		public ObjectManager ObjectManager { get; private set; }

		protected UIMainScreenScreenController MainScreen { get; private set; }

		private bool mouseIsDown;
		private Vector3 startMousePosition = Vector3.zero;

		public virtual void Awake()
		{
			BaseCamera = GetComponent<Camera>();
			GameStateManager = FindObjectOfType<GameStateManager>();
			MainScreen = FindObjectOfType<UIMainScreenScreenController>();
			ObjectManager = FindObjectOfType<ObjectManager>();
			InputController = new InputController();
			InputController.Enable();
		}

		public virtual void EnterState()
		{
			ThisIsTheMainCamera = true;
			BaseCamera.enabled = true;
			App.ActiveCamera = BaseCamera;
			ObjectManager.SetAllTranslateDetectorsCamera(BaseCamera);
			ObjectManager.SetAllRotateDectectorsCamera(BaseCamera);
		}

		public virtual void ExitState()
		{
			ThisIsTheMainCamera = false;
			BaseCamera.enabled = false;
		}

		protected virtual void ConsumeDesktopUserMovementInput(float cameraMovementSpeed, Transform camera)
		{
			var newPosition = camera.position;
			if (Input.GetKey(KeyCode.W))
			{
				newPosition += camera.forward * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				newPosition += -camera.forward * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				newPosition += camera.right * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				newPosition += -camera.right * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				newPosition += Vector3.up * cameraMovementSpeed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				newPosition += -Vector3.up * cameraMovementSpeed * Time.deltaTime;
			}
			newPosition.y = Mathf.Clamp(newPosition.y, MIN_NEW_POSITION, MAX_NEW_POSITION);
			camera.position = newPosition;
		}

		protected virtual void ConsumeDesktopUserRotationInput(float cameraRotationSpeed, Transform camera)
		{
			if (Input.GetMouseButtonDown(1))
			{
				mouseIsDown = true;
				startMousePosition = Input.mousePosition;
			}

			if (Input.GetMouseButton(1) && mouseIsDown)
			{
				var difference = Input.mousePosition - startMousePosition;
				var offset = new Vector2(difference.x / Screen.width, difference.y / Screen.height) * cameraRotationSpeed;
				camera.Rotate(0.0f, offset.x, 0.0f, Space.World);
				camera.Rotate(offset.y, 0.0f, 0.0f, Space.Self);
			}

			if (Input.GetMouseButtonUp(1))
			{
				mouseIsDown = false;
			}
		}
	}
}
