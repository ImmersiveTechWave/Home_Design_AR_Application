using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

namespace AF.UI
{
public class UIMainScreenScreenComponents : MonoBehaviour
{
	public Image UILeftBarMenuImage{ get; protected set; }
	public Button UILeftBarMenuImageMenuButton{ get; protected set; }
	public Button UILeftBarMenuImageMovementButton{ get; protected set; }
	public TextMeshProUGUI UILeftBarMenuImageMovementButtonText{ get; protected set; }
	public Button UILeftBarMenuImageCreateWallButton{ get; protected set; }
	public TextMeshProUGUI UILeftBarMenuImageCreateWallButtonText{ get; protected set; }
	public Button UILeftBarMenuImageEditWallButton{ get; protected set; }
	public TextMeshProUGUI UILeftBarMenuImageEditWallButtonText{ get; protected set; }
	public Button UILeftBarMenuImagePartialWallButton{ get; protected set; }
	public TextMeshProUGUI UILeftBarMenuImagePartialWallButtonText{ get; protected set; }
	public Button UILeftBarMenuImageEntireWallButton{ get; protected set; }
	public TextMeshProUGUI UILeftBarMenuImageEntireWallButtonText{ get; protected set; }
	public ScrollRect UICostumizeWallScrollView{ get; protected set; }
	public Image UICostumizeWallScrollViewViewport{ get; protected set; }
	public RectTransform UICostumizeWallScrollViewViewportContent{ get; protected set; }
	public Scrollbar UICostumizeWallScrollViewScrollbarHorizontal{ get; protected set; }
	public Image UICostumizeWallImage{ get; protected set; }
	public Image UICostumizeWallImageColor0{ get; protected set; }
	public Button UICostumizeWallImageColor1{ get; protected set; }
	public Button UICostumizeWallImageColor2{ get; protected set; }
	public Button UICostumizeWallImageColor3{ get; protected set; }
	public Button UICostumizeWallImageColor4{ get; protected set; }
	public Button UICostumizeWallImageColor5{ get; protected set; }
	public Button UICostumizeWallImageColor6{ get; protected set; }
	public Button UICostumizeWallImageColor7{ get; protected set; }
	public Button UICostumizeWallImageColor8{ get; protected set; }
	public Button UICostumizeWallImageColor9{ get; protected set; }
	public Button UICostumizeWallImageColor10{ get; protected set; }
	public Button UICostumizeWallImageColor11{ get; protected set; }
	public Button UICostumizeWallImageColor12{ get; protected set; }
	public Button UICostumizeWallImageColor13{ get; protected set; }
	public Button UICostumizeWallImageColor14{ get; protected set; }
	public Button UICostumizeWallImageColor15{ get; protected set; }
	public Button UICostumizeWallImageColor16{ get; protected set; }
	public Button UICostumizeWallImageColor17{ get; protected set; }
	public Button UICostumizeWallImageColor18{ get; protected set; }
	public Button UICostumizeWallImageColor19{ get; protected set; }
	public InputSliderComponent UICostumizeWallImageRInputSlider{ get; protected set; }
	public InputSliderComponent UICostumizeWallImageGInputSlider{ get; protected set; }
	public InputSliderComponent UICostumizeWallImageBInputSlider{ get; protected set; }
	public Button UICostumizeWallImageMinimizeButton{ get; protected set; }
	public RectTransform UIMovementButtons{ get; protected set; }
	public Button UIMovementButtonsTopViewButton{ get; protected set; }
	public TextMeshProUGUI UIMovementButtonsTopViewButtonText{ get; protected set; }
	public Button UIMovementButtonsFreeRoamButton{ get; protected set; }
	public TextMeshProUGUI UIMovementButtonsFreeRoamButtonText{ get; protected set; }
	public Button UIMovementButtonsARButton{ get; protected set; }
	public TextMeshProUGUI UIMovementButtonsARButtonText{ get; protected set; }

	private void Awake()
	{
		UILeftBarMenuImage = transform.Find("LeftBarMenuImage").GetComponent<Image>();
		UILeftBarMenuImageMenuButton = transform.Find("LeftBarMenuImage/MenuButton").GetComponent<Button>();
		UILeftBarMenuImageMovementButton = transform.Find("LeftBarMenuImage/MovementButton").GetComponent<Button>();
		UILeftBarMenuImageMovementButtonText = transform.Find("LeftBarMenuImage/MovementButton/Text").GetComponent<TextMeshProUGUI>();
		UILeftBarMenuImageCreateWallButton = transform.Find("LeftBarMenuImage/CreateWallButton").GetComponent<Button>();
		UILeftBarMenuImageCreateWallButtonText = transform.Find("LeftBarMenuImage/CreateWallButton/Text").GetComponent<TextMeshProUGUI>();
		UILeftBarMenuImageEditWallButton = transform.Find("LeftBarMenuImage/EditWallButton").GetComponent<Button>();
		UILeftBarMenuImageEditWallButtonText = transform.Find("LeftBarMenuImage/EditWallButton/Text").GetComponent<TextMeshProUGUI>();
		UILeftBarMenuImagePartialWallButton = transform.Find("LeftBarMenuImage/PartialWallButton").GetComponent<Button>();
		UILeftBarMenuImagePartialWallButtonText = transform.Find("LeftBarMenuImage/PartialWallButton/Text").GetComponent<TextMeshProUGUI>();
		UILeftBarMenuImageEntireWallButton = transform.Find("LeftBarMenuImage/EntireWallButton").GetComponent<Button>();
		UILeftBarMenuImageEntireWallButtonText = transform.Find("LeftBarMenuImage/EntireWallButton/Text").GetComponent<TextMeshProUGUI>();
		UICostumizeWallScrollView = transform.Find("CostumizeWallScrollView").GetComponent<ScrollRect>();
		UICostumizeWallScrollViewViewport = transform.Find("CostumizeWallScrollView/Viewport").GetComponent<Image>();
		UICostumizeWallScrollViewViewportContent = transform.Find("CostumizeWallScrollView/Viewport/Content").GetComponent<RectTransform>();
		UICostumizeWallScrollViewScrollbarHorizontal = transform.Find("CostumizeWallScrollView/Scrollbar Horizontal").GetComponent<Scrollbar>();
		UICostumizeWallImage = transform.Find("CostumizeWallImage").GetComponent<Image>();
		UICostumizeWallImageColor0 = transform.Find("CostumizeWallImage/Color0").GetComponent<Image>();
		UICostumizeWallImageColor1 = transform.Find("CostumizeWallImage/Color1").GetComponent<Button>();
		UICostumizeWallImageColor2 = transform.Find("CostumizeWallImage/Color2").GetComponent<Button>();
		UICostumizeWallImageColor3 = transform.Find("CostumizeWallImage/Color3").GetComponent<Button>();
		UICostumizeWallImageColor4 = transform.Find("CostumizeWallImage/Color4").GetComponent<Button>();
		UICostumizeWallImageColor5 = transform.Find("CostumizeWallImage/Color5").GetComponent<Button>();
		UICostumizeWallImageColor6 = transform.Find("CostumizeWallImage/Color6").GetComponent<Button>();
		UICostumizeWallImageColor7 = transform.Find("CostumizeWallImage/Color7").GetComponent<Button>();
		UICostumizeWallImageColor8 = transform.Find("CostumizeWallImage/Color8").GetComponent<Button>();
		UICostumizeWallImageColor9 = transform.Find("CostumizeWallImage/Color9").GetComponent<Button>();
		UICostumizeWallImageColor10 = transform.Find("CostumizeWallImage/Color10").GetComponent<Button>();
		UICostumizeWallImageColor11 = transform.Find("CostumizeWallImage/Color11").GetComponent<Button>();
		UICostumizeWallImageColor12 = transform.Find("CostumizeWallImage/Color12").GetComponent<Button>();
		UICostumizeWallImageColor13 = transform.Find("CostumizeWallImage/Color13").GetComponent<Button>();
		UICostumizeWallImageColor14 = transform.Find("CostumizeWallImage/Color14").GetComponent<Button>();
		UICostumizeWallImageColor15 = transform.Find("CostumizeWallImage/Color15").GetComponent<Button>();
		UICostumizeWallImageColor16 = transform.Find("CostumizeWallImage/Color16").GetComponent<Button>();
		UICostumizeWallImageColor17 = transform.Find("CostumizeWallImage/Color17").GetComponent<Button>();
		UICostumizeWallImageColor18 = transform.Find("CostumizeWallImage/Color18").GetComponent<Button>();
		UICostumizeWallImageColor19 = transform.Find("CostumizeWallImage/Color19").GetComponent<Button>();
		UICostumizeWallImageRInputSlider = transform.Find("CostumizeWallImage/RInputSlider").GetComponent<InputSliderComponent>();
		UICostumizeWallImageGInputSlider = transform.Find("CostumizeWallImage/GInputSlider").GetComponent<InputSliderComponent>();
		UICostumizeWallImageBInputSlider = transform.Find("CostumizeWallImage/BInputSlider").GetComponent<InputSliderComponent>();
		UICostumizeWallImageMinimizeButton = transform.Find("CostumizeWallImage/MinimizeButton").GetComponent<Button>();
		UIMovementButtons = transform.Find("MovementButtons").GetComponent<RectTransform>();
		UIMovementButtonsTopViewButton = transform.Find("MovementButtons/TopViewButton").GetComponent<Button>();
		UIMovementButtonsTopViewButtonText = transform.Find("MovementButtons/TopViewButton/Text").GetComponent<TextMeshProUGUI>();
		UIMovementButtonsFreeRoamButton = transform.Find("MovementButtons/FreeRoamButton").GetComponent<Button>();
		UIMovementButtonsFreeRoamButtonText = transform.Find("MovementButtons/FreeRoamButton/Text").GetComponent<TextMeshProUGUI>();
		UIMovementButtonsARButton = transform.Find("MovementButtons/ARButton").GetComponent<Button>();
		UIMovementButtonsARButtonText = transform.Find("MovementButtons/ARButton/Text").GetComponent<TextMeshProUGUI>();
	}
}
}
