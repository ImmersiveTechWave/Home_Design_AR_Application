using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF.UI
{
	public class UIMainScreenScreenController : AFScreen
	{
		private UIMainScreenScreenComponents view;
		private CameraManager cameraManager;

		private void Awake()
		{
			base.Awake();
			view = GetComponent<UIMainScreenScreenComponents>();
			cameraManager = FindObjectOfType<CameraManager>();
		}

		private void Start()
		{
			AddUIWalls();
			view.UITopViewButton.onClick.AddListener(ChangeToTopView);
			view.UIFreeRoamButton.onClick.AddListener(ChangeToFreeRoam);
		}

		private void ChangeToTopView()
		{
			cameraManager.ChangeToTopViewCamera();
		}

		private void ChangeToFreeRoam()
		{
			cameraManager.ChangeToFreeRoamCamera();
		}

		private void AddUIWalls()
		{
			var UIWalls = Resources.LoadAll<CostumizeWallController>(WallUIPaths.ALL_WALL_PATH);
			foreach (var wall in UIWalls)
			{
				var wallGO = Instantiate(wall, Vector3.zero, Quaternion.identity);
				wallGO.transform.SetParent(view.UICostumizeWallScrollViewViewportContent.transform);
			}
		}
	}
}
