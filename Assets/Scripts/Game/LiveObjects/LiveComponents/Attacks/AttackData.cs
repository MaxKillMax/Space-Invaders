using SpaceInvaders.Projectiles;
using SpaceInvaders.Sounds;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Attacks
{
    [CreateAssetMenu(fileName = nameof(AttackData), menuName = PathStart + nameof(AttackData), order = Order)]
    public class AttackData : LiveComponentData
    {
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField] private ClipPack _shootClipPack;

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new Attack(new()
            {
                OriginTransform = liveObject.transform,
                ProjectileData = _projectileData,
                ShootClipPack = _shootClipPack
            });
        }
    }
}
