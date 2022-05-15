using UnityEngine;

namespace AF.UI
{
	public class UIMainScreenScreenController : AFScreen
	{
		public UIMainScreenScreenComponents ScreenView { get; set; }

		private CameraManager cameraManager;
		private GameStateManager gameStateManager;

		private ButtonMaterial ButtonMaterial_0;
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

			ScreenView.UICostumizeWallColorColor1.onClick.AddListener(SetWallMaterial_1);

			ScreenView.UICostumizeWallColorRInputSlider.onValueChanged.AddListener(SetRWallColor);
			ScreenView.UICostumizeWallColorGInputSlider.onValueChanged.AddListener(SetGWallColor);
			ScreenView.UICostumizeWallColorBInputSlider.onValueChanged.AddListener(SetBWallColor);

			ScreenView.UICostumizeWallColorSetWallType.onClick.AddListener(ChangeToSetWallType);
			ScreenView.UICostumizeWallTypeSetWallType.onClick.AddListener(ChangeToSetWallType);

			ScreenView.UICostumizeWallColorSetWallColor.onClick.AddListener(ChangeToSetWallColor);
			ScreenView.UICostumizeWallTypeSetWallColor.onClick.AddListener(ChangeToSetWallColor);

			ScreenView.UICostumizeWallColorSetRGBColor.onClick.AddListener(SetRGBWallColor);
		}

		private void ChangeToSetWallType()
		{
			ScreenView.UICostumizeWallColor.gameObject.SetActive(false);
			ScreenView.UICostumizeWallType.gameObject.SetActive(true);
		}

		private void ChangeToSetWallColor()
		{
			ScreenView.UICostumizeWallColor.gameObject.SetActive(true);
			ScreenView.UICostumizeWallType.gameObject.SetActive(false);
		}

		private void SetRWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallColorColor0.color;
			ChangeRGBWallColor(new Color(value / 255f, currentColor.g, currentColor.b));
		}

		private void SetGWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallColorColor0.color;
			ChangeRGBWallColor(new Color(currentColor.r, value / 255f, currentColor.b));
		}

		private void SetBWallColor(float value)
		{
			var currentColor = ScreenView.UICostumizeWallColorColor0.color;
			ChangeRGBWallColor(new Color(currentColor.r, currentColor.g, value / 255f));
		}

		private void ChangeRGBWallColor(Color color)
		{
			ScreenView.UICostumizeWallColorColor0.color = color;
		}

		private void SetRGBWallColor()
		{
			var colorMaterial = new Material(ButtonMaterial_0.Material);
			colorMaterial.color = ScreenView.UICostumizeWallColorColor0.color;
			SetWallMaterial(colorMaterial);
		}

		private void SetWallMaterial_1()
		{
			SetWallMaterial(ButtonMaterial_1.Material);
		}

		private void SetWallMaterial(Material material)
		{
			if (App.SelectedPartialWall != null)
			{
				App.SelectedPartialWall.View?.WallFaceController?.SetFaceMaterial(material);
			}
			else if (App.SelectedWall != null)
			{
				App.SelectedWall.SetFaceMaterial(material);
			}
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
			ButtonMaterial_0 = ScreenView.UICostumizeWallColorColor0.GetComponent<ButtonMaterial>();
			ButtonMaterial_1 = ScreenView.UICostumizeWallColorColor1.GetComponent<ButtonMaterial>();
		}

		private void SetAllButtonsTexture()
		{
			ScreenView.UICostumizeWallColorColor1.image.sprite = ButtonMaterial_1.Sprite;
		}

		private void AddUIWalls()
		{
			var UIWalls = Resources.LoadAll<CostumizeWallController>(WallUIPaths.ALL_WALL_PATH);
			foreach (var wall in UIWalls)
			{
				var wallGO = Instantiate(wall, Vector3.zero, Quaternion.identity);
				wallGO.transform.SetParent(ScreenView.UICostumizeWallTypeViewportContent.transform);
			}
		}
	}
}
