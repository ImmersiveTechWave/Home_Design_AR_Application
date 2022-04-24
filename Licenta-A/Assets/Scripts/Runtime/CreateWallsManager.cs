using System.Collections;
using UnityEngine;

namespace AF
{
	public class CreateWallsManager : MonoBehaviour
	{
		private const float WALL_LENGHT = 4f;

		// Managers
		private InputManager inputManager;
		private WallsManager wallsManager;
		private GameStateManager gameStateManager;

		// Resources
		private GameObject startWallResource;
		private GameObject endWallResource;
		private GameObject previewWallResource;
		private PartialWallController partialWallResource;

		// Game Objects
		private GameObject startWallGO;
		private GameObject endWallGO;
		private GameObject previewWallGO;

		private bool isCreating;

		private void Start()
		{
			InitializeManagers();
			InitializeResources();
		}

		private void InitializeManagers()
		{
			inputManager = FindObjectOfType<InputManager>();
			wallsManager = FindObjectOfType<WallsManager>();
			gameStateManager = FindObjectOfType<GameStateManager>();
		}

		private void InitializeResources()
		{
			startWallResource = Resources.Load<GameObject>(WallPaths.START_WALL_PATH);
			endWallResource = Resources.Load<GameObject>(WallPaths.END_WALL_PATH);
			partialWallResource = Resources.Load<PartialWallController>(WallPaths.PARTIAL_WALL_PATH);
			previewWallResource = Resources.Load<GameObject>(WallPaths.PREVIEW_WALL_PATH);
		}

		private void Update()
		{
			if (gameStateManager.IsCurrentState<CreateWallState>())
			{
				DrawWall();
			}
		}

		private void DrawWall()
		{
			if (Input.GetMouseButtonDown(0) && !inputManager.IsPointerOverUIElement() && Physics.Raycast(App.ActiveCamera.ScreenPointToRay(Input.mousePosition), out var rayHit, Mathf.Infinity, LayerMask.GetMask(LayersName.PLANE)))
			{
				startWallGO = Instantiate(startWallResource, Vector3.zero, Quaternion.identity);
				endWallGO = Instantiate(endWallResource, Vector3.zero + new Vector3(1f, 0f, 1f), Quaternion.identity);
				SetStartWallPosition();
			}
			else if (isCreating)
			{
				if (Input.GetMouseButtonUp(0))
				{
					CreateWall();
				}
				else
				{
					CreatePreviewWalls();
				}
			}
		}

		private void SetStartWallPosition()
		{
			previewWallGO = Instantiate(previewWallResource, startWallResource.transform.position, Quaternion.identity);
			var hit = GetBestMarginPosition(inputManager.GetWorldPoint());
			isCreating = true;
			startWallGO.transform.position = hit;
		}

		private void CreateWall()
		{
			isCreating = false;
			endWallGO.transform.position = GetBestMarginPosition(inputManager.GetWorldPoint());

			// Calculate the numbers of completed walls
			var distance = Vector3.Distance(startWallGO.transform.position, endWallGO.transform.position);
			var numberOfWalls = (int)(distance / WALL_LENGHT);
			var difference = distance - numberOfWalls * WALL_LENGHT;

			// Create a wall Controller
			var wallController = new GameObject().AddComponent<WallController>();

			// Create the Left Wall
			var leftWallPosition = startWallGO.transform.position + previewWallGO.transform.forward * (difference / 4);
			var leftWall = Instantiate(partialWallResource, leftWallPosition, previewWallGO.transform.rotation);
			leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, leftWall.transform.localScale.y, difference / 8);
			wallController.AddNewSimpleWalls(leftWall);

			// Create the completed walls
			var wallForward = previewWallGO.transform.forward;
			var startWallPosition = startWallGO.transform.position;
			for (int wallNumber = 0; wallNumber < numberOfWalls; wallNumber++)
			{
				var startPosition = wallForward * (2 * 2 * wallNumber + difference / 2 + 2) + startWallPosition;
				var wall = Instantiate(partialWallResource, startPosition, previewWallGO.transform.rotation);
				wallController.AddNewSimpleWalls(wall);
			}

			// Create the Right Wall
			var rightWallPosition = wallForward * (difference / 2 + difference / 4 + WALL_LENGHT * numberOfWalls) + startWallPosition;
			var rightWall = Instantiate(partialWallResource, rightWallPosition, previewWallGO.transform.rotation);
			rightWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, leftWall.transform.localScale.y, difference / 8);
			wallController.AddNewSimpleWalls(rightWall);

			wallsManager.AddNewWall(wallController);

			Destroy(previewWallGO.gameObject);
			Destroy(startWallGO.gameObject);
			Destroy(endWallGO.gameObject);
		}

		private void CreatePreviewWalls()
		{
			var endWallPosition = GetBestMarginPosition(inputManager.GetWorldPoint());
			endWallGO.transform.position = endWallPosition == startWallGO.transform.position ? endWallPosition + new Vector3(0.1f, 0, 0.1f) : endWallPosition;

			startWallGO.transform.LookAt(endWallGO.transform.position);
			endWallGO.transform.LookAt(startWallGO.transform.position);
			var distance = (startWallGO.transform.position - endWallGO.transform.position).magnitude;

			previewWallGO.transform.position = startWallGO.transform.position + distance / 2 * startWallGO.transform.forward;
			previewWallGO.transform.rotation = startWallGO.transform.rotation;
			previewWallGO.transform.localScale = new Vector3(previewWallGO.transform.localScale.x, previewWallGO.transform.localScale.y, distance);
		}

		private Vector3 GetBestMarginPosition(Vector3 startPosition)
		{
			var hits = Physics.OverlapSphere(startPosition, 1, LayerMask.GetMask(LayersName.WALL));
			var bestMarginPosition = startPosition;
			float minSqrDistance = float.MaxValue;

			foreach (var hit in hits)
			{
				var margin = hit.GetComponent<WallMarginController>();
				if (margin != null)
				{
					var sqrDistance = (margin.transform.position - startPosition).sqrMagnitude;
					if (minSqrDistance > sqrDistance)
					{
						minSqrDistance = sqrDistance;
						bestMarginPosition = margin.transform.position;
					}
				}
			}
			bestMarginPosition.y = startPosition.y;
			return bestMarginPosition;
		}
	}
}
