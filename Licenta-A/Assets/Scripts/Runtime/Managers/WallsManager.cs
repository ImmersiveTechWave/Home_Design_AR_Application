using System.Collections.Generic;
using UnityEngine;

namespace AF
{
	public class WallsManager : MonoBehaviour
	{
		private const string WALL_NAME = "Wall_";

		private List<WallController> walls = new List<WallController>();

		public void AddNewWall(WallController wall)
		{
			wall.name = WALL_NAME + walls.Count;
			walls.Add(wall);
			wall.transform.SetParent(transform);
		}
	}
}
