using UnityEngine;

namespace AF
{
	public class PartialWallController : MonoBehaviour, ISelectable
	{
		public PartialWallView View { get; private set; }

		private void Start()
		{
			View = gameObject.GetComponent<PartialWallView>();
			View.SelectObject.Deselect();
		}

		public void Select()
		{
			if (transform.localScale == Vector3.one)
			{
				if (App.SelectedWall != null)
				{
					App.SelectedWall.DeactivateSelectBox();
					App.SelectedWall = null;
				}

				if (App.SelectedPartialWall != null)
				{
					App.SelectedPartialWall.View.SelectObject.Deselect();
				}
				App.SelectedPartialWall = this;
				App.SelectedPartialWall.View.SelectObject.Select();
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
