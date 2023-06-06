namespace SpaceInvaders.LiveObjects.LiveComponents.LootDrops.Loots
{
    /// <summary>
    /// Object dropping out after LiveObject death with LootDrop component
    /// </summary>
    public abstract class Loot
    {
        public abstract void Give(LiveObject liveObject);
    }
}
