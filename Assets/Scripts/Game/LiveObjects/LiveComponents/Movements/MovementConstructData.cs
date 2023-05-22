using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.Movements
{
    public struct MovementConstructData
    {
        public Rigidbody2D Rigidbody;
        public Vector2 Speed;
        public float BothDirectionsDivider;
    }
}
