using System.Collections.Generic;
using UnityEngine;

namespace AF
{
	public class WallController : MonoBehaviour
	{
		private const string WALL_NAME = "SimpleWall_";

		private List<SimpleWallController> simpleWalls = new List<SimpleWallController>();

		public void AddNewSimpleWalls(SimpleWallController simpleWall)
		{
			simpleWall.name = WALL_NAME + simpleWalls.Count;
			simpleWalls.Add(simpleWall);
			simpleWall.transform.SetParent(transform);
		}
	}
}
