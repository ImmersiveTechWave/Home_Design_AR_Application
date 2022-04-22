using AF.UI;
using UnityEngine;

namespace AF
{
	public abstract class BaseCameraController : MonoBehaviour
	{
		public Camera BaseCamera { get; private set; }
		public bool ThisIsTheMainCamera { get; set; }
		public GameStateManager GameStateManager { get; private set; }

		//
		protected UIMainScreenScreenController MainScreen { get; private set; }

		private void Awake()
		{
			GameStateManager = FindObjectOfType<GameStateManager>();
			MainScreen = FindObjectOfType<UIMainScreenScreenController>();
		}

		private void Start()
		{
			BaseCamera = GetComponent<Camera>();
		}

		public virtual void EnterState()
		{
			ThisIsTheMainCamera = true;
			BaseCamera.enabled = true;
			App.ActiveCamera = BaseCamera;
		}

		public virtual void ExitState()
		{
			ThisIsTheMainCamera = false;
			BaseCamera.enabled = false;
		}
	}
}
