using SI.Controllers.Players;
using SI.Projectiles;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 3 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SniperProduct), menuName = PATH_START + nameof(SniperProduct), order = ORDER)]
    public class SniperProduct : Product
    {
        [SerializeField] private float _damageAddition;

        public override void Execute() => PlayerComponentHandler.ModifyComponent<ProjectileData>((p) => p.HealthAction = new() { TargetTeamIndexs = p.HealthAction.TargetTeamIndexs, Amount = p.HealthAction.Amount - _damageAddition });
    }
}