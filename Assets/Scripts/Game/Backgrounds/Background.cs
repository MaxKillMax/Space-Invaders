using UnityEngine;

namespace SI.Backgrounds
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private float _degreesPerSecond = -1;
        private Transform _transform;

        public float DegreesPerSecond => _degreesPerSecond;

        public void Initialize()
        {
            _transform = transform;

            Times.Time.OnUpdate += MoveSprite;
        }

        private void OnDestroy()
        {
            Times.Time.OnUpdate -= MoveSprite;
        }

        private void MoveSprite()
        {
            _transform.Rotate(Vector3.forward, _degreesPerSecond * Time.deltaTime);
        }
    }
}
