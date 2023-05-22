using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops
{
    [CreateAssetMenu(fileName = nameof(LootDropData), menuName = PathStart + nameof(LootDropData), order = Order)]
    public class LootDropData : LiveComponentData
    {
        [SerializeField] private LootData _lootData;
        [SerializeField, Range(0, 1)] private float _chance;

        public override LiveComponent Create(LiveObject liveObject)
        {
            return new LootDrop(new()
            {
                Origin = liveObject.transform,
                Health = liveObject.GetLiveComponent<Health>(),
                LootData = _lootData,
                Chance = _chance
            });
        }
    }
}
