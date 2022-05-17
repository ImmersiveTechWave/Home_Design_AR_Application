using UnityEngine;

namespace AF
{
	public class App : MonoBehaviour
	{
		public static Camera ActiveCamera { get; set; }
		public static PartialWallController SelectedPartialWall { get; set; }
		public static WallController SelectedWall { get; set; }
		public static SelectedWallFace SelectedWallFace { get; set; } = SelectedWallFace.None;
		public static GizmoType GizmoType { get; set; } = GizmoType.Default;
		public static ObjectController SelectedObjectController { get; set; }
	}
}
