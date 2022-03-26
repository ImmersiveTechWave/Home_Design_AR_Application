using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF
{
    public class MathUtils
    {

        public static float RoundNumber(float number, float intervalDifference)
        {
            var half = intervalDifference / 2;
            int quotient = (int)(number / intervalDifference);
            if (quotient > half)
            {
                return (quotient + 1) * intervalDifference;
            }
            else
            {
                return quotient * intervalDifference;
            }
        }

        public static float GetAngle(Vector3 start, Vector3 end)
        {
            var distance = (start - end).magnitude;
            var differnce = end.x - start.x;
            var oppositeS = Mathf.Sign(differnce) * differnce;
            var sinAngle = oppositeS / distance;
            var angle = Mathf.Asin(sinAngle);
            return angle * Mathf.Rad2Deg;
        }
    }
}