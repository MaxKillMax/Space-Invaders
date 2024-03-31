using SI.Controllers.Players;
using SI.Projectiles;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 3 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(MinerProduct), menuName = PATH_START + nameof(MinerProduct), order = ORDER)]
    public class MinerProduct : Product
    {
        [SerializeField] private Vector2 _speedRemoving;
        [SerializeField] private float _lifetimeAddition;

        public override void Execute() => PlayerComponentHandler.ModifyComponent<ProjectileData>((p) =>
        {
            p.SpeedMultiply = new Vector2(p.SpeedMultiply.x - _speedRemoving.x, p.SpeedMultiply.y - _speedRemoving.y);
            p.LifeTime += _lifetimeAddition;
        });
    }
}