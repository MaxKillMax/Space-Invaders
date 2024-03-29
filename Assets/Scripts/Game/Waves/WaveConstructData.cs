using SI.LiveObjects;
using UnityEngine;

namespace SI.Waves
{
    /// <summary>
    /// Data for wave initialization
    /// </summary>
    public struct WaveConstructData
    {
        public Vector2[] Path;
        public LiveObject[] LiveObjects;
        public float ShootDelay;
    }
}
