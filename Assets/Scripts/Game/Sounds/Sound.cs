using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace SI.Sounds
{
    /// <summary>
    /// Very simple sound manager
    /// </summary>
    public class Sound : MonoBehaviour
    {
        private static Sound Instance;

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Transform _parent;
        [SerializeField] private Player _prefab;

        public void Initialize()
        {
            Assert.IsNull(Instance);

            Instance = this;
        }

        public static void SetGroupVolume(string key, float volume)
        {
            Instance._mixer.SetFloat(key, volume);
        }

        public static void PlayOnPoint(Vector3 point, ClipPack pack)
        {
            Player player = Instantiate(Instance._prefab, point, Quaternion.identity, Instance._parent);
            player.Initialize(pack);
        }

        // PlayGlobal

        // PlayOnTransform
    }
}
