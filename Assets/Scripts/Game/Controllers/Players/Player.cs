using System;
using SpaceInvaders.LiveObjects;
using SpaceInvaders.LiveObjects.LiveComponents.Attacks;
using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.LiveObjects.LiveComponents.Movements;
using SpaceInvaders.UInputs;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpaceInvaders.Controllers.Players
{
    public class Player : MonoBehaviour
    {
        public event Action OnDestroyed;
        public event Action OnHealthChanged;

        [SerializeField] private LiveObjectData _liveObjectData;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _position;

        private LiveObject _liveObject;

        private Attack _attack;
        private Movement _movement;
        private Health _health;

        public float Health => _health.Amount;
        public float MaxHealth => _health.MaxAmount;

        private void Awake()
        {
            UInput.OnUpdate += Move;
            UInput.OnLmbDown += () => _attack?.Launch();
        }

        public void Initialize()
        {
            if (_liveObject != null)
                Destroy(_liveObject.gameObject);

            _liveObject = _liveObjectData.Create(_parent, _position);

            _attack = _liveObject.GetLiveComponent<Attack>();
            _movement = _liveObject.GetLiveComponent<Movement>();
            _health = _liveObject.GetLiveComponent<Health>();

            _health.OnDestroyed += () => OnDestroyed?.Invoke();
            _health.OnChanged += () => OnHealthChanged?.Invoke();

            Assert.IsTrue(_attack != default && _movement != default && _health != default);

            OnHealthChanged?.Invoke();
        }

        private void OnDestroy()
        {
            UInput.OnUpdate -= Move;
            UInput.OnLmbDown -= () => _attack?.Launch();

            if (_health != null)
            {
                _health.OnDestroyed -= () => OnDestroyed?.Invoke();
                _health.OnChanged -= () => OnHealthChanged?.Invoke();
            }
        }

        private void Move()
        {
            float horizontal = UInput.Horizontal;
            float vertical = UInput.Vertical;

            if (horizontal != 0 || vertical != 0)
                _movement?.Move(new(horizontal, vertical));
        }
    }
}
