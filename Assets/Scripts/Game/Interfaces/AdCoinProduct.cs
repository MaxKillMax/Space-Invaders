using CrazyGames;
using SI.Controllers.Players;
using UnityEngine;

namespace SI.Purchasings
{
    [CreateAssetMenu(fileName = nameof(AdCoinProduct), menuName = PATH_START + nameof(AdCoinProduct), order = ORDER)]
    public class AdCoinProduct : Product
    {
        [SerializeField] private int _reward;

        public override void Execute() => Ad.Show(CrazyAdType.rewarded, () => Player.Points += _reward);
    }
}