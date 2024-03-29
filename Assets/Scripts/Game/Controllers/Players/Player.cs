using System;
using SI.LiveObjects;
using SI.LiveObjects.LiveComponents.Attacks;
using SI.LiveObjects.LiveComponents.Healths;
using SI.LiveObjects.LiveComponents.Movements;
using UnityEngine;

namespace SI.Controllers.Players
{
    public class Player : MonoBehaviour
    {
        public const string POINTS_KEY = "PLAYER_POINTS";

        [SerializeField] private LiveObjectData _liveObjectData;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _position;

        public int Points { get; private set; }
        public LiveObject LiveObject { get; private set; }

        public event Action OnDestroyed;
        public event Action OnHealthChanged;

        private void Awake()
        {
            Points = PlayerPrefs.GetInt(POINTS_KEY);
        }

        public void RecreateView()
        {
            if (LiveObject != null)
            {
                Unsubscribe();
                Destroy(LiveObject.gameObject);
            }

            CreateView();
        }

        public void TryCreateView()
        {
            if (LiveObject != null)
                return;

            CreateView();
        }

        private void CreateView()
        {
            LiveObject = _liveObjectData.Create(_parent, _position);
            Subscribe();

            OnHealthChanged?.Invoke();
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(POINTS_KEY, Points);

            Unsubscribe();
        }

        private void Subscribe()
        {
            Times.Time.OnUpdate += Move;
            Inputs.Input.OnPointerDown += Attack;

            if (LiveObject.TryGetLiveComponent(out Health health))
            {
                health.OnDestroyed += () => OnDestroyed?.Invoke();
                health.OnChanged += () => OnHealthChanged?.Invoke();
            }
        }

        private void Unsubscribe()
        {
            Times.Time.OnUpdate -= Move;
            Inputs.Input.OnPointerDown -= Attack;

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
            float horizontal = Inputs.Input.Horizontal;
            float vertical = Inputs.Input.Vertical;

            if (horizontal == 0 && vertical == 0)
                return;

            if (LiveObject.TryGetLiveComponent(out Movement movement))
                movement.Move(new(horizontal, vertical));
        }
    }
}
