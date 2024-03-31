using SI.Projectiles;
using SI.Sounds;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Attacks
{
    [CreateAssetMenu(fileName = nameof(AttackData), menuName = PathStart + nameof(AttackData), order = Order)]
    public class AttackData : LiveComponentData
    {
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField] private ClipPack _shootClipPack;

        public ProjectileData ProjectileData { get => _projectileData; set => _projectileData = value; }
        public ClipPack ClipPack { get => _shootClipPack; set => _shootClipPack = value; }

        public override LiveComponent Create(LiveObject liveObject) => new Attack(new()
        {
            OriginTransform = liveObject.transform,
            ProjectileData = _projectileData,
            ShootClipPack = _shootClipPack
        });
    }
}
