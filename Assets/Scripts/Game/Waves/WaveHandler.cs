using System;
using UnityEngine;

namespace SpaceInvaders.Waves
{
    public class WaveHandler : MonoBehaviour
    {
        public event Action OnWaveIndexChanged;
        public event Action OnPathEndReached;
        public event Action OnEnemyDestroyed;
        public event Action OnDestroyed;

        [SerializeField] private Transform _parent;
        [SerializeField] private WaveData[] _waveDatas;

        private Wave Wave { get; set; }
        public int WaveIndex { get; private set; } = -1;

        public void LaunchNewWave()
        {
            TryDestroyWave();

            WaveIndex++;
            int index = WaveIndex % _waveDatas.Length;
            Wave = _waveDatas[index].Create(_parent);

            Wave.OnDestroyed += OnWaveDestroy;
            Wave.OnEnemyDestroyed += () => OnEnemyDestroyed?.Invoke();
            Wave.OnPathEndReached += () => OnPathEndReached?.Invoke();

            OnWaveIndexChanged?.Invoke();
        }

        public void ResetIndex() => WaveIndex = -1;

        private void OnWaveDestroy() => OnDestroyed?.Invoke();

        private void OnDestroy() => TryDestroyWave();

        private void TryDestroyWave()
        {
            if (Wave == null)
                return;

            Wave.OnDestroyed -= OnWaveDestroy;
            Wave.Destroy();
            Wave = null;
        }
    }
}
