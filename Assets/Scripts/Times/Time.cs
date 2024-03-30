using System;
using SI.Extensions;

namespace SI.Times
{
    public class Time
    {
        public static float Delta { get; private set; }
        public static bool IsStopped { get; private set; } = true;

        public static event Action OnUpdate;
        public static event Action OnStop;
        public static event Action OnResume;

        public void Update()
        {
            if (IsStopped)
            {
                IsStopped = false;
                OnResume?.Invoke();
            }

            Delta = UnityEngine.Time.deltaTime;
            OnUpdate?.SafeInvoke();
        }

        public void Stop()
        {
            if (IsStopped)
                return;

            IsStopped = true;

            Delta = 0;
            OnStop?.SafeInvoke();
        }
    }
}