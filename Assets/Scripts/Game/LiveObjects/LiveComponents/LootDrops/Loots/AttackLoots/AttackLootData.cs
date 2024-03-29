using SI.LiveObjects.LiveComponents.Attacks;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.LootDrops.Loots.AttackLoots
{
    [CreateAssetMenu(fileName = nameof(AttackLootData), menuName = PathStart + nameof(AttackLootData), order = Order)]
    public class AttackLootData : LootData
    {
        [SerializeField] private AttackData _attackData;

        public override LootHandler Create(Transform parent, Vector3 position)
        {
            LootHandler lootHandler = Instantiate(HandlerPrefab, position, Quaternion.identity, parent);

            AttackLoot attackLoot = new(new()
            {
                AttackData = _attackData
            });

            lootHandler.Initialize(new()
            {
                Force = Force,
                LifeTime = LifeTime,
                TargetTeamIndex = TargetTeamIndex,
                Loot = attackLoot
            });

            return lootHandler;
        }
    }
}
