using System.Collections.Generic;
using SI.Projectiles;
using SI.Sounds;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Attacks
{
    /// <summary>
    /// RangeAttack (shoot projectiles forward)
    /// </summary>
    public class Attack : LiveComponent
    {
        private Transform _originTransform;
        private ProjectileData _projectileData;
        private ClipPack _shootClipPack;

        private readonly List<Projectile> _projectiles = new();

        public Attack(AttackConstructData data)
        {
            _originTransform = data.OriginTransform;
            _projectileData = data.ProjectileData;
            _shootClipPack = data.ShootClipPack;
        }

        public void Launch()
        {
            Sound.PlayOnPoint(_originTransform.position, _shootClipPack);

            Projectile projectile = _projectileData.Create(_originTransform.parent, _originTransform.position);
            projectile.Direction = _originTransform.up;
            projectile.SetState(true);
            projectile.OnEnded += () => DestroyProjectile(projectile);
            _projectiles.Add(projectile);
        }

        private void DestroyProjectile(Projectile projectile)
        {
            _projectiles.Remove(projectile);
            Object.Destroy(projectile.gameObject);
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not Attack attack)
                return;

            _originTransform = attack._originTransform;
            _projectileData = attack._projectileData;
            _shootClipPack = attack._shootClipPack;
        }
    }
}
