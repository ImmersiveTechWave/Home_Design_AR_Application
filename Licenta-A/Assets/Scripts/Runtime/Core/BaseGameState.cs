using UnityEngine;

namespace AF
{
	public abstract class BaseGameState : MonoBehaviour
	{
		public virtual void Enter(BaseGameState from) { }

		public virtual void Exit(BaseGameState to) { }
	}
}
