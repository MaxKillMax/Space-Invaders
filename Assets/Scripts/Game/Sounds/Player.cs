using System.Threading.Tasks;
using UnityEngine;

namespace SpaceInvaders.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        private AudioSource _source;

        public void Initialize(ClipPack pack)
        {
            _source = GetComponent<AudioSource>();
            _source.volume = pack.Volume;
            _source.PlayOneShot(pack.Clip);
            WaitForDestroyAsync();
        }

        private async void WaitForDestroyAsync()
        {
            while (_source.isPlaying)
            {
                await Task.Yield();

                if (_source == null || gameObject == null)
                    return;
            }

            Destroy(gameObject);
        }
    }
}
