using UnityEngine;

namespace AF.UI
{
	public class UIMainScreenScreenController : AFScreen
	{
		public UIMainScreenScreenComponents ScreenView { get; set; }

		private CameraManager cameraManager;
		private GameStateManager gameStateManager;

		private void Awake()
		{
			base.Awake();
			ScreenView = GetComponent<UIMainScreenScreenComponents>();
			cameraManager = FindObjectOfType<CameraManager>();
			gameStateManager = FindObjectOfType<GameStateManager>();
		}

		private void Start()
		{
			AddUIWalls();
			ScreenView.UIMovementButtonsTopViewButton.onClick.AddListener(ChangeToTopView);
			ScreenView.UIMovementButtonsFreeRoamButton.onClick.AddListener(ChangeToFreeRoam);
			ScreenView.UIMovementButtonsARButton.onClick.AddListener(ChangeToAR);

			ScreenView.UILeftBarMenuImageCreateWallButton.onClick.AddListener(ChangeToCreateWallState);
			ScreenView.UILeftBarMenuImageEditWallButton.onClick.AddListener(ChangeToCostumizeWallState);
			ScreenView.UILeftBarMenuImageMovementButton.onClick.AddListener(ChangeToMovementState);
		}

		private void ChangeToCostumizeWallState()
		{
			gameStateManager.SwitchState<EditWallState>();
		}

		private void ChangeToMovementState()
		{
			gameStateManager.SwitchState<MovementState>();
		}

		private void ChangeToCreateWallState()
		{
			gameStateManager.SwitchState<CreateWallState>();
		}

		private void ChangeToAR()
		{
			cameraManager.ChangeToARCamera();
		}

		private void ChangeToTopView()
		{
			cameraManager.ChangeToTopViewCamera();
		}

		private void ChangeToFreeRoam()
		{
			cameraManager.ChangeToFreeRoamCamera();
		}

		private void AddUIWalls()
		{
			var UIWalls = Resources.LoadAll<CostumizeWallController>(WallUIPaths.ALL_WALL_PATH);
			foreach (var wall in UIWalls)
			{
				var wallGO = Instantiate(wall, Vector3.zero, Quaternion.identity);
				wallGO.transform.SetParent(ScreenView.UICostumizeWallScrollViewViewportContent.transform);
			}
		}
	}
}
