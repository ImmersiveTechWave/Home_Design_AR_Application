using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF
{
    public class App : MonoBehaviour
    {
        public static Camera ActiveCamera { get; set; }
        public static SimpleWallController SelectedWall;

        private void Start()
        {
            ActiveCamera = Camera.main;
        }
    }
}
