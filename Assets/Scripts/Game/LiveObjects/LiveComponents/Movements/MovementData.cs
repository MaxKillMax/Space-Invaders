using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Movements
{
    [CreateAssetMenu(fileName = nameof(MovementData), menuName = PathStart + nameof(MovementData), order = Order)]
    public class MovementData : LiveComponentData
    {
        [SerializeField] private Vector2 _speed = Vector2.one;
        [SerializeField] private float _bothDirectionsDivider = 1;

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
