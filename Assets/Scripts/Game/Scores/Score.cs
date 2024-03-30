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
        public const float HEALTH_SCORE_REDUCTION_MULTIPLIER = 2f;
        public const float START_WAVE_SCORE = 15;

        private int _current = START_WAVE_SCORE;

        public event Action OnUpdated;

        public int GetNativeScore() => _current;

        public int GetFullScore(float time, float removedHealthsCount)
        {
            float score = _current - (time * TIME_SCORE_REDUCTION_MULTIPLIER);
            score -= HEALTH_SCORE_REDUCTION_MULTIPLIER * removedHealthsCount;

            if (score < 0)
                score = 0;

            return Mathf.RoundToInt(score);
        }

        public void Add(int value)
        {
            _current += value;

            if (_current < 0)
                _current = 0;

            OnUpdated?.Invoke();
        }

        public void Clear()
        {
            _current = START_WAVE_SCORE;
            OnUpdated?.Invoke();
        }
    }
}
