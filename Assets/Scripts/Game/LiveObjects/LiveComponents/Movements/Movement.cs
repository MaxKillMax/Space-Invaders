using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Movements
{
    /// <summary>
    /// Moves the LiveObject in the direction of
    /// </summary>
    public class Movement : LiveComponent
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _speed;
        private float _bothDirectionsDivider;

        public Movement(MovementConstructData data)
        {
            _rigidbody = data.Rigidbody;
            _speed = data.Speed;
            _bothDirectionsDivider = data.BothDirectionsDivider;
        }

        public void Move(Vector2 direction)
        {
            if (direction.x != 0 && direction.y != 0)
                direction /= _bothDirectionsDivider;

            _rigidbody.velocity = _speed * direction;
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not Movement movement)
                return;

            _rigidbody = movement._rigidbody;
            _speed = movement._speed;
            _bothDirectionsDivider = movement._bothDirectionsDivider;
        }
    }
}