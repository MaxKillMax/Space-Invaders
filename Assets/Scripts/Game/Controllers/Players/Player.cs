using System;
using SI.LiveObjects;
using SI.LiveObjects.LiveComponents.Attacks;
using SI.LiveObjects.LiveComponents.Healths;
using SI.LiveObjects.LiveComponents.Movements;
using UnityEngine;
using UnityEngine.Assertions;

namespace SI.Controllers.Players
{

    public class Player : MonoBehaviour
    {
        public const string POINTS_KEY = "PLAYER_POINTS";

        private static Player Instance;

        [SerializeField] private LiveObjectData _liveObjectData;
        [SerializeField] private Transform _parent;
        [SerializeField] private Vector3 _position;

        private static int PPoints;
        public static int Points
        {
            get => PPoints; set
            {
                if (PPoints == value)
                    return;

                PPoints = value;
                Saving.Save(POINTS_KEY, Points);
            }
        }
        public LiveObject LiveObject { get; private set; }

        public event Action OnDestroyed;
        public event Action OnHealthChanged;

        public void Initialize()
        {
            Assert.IsNull(Instance);

            Instance = this;

            Points = Saving.Load(POINTS_KEY, out int points) ? points : 0;
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
