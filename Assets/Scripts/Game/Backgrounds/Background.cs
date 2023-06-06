using SpaceInvaders.UInputs;
using UnityEngine;

namespace SpaceInvaders.Backgrounds
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private float _degreesPerSecond = -1;
        private Transform _transform;

        public float DegreesPerSecond => _degreesPerSecond;

        public void Initialize()
        {
            _transform = transform;

            UInput.OnUpdate += MoveSprite;
        }

        private void OnDestroy()
        {
            UInput.OnUpdate -= MoveSprite;
        }

        private void MoveSprite()
        {
            _transform.Rotate(Vector3.forward, _degreesPerSecond * Time.deltaTime);
        }
    }
}
