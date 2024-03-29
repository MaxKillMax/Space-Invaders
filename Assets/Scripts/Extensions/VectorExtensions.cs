
using UnityEngine;

namespace SI
{
    public static class VectorExtensions
    {
        public static Vector2 DirectionTo(this Vector2 selfVector, Vector2 vector) => DirectionTo(selfVector, vector, out _);

        public static Vector2 DirectionTo(this Vector2 selfVector, Vector2 vector, out float distance)
        {
            Vector2 heading = vector - selfVector;
            distance = heading.magnitude;
            return heading / distance;
        }

        public static Vector3 DirectionTo(this Vector3 selfVector, Vector3 vector) => DirectionTo(selfVector, vector, out _);

        public static Vector3 DirectionTo(this Vector3 selfVector, Vector3 vector, out float distance)
        {
            Vector3 heading = vector - selfVector;
            distance = heading.magnitude;
            return heading / distance;
        }
    }
}
