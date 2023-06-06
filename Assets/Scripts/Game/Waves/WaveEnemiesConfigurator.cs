using System;
using System.Collections.Generic;
using SpaceInvaders.LiveObjects;
using UnityEngine;

namespace SpaceInvaders.Waves
{
    /// <summary>
    /// Configures parameters for LiveObjects. Generates the result as packed dates
    /// </summary>
    public class WaveEnemiesConfigurator
    {
        private readonly Vector2[] _positions;
        private readonly LiveObject[] _liveObjects;

        private readonly Action<WaveLiveObjectData> _onConfigured;
        private readonly Action<WaveLiveObjectData> _onTargetReaching;
        private readonly Action<WaveLiveObjectData> _onDestroyed;

        public WaveEnemiesConfigurator(Vector2[] positions, LiveObject[] liveObjects, Action<WaveLiveObjectData> onConfigured, Action<WaveLiveObjectData> onTargetReaching, Action<WaveLiveObjectData> onDestroyed)
        {
            _positions = positions;
            _liveObjects = liveObjects;
            _onConfigured = onConfigured;
            _onTargetReaching = onTargetReaching;
            _onDestroyed = onDestroyed;
        }

        public List<WaveLiveObjectData> Configure()
        {
            List<WaveLiveObjectData> waveLives = new(_liveObjects.Length);

            for (int i = 0; i < _liveObjects.Length; i++)
            {
                WaveLiveObjectData pack = new(_liveObjects[i]) { PathIndex = i };
                pack.LiveObject.transform.position = _positions[i];
                pack.LiveObject.transform.name += i.ToString();

                pack.TargetTracking.OnTargetReaching += () => _onTargetReaching?.Invoke(pack);
                pack.Health.OnDestroyed += () => _onDestroyed?.Invoke(pack);

                _onConfigured?.Invoke(pack);
                waveLives.Add(pack);
            }

            return waveLives;
        }
    }
}
