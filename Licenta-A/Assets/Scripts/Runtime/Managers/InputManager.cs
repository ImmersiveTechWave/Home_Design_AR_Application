using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AF
{
	public class InputManager : MonoBehaviour
	{
		private GameStateManager gameStateManager;

		private void Awake()
		{
			gameStateManager = FindObjectOfType<GameStateManager>();
		}

		private void Update()
		{
			SelectWall();
			SelectObject();
		}

		private void SelectWall()
		{
			if (gameStateManager.IsCurrentState<EditWallState>() && Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
			{
				var ray = App.ActiveCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var rayHit, Mathf.Infinity, LayerMask.GetMask(LayersName.WALL)))
				{
					var selectable = rayHit.collider.gameObject.GetComponentInParent<ISelectable>();
					if (selectable != null)
					{
						var editWallState = gameStateManager.FindState<EditWallState>();

						var wallCollider = rayHit.collider;

						if (wallCollider.name == WallFaceController.FIRST_FACE)
						{
							App.SelectedWallFace = SelectedWallFace.FirstFace;
						}
						else if (wallCollider.name == WallFaceController.SECOND_FACE)
						{
							App.SelectedWallFace = SelectedWallFace.SecondFace;
						}

						switch (editWallState.SelectedWallType)
						{
							case SelectedWallType.PartialWall:
								var simpleWall = wallCollider.gameObject.GetComponentInParent<PartialWallController>();
								simpleWall?.Select();
								break;

							case SelectedWallType.EntireWall:
								var completeWall = wallCollider.gameObject.GetComponentInParent<WallController>();
								completeWall?.Select();
								break;
						}
					}
				}
				else
				{
					if (App.SelectedPartialWall != null)
					{
						App.SelectedPartialWall.View.SelectObject.Deselect();
						App.SelectedPartialWall = null;
					}
					if (App.SelectedWall != null)
					{
						App.SelectedWall.Deselect();
						App.SelectedWall = null;
					}
				}
			}
		}

		private void SelectObject()
		{
			if (gameStateManager.IsCurrentState<AddObjectsState>() && Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
			{
				var ray = App.ActiveCamera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var objectRayHit, Mathf.Infinity, LayerMask.GetMask(LayersName.OBJECT)))
				{
					var selectable = objectRayHit.collider.gameObject.GetComponentInParent<ISelectable>();
					if (selectable != null)
					{
						var objectController = objectRayHit.collider.gameObject.GetComponentInParent<ObjectController>();
						objectController?.Select();
					}
				}
				else
				{
					if (App.SelectedObjectController != null)
					{
						App.SelectedObjectController.Deselect();
						App.SelectedObjectController = null;
					}
				}
			}
		}

		public Vector3 GetWorldPoint()
		{
			var ray = App.ActiveCamera.ScreenPointToRay(Input.mousePosition);
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
