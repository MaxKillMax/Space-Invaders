using UnityEngine;

namespace SI.Sounds
{
    /// <summary>
    /// Pack for Sound, which can contain different settings (mixer group, volume, loop, delay..)
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ClipPack), menuName = nameof(ClipPack), order = 51)]
    public class ClipPack : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField, Range(0, 1)] private float _volume = 1;

        public AudioClip Clip => _clip;
        public float Volume => _volume;
    }
}
