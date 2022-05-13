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
			Select();
		}

		private void Select()
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

						switch (editWallState.SelectedWallType)
						{
							case SelectedWallType.PartialWall:
								var wallCollider = rayHit.collider;

								if (wallCollider.name == WallFaceController.FIRST_FACE)
								{
									App.SelectedWallFace = SelectedWallFace.FirstFace;
								}
								else if (wallCollider.name == WallFaceController.SECOND_FACE)
								{
									App.SelectedWallFace = SelectedWallFace.SecondFace;
								}

								var simpleWall = wallCollider.gameObject.GetComponentInParent<PartialWallController>();
								simpleWall?.Select();
								break;

							case SelectedWallType.EntireWall:
								var completeWall = rayHit.collider.gameObject.GetComponentInParent<WallController>();
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
