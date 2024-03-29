using System;
using System.Collections.Generic;
using UnityEngine;

namespace SI.Waves
{
    /// <summary>
    /// Control all enemies in wave
    /// </summary>
    public class Wave
    {
        public event Action OnPathEndReached;
        public event Action<WaveLiveObjectData> OnEnemyDestroyed;
        public event Action OnDestroyed;

        private readonly Vector2[] _path;
        private List<WaveLiveObjectData> _liveObjectPacks;

        private readonly float _shootDelay;
        private float _delay = 0;

        public Wave(WaveConstructData data)
        {
            _path = data.Path;
            _shootDelay = data.ShootDelay;

            WaveEnemiesConfigurator configurator = new(data.Path, data.LiveObjects, onConfigured: MoveToNextPoint, onTargetReaching: MoveToNextPoint, onDestroyed: Remove);
            _liveObjectPacks = configurator.Configure();

            Times.Time.OnUpdate += WaitForShoot;
        }

        private void WaitForShoot()
        {
            _delay += Time.deltaTime;

            if (_delay < _shootDelay)
                return;

            _delay = 0;
            Shoot();
        }

        private void MoveToNextPoint(WaveLiveObjectData pack)
        {
            pack.PathIndex++;

            if (pack.PathIndex == _path.Length)
            {
                OnPathEndReached?.Invoke();
                return;
            }

            pack.TargetTracking.Track(_path[pack.PathIndex]);
            pack.Movement.Move(pack.LiveObject.transform.position.DirectionTo(_path[pack.PathIndex]));
        }

        private void Shoot()
        {
            WaveLiveObjectData randomPack = _liveObjectPacks[UnityEngine.Random.Range(0, _liveObjectPacks.Count)];
            randomPack.Attack.Launch();
        }

        private void Remove(WaveLiveObjectData pack)
        {
            Times.Time.OnStop -= pack.Movement.Stop;
            Times.Time.OnResume -= pack.Movement.Resume;

            _liveObjectPacks.Remove(pack);

            OnEnemyDestroyed?.Invoke(pack);

            if (_liveObjectPacks.Count == 0)
                Destroy();
        }

        public void Destroy()
        {
            for (int i = 0; i < _liveObjectPacks.Count; i++)
            {
                Times.Time.OnStop -= _liveObjectPacks[i].Movement.Stop;
                Times.Time.OnResume -= _liveObjectPacks[i].Movement.Resume;

                UnityEngine.Object.Destroy(_liveObjectPacks[i].LiveObject.gameObject);
            }

            _liveObjectPacks.Clear();

            Times.Time.OnUpdate -= WaitForShoot;
            OnDestroyed?.Invoke();
        }
    }
}
