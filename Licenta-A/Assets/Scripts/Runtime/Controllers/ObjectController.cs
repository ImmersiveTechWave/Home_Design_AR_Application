using UnityEngine;

namespace AF
{
	public class ObjectController : MonoBehaviour, ISelectable
	{
		private InputManager inputManager;
		private GizmoTranslateScript gizmoTranslate;
		private GizmoRotateScript gizmoRotate;
		private ObjectManager objectManager;
		private bool isMoving;
		private SelectObject selectObject;

		private void Awake()
		{
			objectManager = FindObjectOfType<ObjectManager>();
			inputManager = FindObjectOfType<InputManager>();
			gizmoTranslate = GetComponentInChildren<GizmoTranslateScript>();
			gizmoRotate = GetComponentInChildren<GizmoRotateScript>();
			selectObject = GetComponentInChildren<SelectObject>();

			gizmoTranslate.translateTarget = gameObject;
			gizmoRotate.rotateTarget = gameObject;
		}

		private void Start()
		{
			selectObject.Deselect();
		}

		private void Update()
		{
			if (isMoving)
			{
				transform.position = inputManager.GetWorldPoint();
				if (Input.GetMouseButtonUp(0))
				{
					if (inputManager.IsPointerOverUIElement())
					{
						Destroy(gameObject);
					}
					else
					{
						isMoving = false;
					}
				}
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
			SetTranslateGizmoState(false);
			SetRotateGizmoState(false);
		}

		public void Select()
		{
			if (App.SelectedObjectController != this)
			{
				App.SelectedObjectController?.Deselect();
				if (App.GizmoType == GizmoType.Rotate)
				{
					SetRotateGizmoState(true);
				}
				if (App.GizmoType == GizmoType.Translate)
				{
					SetTranslateGizmoState(true);
				}
				selectObject.Select();
				App.SelectedObjectController = this;
			}
		}

		public void Deselect()
		{
			selectObject.Deselect();
			App.SelectedObjectController = null;
			SetTranslateGizmoState(false);
			SetRotateGizmoState(false);
		}

		private void OnDestroy()
		{
			if (App.SelectedObjectController == this)
			{
				Deselect();
			}
		}
	}
}
