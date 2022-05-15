using UnityEngine;

namespace AF.UI
{
	public class UIMainScreenScreenController : AFScreen
	{
		public UIMainScreenScreenComponents ScreenView { get; set; }

		private CameraManager cameraManager;
		private GameStateManager gameStateManager;

		private ButtonMaterial ButtonMaterial_1;

		override public void Awake()
		{
			base.Awake();
			ScreenView = GetComponent<UIMainScreenScreenComponents>();
			cameraManager = FindObjectOfType<CameraManager>();
			gameStateManager = FindObjectOfType<GameStateManager>();
		}

		private void Start()
		{
			AddUIWalls();
			GetAllButtonsMaterial();
			SetAllButtonsTexture();
			ScreenView.UIMovementButtonsTopViewButton.onClick.AddListener(ChangeToTopView);
			ScreenView.UIMovementButtonsFreeRoamButton.onClick.AddListener(ChangeToFreeRoam);
			ScreenView.UIMovementButtonsARButton.onClick.AddListener(ChangeToAR);

			ScreenView.UILeftBarMenuImageHolderCreateWallButton.onClick.AddListener(ChangeToCreateWallState);
			ScreenView.UILeftBarMenuImageHolderEditWallButton.onClick.AddListener(ChangeToCostumizeWallState);
			ScreenView.UILeftBarMenuImageHolderMovementButton.onClick.AddListener(ChangeToMovementState);
			
			ScreenView.UICostumizeWallImageColor1.onClick.AddListener(SetWallMaterial_1);
			
			ScreenView.UICostumizeWallImageRInputSlider.onValueChanged.AddListener(SetRWallColor);
			ScreenView.UICostumizeWallImageGInputSlider.onValueChanged.AddListener(SetGWallColor);
			ScreenView.UICostumizeWallImageBInputSlider.onValueChanged.AddListener(SetBWallColor);
		}

		private void SetRWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallImageColor0.color;
			SetRGBWallColor(new Color(value / 255f, currentColor.g, currentColor.b));
		}

		private void SetGWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallImageColor0.color;
			SetRGBWallColor(new Color(currentColor.r, value / 255f, currentColor.b));
		}

		private void SetBWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallImageColor0.color;
			SetRGBWallColor(new Color(currentColor.r, currentColor.g, value / 255f));
		}

		private void SetRGBWallColor(Color color)
		{
			ScreenView.UICostumizeWallImageColor0.color = color;
		}

		private void SetWallMaterial_1()
		{
			SetWallMaterial(ButtonMaterial_1.Material);
		}

		private void SetWallMaterial(Material material)
		{
			App.SelectedPartialWall?.View?.WallFaceController?.SetFaceMaterial(material);
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

		private void GetAllButtonsMaterial()
		{
			ButtonMaterial_1 = ScreenView.UICostumizeWallImageColor1.GetComponent<ButtonMaterial>();
		}

		private void SetAllButtonsTexture()
		{
			ScreenView.UICostumizeWallImageColor1.image.sprite = ButtonMaterial_1.Sprite;
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
