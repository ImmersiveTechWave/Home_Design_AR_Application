using UnityEngine;

namespace AF
{
	public class App : MonoBehaviour
	{
		public static Camera ActiveCamera { get; set; }
		public static PartialWallController SelectedPartialWall;
		public static WallController SelectedWall;
	}
}
