using UnityEngine;

namespace AF
{
	public abstract class BaseCameraController : MonoBehaviour
	{
		public Camera BaseCamera { get; private set; }
		public bool ThisIsTheMainCamera { get; set; }
		public GameStateManager GameStateManager { get; private set; }

		private void Awake()
		{
			GameStateManager = FindObjectOfType<GameStateManager>();
		}

		private void Start()
		{
			BaseCamera = GetComponent<Camera>();
		}

		public virtual void EnterState()
		{
			ThisIsTheMainCamera = true;
			BaseCamera.enabled = true;
		}

		public virtual void ExitState()
		{
			ThisIsTheMainCamera = false;
			BaseCamera.enabled = false;
		}
	}
}
