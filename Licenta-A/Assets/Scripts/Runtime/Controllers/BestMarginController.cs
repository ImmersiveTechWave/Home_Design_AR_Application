using System.Collections.Generic;
using UnityEngine;

namespace AF
{
    public class BestMarginController : MonoBehaviour
    {
        private List<WallMarginController> posiblePoints = new List<WallMarginController>();

        /// <summary>
        /// Find the best point from where to start the construction of the wall.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public Vector3 GetBestPoint(Vector3 startPosition)
        {
            var minDistance = float.MaxValue;
            var bestPoint = Vector3.zero;

            foreach (var point in posiblePoints)
            {
                var distance = (startPosition - point.transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestPoint = point.transform.position;
                }
            }

            return bestPoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            var wallMarginController = other.gameObject.GetComponent<WallMarginController>();
            if (wallMarginController != null)
            {
                posiblePoints.Add(wallMarginController);
            }
        }
    }
}
