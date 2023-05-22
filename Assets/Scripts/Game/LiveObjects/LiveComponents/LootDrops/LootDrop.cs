using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops
{
    public class LootDrop : LiveComponent
    {
        private Transform _origin;
        private LootData _lootData;
        private float _chance;
        private Health _health;

        public LootDrop(LootDropConstructData data)
        {
            _lootData = data.LootData;
            _chance = data.Chance;
            _origin = data.Origin;
            _health = data.Health;

            _health.OnDestroyed += TrySpawnLoot;
        }

        private void TrySpawnLoot()
        {
            _health.OnDestroyed -= TrySpawnLoot;

            if (Random.Range(0, 1f) <= _chance)
                _lootData.Create(_origin.parent, _origin.position);
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not LootDrop lootDrop)
                return;

            _origin = lootDrop._origin;
            _lootData = lootDrop._lootData;
            _chance = lootDrop._chance;
            _health = lootDrop._health;
        }
    }
}
