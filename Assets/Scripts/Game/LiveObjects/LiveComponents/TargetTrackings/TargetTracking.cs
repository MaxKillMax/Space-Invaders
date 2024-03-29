using System;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.TargetTrackings
{
    /// <summary>
    /// Tracks path to target (point)
    /// </summary>
    public class TargetTracking : LiveComponent
    {
        private Transform _transform;
        private float _errorSize;

        private Vector2 _destination;

        public event Action OnTargetReaching;

        public TargetTracking(TargetTrackingConstructData data)
        {
            _transform = data.Transform;
            _errorSize = data.ErrorSize;
        }

        public override void OnDestroy()
        {
            StopTrack();
        }

        public void Track(Vector2 destination)
        {
            Times.Time.OnUpdate += CheckDistance;

            _destination = destination;
        }

        public void StopTrack()
        {
            Times.Time.OnUpdate -= CheckDistance;
        }

        private void CheckDistance()
        {
            if (_transform == null)
            {
                StopTrack();
                return;
            }

            if (Vector2.Distance(_transform.position, _destination) <= _errorSize)
            {
                StopTrack();
                OnTargetReaching?.Invoke();
            }
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not TargetTracking targetTracking)
                return;

            _transform = targetTracking._transform;
            _errorSize = targetTracking._errorSize;
        }
    }
}
