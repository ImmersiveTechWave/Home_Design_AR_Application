using UnityEngine;

namespace AF
{
	public class MovementState : BaseGameState
	{
		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetButtonsTextColor(ColorUtils.BLUE_COLOR);
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);
			SetButtonsTextColor(ColorUtils.WHITE_COLOR);
		}

		private void SetButtonsTextColor(Color32 color)
		{
			if (MainScreen.ScreenView != null)
			{
				MainScreen.ScreenView.UILeftBarMenuImageMovementButtonText.color = color;
			}
		}
	}
}
