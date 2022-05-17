using UnityEngine;

namespace AF
{
	public class ObjectController : MonoBehaviour
	{
		private InputManager inputManager;
		private GizmoTranslateScript gizmoTranslate;
		private GizmoRotateScript gizmoRotate;
		private ObjectManager objectManager;
		private bool isMoving;

		private void Awake()
		{
			objectManager = FindObjectOfType<ObjectManager>();
			inputManager = FindObjectOfType<InputManager>();
			gizmoTranslate = GetComponentInChildren<GizmoTranslateScript>();
			gizmoRotate = GetComponentInChildren<GizmoRotateScript>();

			gizmoTranslate.translateTarget = gameObject;
			gizmoRotate.rotateTarget = gameObject;
		}

		private void Update()
		{
			if (isMoving)
			{
				transform.position = inputManager.GetWorldPoint();
			}
			if (Input.GetMouseButtonUp(0))
			{
				isMoving = false;
			}
		}

		public void SetTranslateDectectorsCamera(Camera camera)
		{
			gizmoTranslate.Detectors[0].gizmoCamera = camera;
			gizmoTranslate.Detectors[1].gizmoCamera = camera;
			gizmoTranslate.Detectors[2].gizmoCamera = camera;
		}

		public void SetRotateDectectorsCamera(Camera camera)
		{
			gizmoRotate.Detectors[0].gizmoCamera = camera;
			gizmoRotate.Detectors[1].gizmoCamera = camera;
			gizmoRotate.Detectors[2].gizmoCamera = camera;
		}

		public void SetTranslateGizmoState(bool state)
		{
			gizmoTranslate.gameObject.SetActive(state);
		}

		public void SetRotateGizmoState(bool state)
		{
			gizmoRotate.gameObject.SetActive(state);
		}

		public void ObjectWasInstantiated()
		{
			isMoving = true;
			if (objectManager == null)
			{
				objectManager = FindObjectOfType<ObjectManager>();
			}
			transform.SetParent(objectManager.transform);
			objectManager.AddObjectController(this);

			gizmoTranslate.enabled = true;
			gizmoRotate.enabled = true;
			SetTranslateDectectorsCamera(App.ActiveCamera);
			SetRotateDectectorsCamera(App.ActiveCamera);
		}
	}
}
