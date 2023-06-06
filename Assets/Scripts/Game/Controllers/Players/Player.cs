using System;
using SpaceInvaders.LiveObjects;
using SpaceInvaders.LiveObjects.LiveComponents.Attacks;
using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.LiveObjects.LiveComponents.Movements;
using SpaceInvaders.UInputs;
using UnityEngine;

namespace SpaceInvaders.Controllers.Players
{
    public class Player : MonoBehaviour
    {
        public event Action OnDestroyed;
        public event Action OnHealthChanged;

        [SerializeField] private LiveObjectData _liveObjectData;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _position;

        public LiveObject LiveObject { get; private set; }

        public void Initialize()
        {
            if (LiveObject != null)
                return;

            LiveObject = _liveObjectData.Create(_parent, _position);

            Subscribe();

            OnHealthChanged?.Invoke();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            UnityInput.OnUpdate += Move;
            UnityInput.OnLmbDown += Attack;

            if (LiveObject.TryGetLiveComponent(out Health health))
            {
                health.OnDestroyed += () => OnDestroyed?.Invoke();
                health.OnChanged += () => OnHealthChanged?.Invoke();
            }
        }

        private void Unsubscribe()
        {
            UnityInput.OnUpdate -= Move;
            UnityInput.OnLmbDown -= Attack;

            if (LiveObject.TryGetLiveComponent(out Health health))
            {
                health.OnDestroyed += () => OnDestroyed?.Invoke();
                health.OnChanged += () => OnHealthChanged?.Invoke();
            }
        }

        private void Attack()
        {
            if (LiveObject.TryGetLiveComponent(out Attack attack))
                attack.Launch();
        }

        private void Move()
        {
            float horizontal = UnityInput.Horizontal;
            float vertical = UnityInput.Vertical;

            if (horizontal == 0 && vertical == 0)
                return;

            if (LiveObject.TryGetLiveComponent(out Movement movement))
                movement.Move(new(horizontal, vertical));
        }
    }
}
