using System.Collections.Generic;
using System.Linq;
using SI.LiveObjects.LiveComponents;
using UnityEngine;

namespace SI.LiveObjects
{
    /// <summary>
    /// LiveObject is a container of LiveComponents
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Collider2D))]
    public class LiveObject : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Collider2D Collider { get; private set; }

        private readonly List<LiveComponent> _liveComponents = new();

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Collider = GetComponent<Collider2D>();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _liveComponents.Count; i++)
                _liveComponents[i].OnDestroy();
        }

        public void TryAddLiveComponent(LiveComponent liveComponent)
        {
            if (!_liveComponents.Contains(liveComponent))
                _liveComponents.Add(liveComponent);
        }

        public void TryRemoveLiveComponent(LiveComponent liveComponent)
        {
            if (_liveComponents.Contains(liveComponent))
                _liveComponents.Remove(liveComponent);
        }

        public void TryRemoveLiveComponent<T>()
        {
            LiveComponent liveComponent = _liveComponents.OfType<T>().FirstOrDefault() as LiveComponent;

            if (liveComponent != default)
                _liveComponents.Remove(liveComponent);
        }

        public bool TryGetLiveComponent<T>(out T liveComponent) => ((liveComponent = GetLiveComponent<T>()) as object) != default;

        public T GetLiveComponent<T>() => _liveComponents.OfType<T>().FirstOrDefault();
    }
}
