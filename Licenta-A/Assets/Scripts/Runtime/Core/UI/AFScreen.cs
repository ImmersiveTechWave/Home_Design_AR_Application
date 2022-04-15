using UnityEngine;

namespace AF.UI
{
    public class AFScreen : MonoBehaviour
	{
		public ScreenManager ScreenManager { get; set; }

		virtual public void Awake()
		{
			ScreenManager = FindObjectOfType<ScreenManager>();
		}
	}
}
