using UnityEngine;

namespace AF
{
	public class ObjectController : MonoBehaviour
	{
		private InputManager inputManager;
		private bool isMoving;

		private void Start()
		{
			inputManager = FindObjectOfType<InputManager>();
		}

		private void Update()
		{
			if (isMoving)
			{
				transform.position = inputManager.GetWorldPoint();
			}
			if (Input.GetMouseButtonUp(0))
			{
				isMoving = false;
			}
		}

		public void ObjectWasInstantiated()
		{
			isMoving = true;
		}
	}
}
