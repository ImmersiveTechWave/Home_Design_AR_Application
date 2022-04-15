using UnityEngine;

namespace AF
{
	public abstract class BaseGameState : MonoBehaviour
	{
		public virtual void Enter(BaseGameState to)
		{
			Debug.Log("Enter in " + to?.name.ToString());
		}

		public virtual void Exit(BaseGameState from)
		{
			Debug.Log("Exit from " + from?.name.ToString());
		}
	}
}
