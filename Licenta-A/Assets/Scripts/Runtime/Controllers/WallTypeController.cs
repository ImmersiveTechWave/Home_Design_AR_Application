using UnityEngine;

namespace AF
{
	public class WallTypeController : MonoBehaviour
	{
		public static string FIRST_FACE = "FirstFace";
		public static string SECOND_FACE = "SecondFace";
		public static string MIDDLE_FACE = "MiddleFace";

		public Material FirstMaterial { get; set; }
		public Material SecondMaterial { get; set; }
		public Material MiddleMaterial { get; set; }

		private void Awake()
		{
			FirstMaterial = transform.Find(FIRST_FACE).GetComponent<Renderer>().material;
			SecondMaterial = transform.Find(SECOND_FACE).GetComponent<Renderer>().material;
			MiddleMaterial = transform.Find(MIDDLE_FACE).GetComponent<Renderer>().material;
		}
	}
}
