using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF
{
    public static class WallNames
    {
        public const string SIMPLE_WALL = "SimpleWall";
        public const string START_WALL = "StartWall";
        public const string END_WALL = "EndWall";
        public const string PREVIEW_WALL = "PreviewWall";
    }

    public static class WallPaths
    {
        public const string SIMPLE_WALL_PATH = "Prefabes/" + WallNames.SIMPLE_WALL;
        public const string START_WALL_PATH = "Prefabes/" + WallNames.START_WALL;
        public const string END_WALL_PATH = "Prefabes/" + WallNames.END_WALL;
        public const string PREVIEW_WALL_PATH = "Prefabes/" + WallNames.PREVIEW_WALL;
        public const string BEST_MARGIN_SPHERE = "Prefabes/BestMarginSphere";
    }
}
