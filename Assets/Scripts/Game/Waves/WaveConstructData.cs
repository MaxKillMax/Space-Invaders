using SpaceInvaders.LiveObjects;
using UnityEngine;

namespace SpaceInvaders.Waves
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
