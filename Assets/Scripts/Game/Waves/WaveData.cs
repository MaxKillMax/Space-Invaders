using System;
using SI.LiveObjects;
using UnityEngine;

namespace SI.Waves
{
    [CreateAssetMenu(fileName = nameof(WaveData), menuName = nameof(WaveData), order = 51)]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private Vector2[] _path;
        [SerializeField] private LiveObjectData _prefab;
        [SerializeField] private uint _count;
        [SerializeField, Range(0, 100)] private float _shootDelay;

        private void OnValidate()
        {
            if (_path.Length <= _count * 2)
                Debug.LogWarning("The number of enemies is too high in wave in relation to path");
        }

        public Wave Create(Transform parent)
        {
            LiveObject[] liveObjects = new LiveObject[_count];

            for (int i = 0; i < _count; i++)
            {
                liveObjects[i] = _prefab.Create(parent, parent.position);
                liveObjects[i].transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            return new(new()
            {
                Path = _path,
                LiveObjects = liveObjects,
                ShootDelay = _shootDelay
            });
        }
    }
}
