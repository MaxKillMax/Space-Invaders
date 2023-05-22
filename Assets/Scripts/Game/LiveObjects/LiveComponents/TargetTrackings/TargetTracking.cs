using System;
using SpaceInvaders.UInputs;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.TargetTrackings
{
    /// <summary>
    /// Tracks path to target (point)
    /// </summary>
    public class TargetTracking : LiveComponent
    {
        public event Action OnTargetReaching;

        private Transform _transform;
        private float _errorSize;

        private Vector2 _destination;

        public TargetTracking(TargetTrackingConstructData data)
        {
            _transform = data.Transform;
            _errorSize = data.ErrorSize;
        }

        public void Track(Vector2 destination)
        {
            UInput.OnUpdate += CheckDistance;

            _destination = destination;
        }

        public void StopTrack()
        {
            UInput.OnUpdate -= CheckDistance;
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
