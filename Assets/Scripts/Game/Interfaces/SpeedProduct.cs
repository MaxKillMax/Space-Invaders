using SI.Controllers.Players;
using SI.LiveObjects.LiveComponents.Movements;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 2 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SpeedProduct), menuName = PATH_START + nameof(SpeedProduct), order = ORDER)]
    public class SpeedProduct : Product
    {
        [SerializeField] private Vector2 _speedAddition;

        public override void Execute() => PlayerComponentHandler.ModifyComponent<MovementData>((p) => p.Speed = new Vector2(p.Speed.x + _speedAddition.x, p.Speed.y + _speedAddition.y));
    }
}