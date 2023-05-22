using SpaceInvaders.LiveObjects.LiveComponents.Attacks;

namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots.AttackLoots
{
    /// <summary>
    /// Replaces old attack data on new
    /// </summary>
    public class AttackLoot : Loot
    {
        private AttackData _attackData;

        public AttackLoot(AttackLootConstructData data)
        {
            _attackData = data.AttackData;
        }

        public override void Give(LiveObject liveObject)
        {
            if (liveObject.TryGetLiveComponent(out Attack attack))
                attack.TryReplace(_attackData.Create(liveObject));
        }
    }
}
