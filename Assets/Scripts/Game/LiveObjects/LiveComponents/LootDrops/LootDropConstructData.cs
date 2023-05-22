using SpaceInvaders.LiveObjects.LiveComponents.Healths;
using SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots;
using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops
{
    public struct LootDropConstructData
    {
        public Transform Origin;
        public LootData LootData;
        public Health Health;
        public float Chance;
    }
}
