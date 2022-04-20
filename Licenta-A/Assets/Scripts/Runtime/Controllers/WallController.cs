using System.Collections.Generic;
using UnityEngine;

namespace AF
{
	public class WallController : MonoBehaviour, ISelectable
	{
		private const string WALL_NAME = "SimpleWall_";

		private List<PartialWallController> partialWalls = new List<PartialWallController>();

		public void AddNewSimpleWalls(PartialWallController simpleWall)
		{
			simpleWall.name = WALL_NAME + partialWalls.Count;
			partialWalls.Add(simpleWall);
			simpleWall.transform.SetParent(transform);
		}

		public void ActivateSelectBox()
		{
			foreach (var wall in partialWalls)
			{
				wall.View.SelectObject.Select();
			}
		}

		public void DeactivateSelectBox()
		{
			foreach (var wall in partialWalls)
			{
				wall.View.SelectObject.Deselect();
			}
		}

		public void Select()
		{
			if (transform.localScale == Vector3.one)
			{
				if (App.SelectedPartialWall != null)
				{
					App.SelectedPartialWall.View.SelectObject.Deselect();
					App.SelectedPartialWall = null;
				}

				if (App.SelectedWall != null)
				{
					App.SelectedWall.DeactivateSelectBox();
				}
				App.SelectedWall = this;
				App.SelectedWall.ActivateSelectBox();
			}
		}

		public void Deselect()
		{
			if (App.SelectedWall != null)
			{
				App.SelectedWall.DeactivateSelectBox();
				App.SelectedWall = null;
			}

			if (App.SelectedPartialWall != null)
			{
				App.SelectedPartialWall.View.SelectObject.Deselect();
				App.SelectedPartialWall = null;
			}
		}
	}
}
