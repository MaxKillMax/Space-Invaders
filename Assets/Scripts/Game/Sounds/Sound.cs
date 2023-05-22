using UnityEngine;
using UnityEngine.Assertions;

namespace SpaceInvaders.Sounds
{
    /// <summary>
    /// Very simple sound manager
    /// </summary>
    public class Sound : MonoBehaviour
    {
        private static Sound Instance;

        [SerializeField] private Transform _parent;
        [SerializeField] private Player _prefab;

        public void Initialize()
        {
            Assert.IsNull(Instance);

            Instance = this;
        }

        public static void Play(Vector3 point, ClipPack pack)
        {
            Player player = Instantiate(Instance._prefab, point, Quaternion.identity, Instance._parent);
            player.Initialize(pack);
        }
    }
}
