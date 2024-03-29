using SI.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Healths
{
    [CreateAssetMenu(fileName = nameof(HealthData), menuName = PathStart + nameof(HealthData), order = Order)]
    public class HealthData : LiveComponentData
    {
        [SerializeField] private float _maxHealth = 50;

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new Health(new()
            {
                GameObject = liveObject.gameObject,
                Target = liveObject.GetLiveComponent<Target>(),
                Amount = _maxHealth
            });
        }
    }
}
