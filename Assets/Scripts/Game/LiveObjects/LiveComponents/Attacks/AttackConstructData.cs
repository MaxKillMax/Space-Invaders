using SI.Projectiles;
using SI.Sounds;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Attacks
{
    public struct AttackConstructData
    {
        public Transform OriginTransform;
        public ProjectileData ProjectileData;
        public ClipPack ShootClipPack;
    }
}
