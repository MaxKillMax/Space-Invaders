using UnityEngine;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots
{
    public struct LootHandlerConstructData
    {
        public uint TargetTeamIndex;
        public float LifeTime;
        public Vector2 Force;
        public Loot Loot;
    }
}
