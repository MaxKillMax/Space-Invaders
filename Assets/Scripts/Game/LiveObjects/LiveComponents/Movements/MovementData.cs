using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Movements
{
    [CreateAssetMenu(fileName = nameof(MovementData), menuName = PathStart + nameof(MovementData), order = Order)]
    public class MovementData : LiveComponentData
    {
        [SerializeField] private Vector2 _speed = Vector2.one;
        [SerializeField] private float _bothDirectionsDivider = 1;

        public Vector2 Speed { get => _speed; set => _speed = value; }

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new Movement(new()
            {
                Rigidbody = liveObject.Rigidbody,
                Speed = _speed,
                BothDirectionsDivider = _bothDirectionsDivider
            });
        }
    }
}
