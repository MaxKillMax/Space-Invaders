using SI.Controllers.Players;
using SI.LiveObjects.LiveComponents.Attacks;
using SI.Projectiles;
using SI.Sounds;
using UnityEngine;

namespace SI.Purchasings
{
    /// <summary>
    /// 2 tier
    /// </summary>
    [CreateAssetMenu(fileName = nameof(BallProduct), menuName = PATH_START + nameof(BallProduct), order = ORDER)]
    public class BallProduct : Product
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector2 _scale;
        [SerializeField] private ClipPack _clipPack;
        [SerializeField] private float _lifeTimeReducing;

        public override void Execute()
        {
            PlayerComponentHandler.ModifyComponent<ProjectileData>((p) =>
            {
                p.Sprite = _sprite;
                p.Scale = _scale;
                p.LifeTime -= _lifeTimeReducing;
            });

            PlayerComponentHandler.ModifyComponent<AttackData>((a) => a.ClipPack = _clipPack);
        }
    }
}