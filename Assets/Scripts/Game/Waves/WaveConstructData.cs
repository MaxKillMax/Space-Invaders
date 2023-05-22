using SpaceInvaders.LiveObjects;
using UnityEngine;

namespace SpaceInvaders.Waves
{
    public struct WaveConstructData
    {
        public Vector2[] Path;
        public LiveObject[] LiveObjects;
        public float ShootDelay;
    }
}
