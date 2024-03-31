using SI.LiveObjects.LiveComponents.Healths;
using UnityEngine;

namespace SI.Projectiles
{
    [CreateAssetMenu(fileName = nameof(ProjectileData), menuName = nameof(ProjectileData), order = 51)]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Vector3 _scale = Vector3.one;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private HealthAction _healthAction = new(-10, 0);
        [SerializeField] private Vector2 _speedMultiply = Vector2.one;
        [SerializeField] private float _lifeTime = 10;

        public Vector3 Scale { get => _scale; set => _scale = value; }
        public Sprite Sprite { get => _sprite; set => _sprite = value; }
        public HealthAction HealthAction { get => _healthAction; set => _healthAction = value; }
        public Vector2 SpeedMultiply { get => _speedMultiply; set => _speedMultiply = value; }
        public float LifeTime { get => _lifeTime; set => _lifeTime = value; }

        public Projectile Create(Transform parent, Vector3 position)
        {
            Projectile projectile = Instantiate(_prefab, position, Quaternion.identity, parent);
            projectile.transform.localScale = _scale;
            projectile.Initialize(_sprite, _healthAction, _speedMultiply, _lifeTime);
            projectile.SetState(false);
            return projectile;
        }
    }
}
