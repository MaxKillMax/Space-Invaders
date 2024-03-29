using UnityEngine;

namespace SI.LiveObjects.LiveComponents.Targets
{
    [CreateAssetMenu(fileName = nameof(TargetData), menuName = PathStart + nameof(TargetData), order = Order)]
    public class TargetData : LiveComponentData
    {
        [SerializeField] private uint _teamIndex;

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new Target(new()
            {
                TeamIndex = _teamIndex
            });
        }
    }
}
