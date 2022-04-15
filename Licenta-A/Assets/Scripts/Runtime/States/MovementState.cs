using AF.UI;

namespace AF
{
	public class MovementState : BaseGameState
	{
		private UIMainScreenScreenController mainScreen;

		private void Awake()
		{
			mainScreen = FindObjectOfType<UIMainScreenScreenController>();
		}

		public override void Enter(BaseGameState to)
		{
			base.Enter(to);
			SetMovementButtonState(true);
		}

		public override void Exit(BaseGameState from)
		{
			base.Exit(from);
			SetMovementButtonState(false);
		}

		private void SetMovementButtonState(bool state)
		{
			mainScreen.ScreenView?.UIMovementButtonsFreeRoamButton.gameObject.SetActive(state);
			mainScreen.ScreenView?.UIMovementButtonsTopViewButton.gameObject.SetActive(state);
			mainScreen.ScreenView?.UIMovementButtonsARButton.gameObject.SetActive(state);
		}
	}
}
