using UnityEngine;

namespace AF.UI
{
	public class UIMainScreenScreenController : AFScreen
	{
		public UIMainScreenScreenComponents ScreenView { get; set; }

		private CameraManager cameraManager;
		private GameStateManager gameStateManager;
		private ObjectManager objectManager;

		private ButtonMaterial ButtonMaterial_0;
		private ButtonMaterial ButtonMaterial_1;
		private ButtonMaterial ButtonMaterial_2;
		private ButtonMaterial ButtonMaterial_3;
		private ButtonMaterial ButtonMaterial_4;
		private ButtonMaterial ButtonMaterial_5;
		private ButtonMaterial ButtonMaterial_6;
		private ButtonMaterial ButtonMaterial_7;
		private ButtonMaterial ButtonMaterial_8;
		private ButtonMaterial ButtonMaterial_9;
		private ButtonMaterial ButtonMaterial_10;
		private ButtonMaterial ButtonMaterial_11;
		private ButtonMaterial ButtonMaterial_12;
		private ButtonMaterial ButtonMaterial_13;
		private ButtonMaterial ButtonMaterial_14;
		private ButtonMaterial ButtonMaterial_15;
		private ButtonMaterial ButtonMaterial_16;
		private ButtonMaterial ButtonMaterial_17;
		private ButtonMaterial ButtonMaterial_18;
		private ButtonMaterial ButtonMaterial_19;

		override public void Awake()
		{
			base.Awake();
			ScreenView = GetComponent<UIMainScreenScreenComponents>();
			cameraManager = FindObjectOfType<CameraManager>();
			gameStateManager = FindObjectOfType<GameStateManager>();
			objectManager = FindObjectOfType<ObjectManager>();
		}

		private void Start()
		{
			AddUIWalls();
			AddUIObjects();
			GetAllButtonsMaterial();
			SetAllButtonsTexture();
			ScreenView.UIMovementButtonsTopViewButton.onClick.AddListener(ChangeToTopView);
			ScreenView.UIMovementButtonsFreeRoamButton.onClick.AddListener(ChangeToFreeRoam);
			ScreenView.UIMovementButtonsARButton.onClick.AddListener(ChangeToAR);

			ScreenView.UILeftBarMenuImageHolderCreateWallButton.onClick.AddListener(ChangeToCreateWallState);
			ScreenView.UILeftBarMenuImageHolderEditWallButton.onClick.AddListener(ChangeToCostumizeWallState);
			ScreenView.UILeftBarMenuImageHolderMovementButton.onClick.AddListener(ChangeToMovementState);
			ScreenView.UILeftBarMenuImageHolderAddObjectsButton.onClick.AddListener(ChangeToAddObject);

			AddColorButtonsListeners();

			ScreenView.UICostumizeWallColorRInputSlider.onValueChanged.AddListener(SetRWallColor);
			ScreenView.UICostumizeWallColorGInputSlider.onValueChanged.AddListener(SetGWallColor);
			ScreenView.UICostumizeWallColorBInputSlider.onValueChanged.AddListener(SetBWallColor);

			ScreenView.UICostumizeWallColorSetWallType.onClick.AddListener(ChangeToSetWallType);
			ScreenView.UICostumizeWallTypeSetWallType.onClick.AddListener(ChangeToSetWallType);

			ScreenView.UICostumizeWallColorSetWallColor.onClick.AddListener(ChangeToSetWallColor);
			ScreenView.UICostumizeWallTypeSetWallColor.onClick.AddListener(ChangeToSetWallColor);

			ScreenView.UICostumizeWallColorSetRGBColor.onClick.AddListener(SetRGBWallColor);
			ScreenView.UIDeleteButton.onClick.AddListener(DeleteWall);

			ScreenView.UIGizmoHolderRotateButton.onClick.AddListener(SetRotateGizmoStatus);
			ScreenView.UIGizmoHolderTranslateButton.onClick.AddListener(SetTranslateGizmoStatus);
		}

		private void SetRotateGizmoStatus()
		{
			if (App.GizmoType == GizmoType.Default || App.GizmoType == GizmoType.Translate)
			{
				ScreenView.UIGizmoHolderTranslateButtonImage.color = ColorUtils.WHITE_COLOR;
				ScreenView.UIGizmoHolderRotateButtonImage.color = ColorUtils.BLUE_COLOR;
				App.GizmoType = GizmoType.Rotate;
				objectManager.SetAllTranslateGizmoState(false);
				objectManager.SetAllRotateGizmoState(true);
			}
			else if (App.GizmoType == GizmoType.Rotate)
			{
				ScreenView.UIGizmoHolderRotateButtonImage.color = ColorUtils.WHITE_COLOR;
				ScreenView.UIGizmoHolderTranslateButtonImage.color = ColorUtils.WHITE_COLOR;
				App.GizmoType = GizmoType.Default;
				objectManager.SetAllRotateGizmoState(false);
			}
		}

		private void SetTranslateGizmoStatus()
		{
			if (App.GizmoType == GizmoType.Default || App.GizmoType == GizmoType.Rotate)
			{
				ScreenView.UIGizmoHolderRotateButtonImage.color = ColorUtils.WHITE_COLOR;
				ScreenView.UIGizmoHolderTranslateButtonImage.color = ColorUtils.BLUE_COLOR;
				App.GizmoType = GizmoType.Translate;
				objectManager.SetAllRotateGizmoState(false);
				objectManager.SetAllTranslateGizmoState(true);
			}
			else if (App.GizmoType == GizmoType.Translate)
			{
				ScreenView.UIGizmoHolderRotateButtonImage.color = ColorUtils.WHITE_COLOR;
				ScreenView.UIGizmoHolderTranslateButtonImage.color = ColorUtils.WHITE_COLOR;
				App.GizmoType = GizmoType.Default;
				objectManager.SetAllTranslateGizmoState(false);
			}
		}

		private void DeleteWall()
		{
			if (App.SelectedWall != null)
			{
				App.SelectedWall.Delete();
			}
		}

		private void ChangeToSetWallType()
		{
			ScreenView.UICostumizeWallColor.gameObject.SetActive(false);
			ScreenView.UICostumizeWallType.gameObject.SetActive(true);
			ScreenView.UICostumizeWallTypeSetWallTypeText.color = ColorUtils.BLUE_COLOR;
			ScreenView.UICostumizeWallTypeSetWallColorText.color = ColorUtils.WHITE_COLOR;
		}

		private void ChangeToSetWallColor()
		{
			ScreenView.UICostumizeWallColor.gameObject.SetActive(true);
			ScreenView.UICostumizeWallType.gameObject.SetActive(false);
			ScreenView.UICostumizeWallColorSetWallColorColorText.color = ColorUtils.BLUE_COLOR;
			ScreenView.UICostumizeWallColorSetWallTypeTypeText.color = ColorUtils.WHITE_COLOR;
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
			ChangeToSetWallType();
		}

		private void ChangeToAddObject()
		{
			gameStateManager.SwitchState<AddObjectsState>();
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

		private void AddColorButtonsListeners()
		{
			ScreenView.UICostumizeWallColorColor1.onClick.AddListener(SetWallMaterial_1);
			ScreenView.UICostumizeWallColorColor2.onClick.AddListener(SetWallMaterial_2);
			ScreenView.UICostumizeWallColorColor3.onClick.AddListener(SetWallMaterial_3);
			ScreenView.UICostumizeWallColorColor4.onClick.AddListener(SetWallMaterial_4);
			ScreenView.UICostumizeWallColorColor5.onClick.AddListener(SetWallMaterial_5);
			ScreenView.UICostumizeWallColorColor6.onClick.AddListener(SetWallMaterial_6);
			ScreenView.UICostumizeWallColorColor7.onClick.AddListener(SetWallMaterial_7);
			ScreenView.UICostumizeWallColorColor8.onClick.AddListener(SetWallMaterial_8);
			ScreenView.UICostumizeWallColorColor9.onClick.AddListener(SetWallMaterial_9);
			ScreenView.UICostumizeWallColorColor10.onClick.AddListener(SetWallMaterial_10);
			ScreenView.UICostumizeWallColorColor11.onClick.AddListener(SetWallMaterial_11);
			ScreenView.UICostumizeWallColorColor12.onClick.AddListener(SetWallMaterial_12);
			ScreenView.UICostumizeWallColorColor13.onClick.AddListener(SetWallMaterial_13);
			ScreenView.UICostumizeWallColorColor14.onClick.AddListener(SetWallMaterial_14);
			ScreenView.UICostumizeWallColorColor15.onClick.AddListener(SetWallMaterial_15);
			ScreenView.UICostumizeWallColorColor16.onClick.AddListener(SetWallMaterial_16);
			ScreenView.UICostumizeWallColorColor17.onClick.AddListener(SetWallMaterial_17);
			ScreenView.UICostumizeWallColorColor18.onClick.AddListener(SetWallMaterial_18);
			ScreenView.UICostumizeWallColorColor19.onClick.AddListener(SetWallMaterial_19);
		}

		private void SetWallMaterial_1()
		{
			SetWallMaterial(ButtonMaterial_1.Material);
		}

		private void SetWallMaterial_2()
		{
			SetWallMaterial(ButtonMaterial_2.Material);
		}

		private void SetWallMaterial_3()
		{
			SetWallMaterial(ButtonMaterial_3.Material);
		}

		private void SetWallMaterial_4()
		{
			SetWallMaterial(ButtonMaterial_4.Material);
		}

		private void SetWallMaterial_5()
		{
			SetWallMaterial(ButtonMaterial_5.Material);
		}

		private void SetWallMaterial_6()
		{
			SetWallMaterial(ButtonMaterial_6.Material);
		}

		private void SetWallMaterial_7()
		{
			SetWallMaterial(ButtonMaterial_7.Material);
		}

		private void SetWallMaterial_8()
		{
			SetWallMaterial(ButtonMaterial_8.Material);
		}

		private void SetWallMaterial_9()
		{
			SetWallMaterial(ButtonMaterial_9.Material);
		}

		private void SetWallMaterial_10()
		{
			SetWallMaterial(ButtonMaterial_10.Material);
		}

		private void SetWallMaterial_11()
		{
			SetWallMaterial(ButtonMaterial_11.Material);
		}

		private void SetWallMaterial_12()
		{
			SetWallMaterial(ButtonMaterial_12.Material);
		}

		private void SetWallMaterial_13()
		{
			SetWallMaterial(ButtonMaterial_13.Material);
		}

		private void SetWallMaterial_14()
		{
			SetWallMaterial(ButtonMaterial_14.Material);
		}

		private void SetWallMaterial_15()
		{
			var newMaterial = ButtonMaterial_15.Material;
			newMaterial.color = ScreenView.UICostumizeWallColorColor15.image.color;
			SetWallMaterial(newMaterial);
		}

		private void SetWallMaterial_16()
		{
			var newMaterial = ButtonMaterial_16.Material;
			newMaterial.color = ScreenView.UICostumizeWallColorColor16.image.color;
			SetWallMaterial(newMaterial);
		}

		private void SetWallMaterial_17()
		{
			SetWallMaterial(ButtonMaterial_17.Material);
		}

		private void SetWallMaterial_18()
		{
			var newMaterial = ButtonMaterial_18.Material;
			newMaterial.color = ScreenView.UICostumizeWallColorColor18.image.color;
			SetWallMaterial(newMaterial);
		}

		private void SetWallMaterial_19()
		{
			var newMaterial = ButtonMaterial_19.Material;
			newMaterial.color = ScreenView.UICostumizeWallColorColor19.image.color;
			SetWallMaterial(newMaterial);
		}

		private void GetAllButtonsMaterial()
		{
			ButtonMaterial_0 = ScreenView.UICostumizeWallColorColor0.GetComponent<ButtonMaterial>();
			ButtonMaterial_1 = ScreenView.UICostumizeWallColorColor1.GetComponent<ButtonMaterial>();
			ButtonMaterial_2 = ScreenView.UICostumizeWallColorColor2.GetComponent<ButtonMaterial>();
			ButtonMaterial_3 = ScreenView.UICostumizeWallColorColor3.GetComponent<ButtonMaterial>();
			ButtonMaterial_4 = ScreenView.UICostumizeWallColorColor4.GetComponent<ButtonMaterial>();
			ButtonMaterial_5 = ScreenView.UICostumizeWallColorColor5.GetComponent<ButtonMaterial>();
			ButtonMaterial_6 = ScreenView.UICostumizeWallColorColor6.GetComponent<ButtonMaterial>();
			ButtonMaterial_7 = ScreenView.UICostumizeWallColorColor7.GetComponent<ButtonMaterial>();
			ButtonMaterial_8 = ScreenView.UICostumizeWallColorColor8.GetComponent<ButtonMaterial>();
			ButtonMaterial_9 = ScreenView.UICostumizeWallColorColor9.GetComponent<ButtonMaterial>();
			ButtonMaterial_10 = ScreenView.UICostumizeWallColorColor10.GetComponent<ButtonMaterial>();
			ButtonMaterial_11 = ScreenView.UICostumizeWallColorColor11.GetComponent<ButtonMaterial>();
			ButtonMaterial_12 = ScreenView.UICostumizeWallColorColor12.GetComponent<ButtonMaterial>();
			ButtonMaterial_13 = ScreenView.UICostumizeWallColorColor13.GetComponent<ButtonMaterial>();
			ButtonMaterial_14 = ScreenView.UICostumizeWallColorColor14.GetComponent<ButtonMaterial>();
			ButtonMaterial_15 = ScreenView.UICostumizeWallColorColor15.GetComponent<ButtonMaterial>();
			ButtonMaterial_16 = ScreenView.UICostumizeWallColorColor16.GetComponent<ButtonMaterial>();
			ButtonMaterial_17 = ScreenView.UICostumizeWallColorColor17.GetComponent<ButtonMaterial>();
			ButtonMaterial_18 = ScreenView.UICostumizeWallColorColor18.GetComponent<ButtonMaterial>();
			ButtonMaterial_19 = ScreenView.UICostumizeWallColorColor19.GetComponent<ButtonMaterial>();
		}

		private void SetAllButtonsTexture()
		{
			ScreenView.UICostumizeWallColorColor1.image.sprite = ButtonMaterial_1.Sprite;
			ScreenView.UICostumizeWallColorColor2.image.sprite = ButtonMaterial_2.Sprite;
			ScreenView.UICostumizeWallColorColor3.image.sprite = ButtonMaterial_3.Sprite;
			ScreenView.UICostumizeWallColorColor4.image.sprite = ButtonMaterial_4.Sprite;
			ScreenView.UICostumizeWallColorColor5.image.sprite = ButtonMaterial_5.Sprite;
			ScreenView.UICostumizeWallColorColor6.image.sprite = ButtonMaterial_6.Sprite;
			ScreenView.UICostumizeWallColorColor7.image.sprite = ButtonMaterial_7.Sprite;
			ScreenView.UICostumizeWallColorColor8.image.sprite = ButtonMaterial_8.Sprite;
			ScreenView.UICostumizeWallColorColor9.image.sprite = ButtonMaterial_9.Sprite;
			ScreenView.UICostumizeWallColorColor10.image.sprite = ButtonMaterial_10.Sprite;
			ScreenView.UICostumizeWallColorColor11.image.sprite = ButtonMaterial_11.Sprite;
			ScreenView.UICostumizeWallColorColor12.image.sprite = ButtonMaterial_12.Sprite;
			ScreenView.UICostumizeWallColorColor13.image.sprite = ButtonMaterial_13.Sprite;
			ScreenView.UICostumizeWallColorColor14.image.sprite = ButtonMaterial_14.Sprite;
			ScreenView.UICostumizeWallColorColor15.image.sprite = ButtonMaterial_15.Sprite;
			ScreenView.UICostumizeWallColorColor16.image.sprite = ButtonMaterial_16.Sprite;
			ScreenView.UICostumizeWallColorColor17.image.sprite = ButtonMaterial_17.Sprite;
			ScreenView.UICostumizeWallColorColor18.image.sprite = ButtonMaterial_18.Sprite;
			ScreenView.UICostumizeWallColorColor19.image.sprite = ButtonMaterial_19.Sprite;
		}

		private void AddUIWalls()
		{
			var UIWalls = Resources.LoadAll<CostumizeWallController>(UIPaths.ALL_UI_WALL_PATH);
			foreach (var wall in UIWalls)
			{
				var wallGO = Instantiate(wall, Vector3.zero, Quaternion.identity);
				wallGO.transform.SetParent(ScreenView.UICostumizeWallTypeViewportContent.transform);
			}
		}

		private void AddUIObjects()
		{
			var UIObjects = Resources.LoadAll<ObjectUIController>(UIPaths.ALL_UI_OBJECTS_PATH);
			foreach (var objects in UIObjects)
			{
				var objectGO = Instantiate(objects, Vector3.zero, Quaternion.identity);
				objectGO.transform.SetParent(ScreenView.UIAddObjectsPanelViewportContent.transform);
			}
		}
	}
}
