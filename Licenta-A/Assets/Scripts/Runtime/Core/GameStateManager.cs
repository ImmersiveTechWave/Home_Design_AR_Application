using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace AF
{
	public class GameStateManager : MonoBehaviour
	{
		public readonly Type DEFAULT_GAME_STATE = typeof(MovementState);

		public static GameStateManager Instance { get { return instance; } }

		public Type CurrentState { get; private set; }
		public event Action<BaseGameState> GameStateChanged;

		private static GameStateManager instance;
		private Stack<BaseGameState> gameStateStack = new Stack<BaseGameState>();
		private Dictionary<Type, BaseGameState> stateDictionary = new Dictionary<Type, BaseGameState>();


		public void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				if (instance != this)
				{
					Destroy(gameObject);
				}
			}

			InitGameStates();
		}

		private void InitGameStates()
		{
			var baseStateType = typeof(BaseGameState);
			Func<Type, bool> baseStatePredicate;

			baseStatePredicate = t => { return !t.IsInterface && !t.IsAbstract && baseStateType.IsAssignableFrom(t); };

			stateDictionary.Clear();
			var gameStateTypes = TypesUtils.GetTypes(baseStatePredicate);
			foreach (var type in gameStateTypes)
			{
				try
				{
					var addedState = new GameObject(type.Name).AddComponent(type);
					addedState.transform.SetParent(transform);

					var gameState = addedState as BaseGameState;
					stateDictionary.Add(type, gameState);
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			// Init with the default state.
			PushState(gameStateTypes.FirstOrDefault(p => p == DEFAULT_GAME_STATE));
			SwitchState(DEFAULT_GAME_STATE);
		}

		public void SwitchState<T>() where T : BaseGameState
		{
			SwitchState(typeof(T));
		}

		public void SwitchState(Type type)
		{
			var gameState = FindState(type);

			if (gameState == null)
			{
				return;
			}

			var previousState = gameStateStack.Count > 0 ? gameStateStack.Pop() : null;
			gameStateStack.Push(gameState);

			previousState?.Exit(gameState);
			gameState.Enter(previousState);
			CurrentState = type;
			GameStateChanged?.Invoke(gameState);
		}

		public bool IsCurrentState<T>() where T : BaseGameState
		{
			return IsCurrentState(typeof(T));
		}

		public bool IsCurrentState(Type state)
		{
			return CurrentState == state;
		}

		private BaseGameState FindState(Type type)
		{
			if (!stateDictionary.TryGetValue(type, out var state))
			{
				return null;
			}
			return state;
		}

		public T FindState<T>() where T: BaseGameState
		{
			return FindState(typeof(T)) as T;
		}

		private void PushState(Type type)
		{
			if (!stateDictionary.TryGetValue(type, out var state))
			{
				return;
			}

			if (gameStateStack.Count > 0)
			{
				gameStateStack.Peek().Exit(state);
				state.Enter(gameStateStack.Peek());
			}
			else
			{
				state.Enter(null);
			}
			gameStateStack.Push(state);
		}
	}
}
