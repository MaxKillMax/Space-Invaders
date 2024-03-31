using SI.Controllers.Players;
using SI.Projectiles;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 1 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ProjectileSpeedProduct), menuName = PATH_START + nameof(ProjectileSpeedProduct), order = ORDER)]
    public class ProjectileSpeedProduct : Product
    {
        [SerializeField] private Vector2 _speedAddition;

        public override void Execute() => PlayerComponentHandler.ModifyComponent<ProjectileData>((p) => p.SpeedMultiply = new Vector2(p.SpeedMultiply.x + _speedAddition.x, p.SpeedMultiply.y + _speedAddition.y));
    }
}