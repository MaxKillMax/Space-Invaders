using SI.LiveObjects.LiveComponents.Healths;
using SI.LiveObjects.LiveComponents.LootDrops.Loots;
using UnityEngine;

namespace SI.LiveObjects.LiveComponents.LootDrops
{
    public struct LootDropConstructData
    {
        public Transform Origin;
        public LootData LootData;
        public Health Health;
        public float Chance;
    }
}
