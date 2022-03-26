using UnityEngine;

namespace AF.UI
{
    public class AFScreen : MonoBehaviour
	{
		public ScreenManager ScreenManager { get; set; }

		protected void Awake()
		{
			ScreenManager = FindObjectOfType<ScreenManager>();
		}
	}
}
