using UnityEngine;

namespace AF
{
	public enum SelectedWallType
	{
		PartialWall,
		EntireWall
	}

	public class EditWallState : BaseGameState
	{
		public SelectedWallType SelectedWallType { get; set; } = SelectedWallType.PartialWall;

		private void Start()
		{
			MainScreen.ScreenView.UILeftBarMenuImagePartialWallButton.onClick.AddListener(SetPartialWallMode);
			MainScreen.ScreenView.UILeftBarMenuImageEntireWallButton.onClick.AddListener(SetEntireWallMode);
		}

		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImagePartialWallButton.gameObject.SetActive(true);
			MainScreen.ScreenView.UILeftBarMenuImageEntireWallButton.gameObject.SetActive(true);
			MainScreen.ScreenView.UICostumizeWallScrollView.gameObject.SetActive(true);
			SetPartialWallMode();
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);

			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();

			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImagePartialWallButton.gameObject.SetActive(false);
			MainScreen.ScreenView.UILeftBarMenuImageEntireWallButton.gameObject.SetActive(false);
			MainScreen.ScreenView.UICostumizeWallScrollView.gameObject.SetActive(false);
		}

		private void SetPartialWallMode()
		{
			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();
			SelectedWallType = SelectedWallType.PartialWall;
			MainScreen.ScreenView.UILeftBarMenuImageEntireWallButtonText.color = ColorUtils.WHITE_COLOR;
			MainScreen.ScreenView.UILeftBarMenuImagePartialWallButtonText.color = ColorUtils.BLUE_COLOR;
		}

		private void SetEntireWallMode()
		{
			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();
			SelectedWallType = SelectedWallType.EntireWall;
			MainScreen.ScreenView.UILeftBarMenuImagePartialWallButtonText.color = ColorUtils.WHITE_COLOR;
			MainScreen.ScreenView.UILeftBarMenuImageEntireWallButtonText.color = ColorUtils.BLUE_COLOR;
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UILeftBarMenuImageEditWallButtonText.color = color;
			}
		}
	}
}
