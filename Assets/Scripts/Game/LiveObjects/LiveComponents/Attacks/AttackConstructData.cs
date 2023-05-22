using SpaceInvaders.Projectiles;
using SpaceInvaders.Sounds;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Attacks
{
    public struct AttackConstructData
    {
        public Transform OriginTransform;
        public ProjectileData ProjectileData;
        public ClipPack ShootClipPack;
    }
}
