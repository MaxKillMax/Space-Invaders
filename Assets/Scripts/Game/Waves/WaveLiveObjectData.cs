using System;
using SI.LiveObjects;
using SI.LiveObjects.LiveComponents.Attacks;
using SI.LiveObjects.LiveComponents.Healths;
using SI.LiveObjects.LiveComponents.Movements;
using SI.LiveObjects.LiveComponents.TargetTrackings;
using UnityEngine.Assertions;

namespace SI.Waves
{
    /// <summary>
    /// Packed enemy LiveObject for Waves
    /// </summary>
    [Serializable]
    public class WaveLiveObjectData
    {
        public LiveObject LiveObject;
        public TargetTracking TargetTracking;
        public Movement Movement;
        public Attack Attack;
        public Health Health;

        public int PathIndex;

        public WaveLiveObjectData(LiveObject liveObject)
        {
            LiveObject = liveObject;
            TargetTracking = liveObject.GetLiveComponent<TargetTracking>();
            Movement = liveObject.GetLiveComponent<Movement>();
            Attack = liveObject.GetLiveComponent<Attack>();
            Health = liveObject.GetLiveComponent<Health>();

            Assert.IsTrue(TargetTracking is not null && Movement is not null && Attack is not null && Health is not null);
        }
    }
}
