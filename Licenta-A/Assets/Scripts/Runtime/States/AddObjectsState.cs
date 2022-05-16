using UnityEngine;

namespace AF
{
    public class AddObjectsState : BaseGameState
    {
		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
			SetUIElementsStatus(true);
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
			SetUIElementsStatus(false);
		}

		private void SetUIElementsStatus(bool status)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UIAddObjectsPanel.gameObject.SetActive(status);
			}
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UILeftBarMenuImageHolderAddObjectsButtonText.color = color;
			}
		}
	}
}
