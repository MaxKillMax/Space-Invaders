using UnityEngine;

namespace SI.LiveObjects.LiveComponents.LootDrops.Loots
{
    public abstract class LootData : ScriptableObject
    {
        protected const int Order = 52;
        protected const string PathStart = "Loots/";

        [SerializeField] protected LootHandler HandlerPrefab;
        [SerializeField] protected uint TargetTeamIndex;
        [SerializeField] protected float LifeTime;
        [SerializeField] protected Vector2 Force;

        public abstract LootHandler Create(Transform parent, Vector3 position);
    }
}
