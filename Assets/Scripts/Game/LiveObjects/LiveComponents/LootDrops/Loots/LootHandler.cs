using SpaceInvaders.LiveObjects.LiveComponents.Targets;
using SpaceInvaders.UInputs;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots
{
    /// <summary>
    /// Loot falling down and if interact with target, target take loot
    /// </summary>
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class LootHandler : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private uint _targetTeamIndex;
        private float _lifeTime;
        private Vector2 _force;
        private Loot _loot;

        public virtual void Initialize(LootHandlerConstructData data)
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;

            _rigidbody = GetComponent<Rigidbody2D>();
            _targetTeamIndex = data.TargetTeamIndex;
            _lifeTime = data.LifeTime;
            _force = data.Force;
            _loot = data.Loot;

            UnityInput.OnUpdate += Move;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out LiveObject liveObject) && liveObject.TryGetLiveComponent(out Target target) && target.TeamIndex == _targetTeamIndex)
            {
                _loot.Give(liveObject);
                Destroy(gameObject);
            }
        }

        private void Move()
        {
            _rigidbody.velocity = _force;

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            UnityInput.OnUpdate -= Move;
        }
    }
}
