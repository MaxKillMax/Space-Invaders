using System;

namespace SpaceInvaders.Scores
{
    public class Score
    {
        public event Action OnUpdated;

        private int _value;

        public int Current() => _value;

        public void Add()
        {
            _value++;
            OnUpdated?.Invoke();
        }

        public void Remove()
        {
            _value--;
            OnUpdated?.Invoke();
        }

        public void Clear()
        {
            _value = 0;
            OnUpdated?.Invoke();
        }
    }
}
