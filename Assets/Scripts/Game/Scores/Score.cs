using System;
using UnityEngine;

namespace SI.Scores
{
    /// <summary>
    /// Provides methods, properties, and events for working with integer value
    /// </summary>
    public class Score
    {
        public const float TIME_SCORE_REDUCTION_MULTIPLIER = 0.2f;

        private int _current = 0;

        public event Action OnUpdated;

        public int GetNativeScore() => _current;

        public int GetTimeScore(float time)
        {
            float score = _current - (time * TIME_SCORE_REDUCTION_MULTIPLIER);

            if (score < 0)
                score = 0;

            return Mathf.RoundToInt(score);
        }

        public void Add(int value)
        {
            Debug.Log("Add");
            _current += value;

            if (_current < 0)
                _current = 0;

            OnUpdated?.Invoke();
        }

        public void Clear()
        {
            _current = 0;
            OnUpdated?.Invoke();
        }
    }
}
