using System;
using UnityEngine;

namespace SpaceInvaders.Waves
{
    /// <summary>
    /// Keeps track of the current wave and tracks its events
    /// </summary>
    public class WaveHandler : MonoBehaviour
    {
        public event Action OnWaveCountChanged;
        public event Action OnWavePathEndReached;
        public event Action OnWaveEnemyDestroyed;
        public event Action OnWaveDestroyed;

        [SerializeField] private Transform _parent;
        [SerializeField] private WaveData[] _waveDatas;

        private Wave Wave { get; set; }
        public int CountOfWaves { get; private set; } = -1;

        public void LaunchNewWave()
        {
            TryDestroyWave();

            CountOfWaves++;
            int index = CountOfWaves % _waveDatas.Length;
            Wave = _waveDatas[index].Create(_parent);

            Subscribe();

            OnWaveCountChanged?.Invoke();
        }

        private void Subscribe()
        {
            Wave.OnDestroyed += () => OnWaveDestroyed?.Invoke();
            Wave.OnEnemyDestroyed += () => OnWaveEnemyDestroyed?.Invoke();
            Wave.OnPathEndReached += () => OnWavePathEndReached?.Invoke();
        }

        public void ResetCounter() => CountOfWaves = -1;

        private void OnDestroy() => TryDestroyWave();

        private void TryDestroyWave()
        {
            if (Wave == null)
                return;

            Unsubscrie();

            Wave.Destroy();
            Wave = null;
        }

        private void Unsubscrie()
        {
            Wave.OnDestroyed -= () => OnWaveDestroyed?.Invoke();
            Wave.OnEnemyDestroyed -= () => OnWaveEnemyDestroyed?.Invoke();
            Wave.OnPathEndReached -= () => OnWavePathEndReached?.Invoke();
        }
    }
}
