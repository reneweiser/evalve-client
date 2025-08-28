using UnityEngine;

namespace Evalve.Client
{
    public static class SceneObjectExtensions
    {
        public static Vector3 ToVector3(this Vector source)
        {
            return new Vector3(source.X, source.Y, source.Z);
        }

        public static Vector ToVector(this Vector3 source)
        {
            return new Vector
            {
                X = source.x,
                Y = source.y,
                Z = source.z
            };
        }
    }
}