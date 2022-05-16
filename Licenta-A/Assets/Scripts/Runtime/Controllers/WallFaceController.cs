using UnityEngine;

namespace AF
{
	public class WallFaceController : MonoBehaviour
	{
		public static string FIRST_FACE = "FirstFace";
		public static string SECOND_FACE = "SecondFace";
		public static string MIDDLE_FACE = "MiddleFace";

		public Renderer FirstFaceRenderer { get; private set; }
		public Renderer SecondFaceRenderer { get; private set; }
		public Renderer MiddleFaceRenderer { get; private set; }

		private void Awake()
		{
			FirstFaceRenderer = transform.Find(FIRST_FACE).GetComponent<Renderer>();
			SecondFaceRenderer = transform.Find(SECOND_FACE).GetComponent<Renderer>();
			MiddleFaceRenderer = transform.Find(MIDDLE_FACE).GetComponent<Renderer>();
		}

		public void SetFaceMaterial(Material material)
		{
			switch (App.SelectedWallFace)
			{
				case SelectedWallFace.FirstFace:
					FirstFaceRenderer.material = new Material(material);
					break;
				case SelectedWallFace.SecondFace:
					SecondFaceRenderer.material = new Material(material);
					break;
			}
		}

		public void SetFirstFaceMaterial(Material material)
		{
			FirstFaceRenderer.material = new Material(material);
		}

		public void SetSecondFaceMaterial(Material material)
		{
			SecondFaceRenderer.material = new Material(material);
		}
	}
}
