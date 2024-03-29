using System;
using UnityEngine;

namespace SI.Waves
{
    /// <summary>
    /// Keeps track of the current wave and tracks its events
    /// </summary>
    public class WaveHandler : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private WaveData[] _waveDatas;

        private Wave _wave;
        public int CountOfWaves { get; private set; } = -1;
        public DateTime StartTime { get; private set; }
        public float TotalTime => (float)(DateTime.Now - StartTime).TotalSeconds;

        public event Action OnWaveCountChanged;
        public event Action OnPathEndReached;
        public event Action OnEnemyDestroyed;
        public event Action OnWaveDestroyed;

        public void RestartWave()
        {
            CountOfWaves--;
            StartNewWave();
        }

        public void StartNewWave()
        {
            TryDestroyWave();

            StartTime = DateTime.Now;

            CountOfWaves++;
            int index = CountOfWaves % _waveDatas.Length;
            _wave = _waveDatas[index].Create(_parent);

            Subscribe();

            OnWaveCountChanged?.Invoke();
        }

        private void OnDestroy() => TryDestroyWave();

        private void TryDestroyWave()
        {
            if (_wave == null)
                return;

            Unsubscribe();

            _wave.Destroy();
            _wave = null;
        }

        private void Subscribe()
        {
            _wave.OnDestroyed += OnDestroyed;
            _wave.OnEnemyDestroyed += OnEnemyDestroy;
            _wave.OnPathEndReached += OnPathEndReach;
        }

        private void Unsubscribe()
        {
            _wave.OnDestroyed -= OnDestroyed;
            _wave.OnEnemyDestroyed -= OnEnemyDestroy;
            _wave.OnPathEndReached -= OnPathEndReach;
        }

        private void OnDestroyed() => OnWaveDestroyed?.Invoke();

        private void OnEnemyDestroy(WaveLiveObjectData pack)
        {
            Debug.Log(pack.LiveObject.name);
            OnEnemyDestroyed?.Invoke();
        }

        private void OnPathEndReach() => OnPathEndReached?.Invoke();
    }
}
