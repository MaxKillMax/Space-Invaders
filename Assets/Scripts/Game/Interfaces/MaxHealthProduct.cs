using SI.Controllers.Players;
using SI.LiveObjects.LiveComponents.Healths;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 1 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(MaxHealthProduct), menuName = PATH_START + nameof(MaxHealthProduct), order = ORDER)]
    public class MaxHealthProduct : Product
    {
        [SerializeField] private float _healthAddition;

        public override void Execute() => PlayerComponentHandler.ModifyComponent<HealthData>((h) => h.MaxHealth += _healthAddition);
    }
}