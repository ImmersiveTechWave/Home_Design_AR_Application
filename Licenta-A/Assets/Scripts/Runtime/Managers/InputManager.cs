using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AF
{
	public class InputManager : MonoBehaviour
	{
		private void Update()
		{
			Select();
		}

		private void Select()
		{
			if (Input.GetMouseButtonDown(1) && !IsPointerOverUIElement())
			{
				var ray = App.ActiveCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var rayHit, Mathf.Infinity, LayerMask.GetMask(LayersName.WALL)))
				{
					var selectable = rayHit.collider.gameObject.GetComponentInParent<ISelectable>();
					if (selectable != null)
					{
						var simpleWall = rayHit.collider.gameObject.GetComponentInParent<SimpleWallController>();
						if (simpleWall != null)
						{
							simpleWall.Select();
						}
						Debug.Log("An object with name: " + rayHit.collider.gameObject.name + " was selected!");
					}
				}
			}
		}

		public Vector3 GetWorldPoint()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask(LayersName.PLANE)))
			{
				return hit.point;
			}
			return Vector3.zero;
		}

		public bool IsPointerOverUIElement()
		{
			return IsPointerOverUIElement(GetEventSystemRaycastResults());
		}

		private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
		{
			foreach (var result in eventSystemRaysastResults)
			{
				if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
				{
					return true;
				}
			}
			return false;
		}

		private List<RaycastResult> GetEventSystemRaycastResults()
		{
			var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
			return results;
		}
	}
}
