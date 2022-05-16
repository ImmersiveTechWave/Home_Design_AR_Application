using UnityEngine;

namespace AF
{
	public enum SelectedWallType
	{
		PartialWall,
		EntireWall
	}

	public enum SelectedWallFace
	{
		FirstFace,
		SecondFace,
		None
	}

	public class EditWallState : BaseGameState
	{
		public SelectedWallType SelectedWallType { get; set; } = SelectedWallType.PartialWall;

		private void Start()
		{
			MainScreen.ScreenView.UILeftBarMenuImageHolderPartialWallButton.onClick.AddListener(SetPartialWallMode);
			MainScreen.ScreenView.UILeftBarMenuImageHolderEntireWallButton.onClick.AddListener(SetEntireWallMode);
		}

		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImageHolderPartialWallButton.gameObject.SetActive(true);
			MainScreen.ScreenView.UILeftBarMenuImageHolderEntireWallButton.gameObject.SetActive(true);
			MainScreen.ScreenView.UICostumizeWallType.gameObject.SetActive(true);
			SetPartialWallMode();
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);

			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();

			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			MainScreen.ScreenView.UILeftBarMenuImageHolderPartialWallButton.gameObject.SetActive(false);
			MainScreen.ScreenView.UILeftBarMenuImageHolderEntireWallButton.gameObject.SetActive(false);
			MainScreen.ScreenView.UICostumizeWallType.gameObject.SetActive(false);
			MainScreen.ScreenView.UICostumizeWallColor.gameObject.SetActive(false);
		}

		private void SetPartialWallMode()
		{
			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();
			SelectedWallType = SelectedWallType.PartialWall;
			MainScreen.ScreenView.UILeftBarMenuImageHolderEntireWallButtonText.color = ColorUtils.WHITE_COLOR;
			MainScreen.ScreenView.UILeftBarMenuImageHolderPartialWallButtonText.color = ColorUtils.BLUE_COLOR;
			MainScreen.ScreenView.UIDeleteButton.gameObject.SetActive(false);
		}

		private void SetEntireWallMode()
		{
			App.SelectedWall?.Deselect();
			App.SelectedPartialWall?.Deselect();
			SelectedWallType = SelectedWallType.EntireWall;
			MainScreen.ScreenView.UILeftBarMenuImageHolderPartialWallButtonText.color = ColorUtils.WHITE_COLOR;
			MainScreen.ScreenView.UILeftBarMenuImageHolderEntireWallButtonText.color = ColorUtils.BLUE_COLOR;
			MainScreen.ScreenView.UIDeleteButton.gameObject.SetActive(true);
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UILeftBarMenuImageHolderEditWallButtonText.color = color;
			}
		}
	}
}
