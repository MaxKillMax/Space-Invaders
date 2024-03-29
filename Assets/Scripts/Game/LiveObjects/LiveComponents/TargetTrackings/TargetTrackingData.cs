using UnityEngine;

namespace SI.LiveObjects.LiveComponents.TargetTrackings
{
    [CreateAssetMenu(fileName = nameof(TargetTrackingData), menuName = PathStart + nameof(TargetTrackingData), order = Order)]
    public class TargetTrackingData : LiveComponentData
    {
        [SerializeField] private float _errorSize;

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new TargetTracking(new()
            {
                Transform = liveObject.transform,
                ErrorSize = _errorSize
            });
        }
    }
}
