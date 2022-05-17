using System.Collections.Generic;
using UnityEngine;

namespace AF
{
	public class ObjectManager : MonoBehaviour
	{
		private List<ObjectController> objectControllers = new List<ObjectController>();

		public void AddObjectController(ObjectController objectController)
		{
			objectControllers.Add(objectController);
		}

		public void SetAllTranslateDetectorsCamera(Camera camera)
		{
			foreach (var objectController in objectControllers)
			{
				objectController.SetTranslateDectectorsCamera(camera);
			}
		}

		public void SetAllRotateDectectorsCamera(Camera camera)
		{
			foreach (var objectController in objectControllers)
			{
				objectController.SetRotateDectectorsCamera(camera);
			}
		}

		public void SetAllTranslateGizmoState(bool state)
		{
			foreach (var objectController in objectControllers)
			{
				objectController.SetTranslateGizmoState(state);
			}
		}

		public void SetAllRotateGizmoState(bool state)
		{
			foreach (var objectController in objectControllers)
			{
				objectController.SetRotateGizmoState(state);
			}
		}
	}
}
