using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF.UI
{
    public class UIMainScreenScreenController : AFScreen
    {
        private UIMainScreenScreenComponents view;

        private void Awake()
        {
            base.Awake();
            view = GetComponent<UIMainScreenScreenComponents>();
        }

        private void Start()
        {
            AddUIWalls();
        }

        private void AddUIWalls()
        {
            var UIWalls = Resources.LoadAll<CostumizeWallController>(WallUIPaths.ALL_WALL_PATH);
            foreach(var wall in UIWalls)
            {
                var wallGO = Instantiate(wall, Vector3.zero, Quaternion.identity);
                wallGO.transform.SetParent(view.UICostumizeWallScrollViewViewportContent.transform);
            }
        }
    }
}
