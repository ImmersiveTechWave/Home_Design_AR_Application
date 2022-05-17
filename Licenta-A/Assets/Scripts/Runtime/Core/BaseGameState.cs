using AF.UI;
using UnityEngine;

namespace AF
{
	public abstract class BaseGameState : MonoBehaviour
	{
		protected UIMainScreenScreenController MainScreen { get; private set; }
		protected GameStateManager GameStateManager { get; private set; }
		protected ObjectManager ObjectManager { get; private set; }

		public virtual void Awake()
		{
			MainScreen = FindObjectOfType<UIMainScreenScreenController>();
			GameStateManager = FindObjectOfType<GameStateManager>();
			ObjectManager = FindObjectOfType<ObjectManager>();
		}

		public virtual void Enter(BaseGameState to) { }

		public virtual void Exit(BaseGameState from) { }
	}
}
