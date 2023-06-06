using System;

namespace SpaceInvaders.Scores
{
    /// <summary>
    /// Provides methods, properties, and events for working with integer value
    /// </summary>
    public class Score
    {
        public event Action OnUpdated;

        public int Current { get; private set; } = 0;

        public void Add(int value)
        {
            Current += value;

            if (Current < 0)
                Current = 0;

            OnUpdated?.Invoke();
        }

        public void Clear()
        {
            Current = 0;
            OnUpdated?.Invoke();
        }
    }
}
