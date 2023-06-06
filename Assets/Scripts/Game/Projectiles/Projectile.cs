using System;
using SpaceInvaders.LiveObjects;
using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.UInputs;
using UnityEngine;

namespace SpaceInvaders.Projectiles
{
    /// <summary>
    /// A projectile flying in the direction and reacting to enemy LiveObjects
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        public event Action OnEnded;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private HealthAction _healthAction;
        private Vector2 _speedMultiply;
        private float _maxLifeTime;
        private float _lifeTime = 0;

        public Vector2 Direction { get; set; } = Vector2.zero;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            _collider.isTrigger = true;
        }

        private void Start()
        {
            UInput.OnUpdate += Move;
        }

        private void OnDestroy()
        {
            UInput.OnUpdate -= Move;
        }

        public void Initialize(Sprite sprite, HealthAction healthAction, Vector2 speedMultiply, float lifeTime)
        {
            _spriteRenderer.sprite = sprite;
            _healthAction = healthAction;
            _speedMultiply = speedMultiply;
            _maxLifeTime = lifeTime;
        }

        public void SetState(bool state)
        {
            enabled = state;
            _spriteRenderer.enabled = state;
            _collider.enabled = state;
            _rigidbody.bodyType = state ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;

            if (state)
                _lifeTime = _maxLifeTime;
        }

        private void Move()
        {
            _rigidbody.velocity = _speedMultiply * Direction;

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
                End();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject))
                TryAttack(liveObject);
        }

        private void TryAttack(LiveObject liveObject)
        {
            if (liveObject.TryGetLiveComponent(out Health health) && health.TryAct(_healthAction))
                End();
        }

        private void End()
        {
            SetState(false);
            // Пула пока нет
            Destroy(gameObject);
            OnEnded?.Invoke();
        }
    }
}
