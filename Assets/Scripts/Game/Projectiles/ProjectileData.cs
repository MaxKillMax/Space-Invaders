using SI.LiveObjects.LiveComponents.Healths;
using UnityEngine;

namespace SI.Projectiles
{
    [CreateAssetMenu(fileName = nameof(ProjectileData), menuName = nameof(ProjectileData), order = 51)]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private HealthAction _healthAction = new(-10, 0);
        [SerializeField] private Vector2 _speedMultiply = Vector2.one;
        [SerializeField] private float _lifeTime = 10;

        public Projectile Create(Transform parent, Vector3 position)
        {
            Projectile projectile = Instantiate(_prefab, position, Quaternion.identity, parent);
            projectile.Initialize(_sprite, _healthAction, _speedMultiply, _lifeTime);
            projectile.SetState(false);
            return projectile;
        }
    }
}
