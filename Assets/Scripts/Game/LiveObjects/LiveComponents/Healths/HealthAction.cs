using System;

namespace SI.LiveObjects.LiveComponents.Healths
{
    /// <summary>
    /// The data you need to interact with health
    /// </summary>
    [Serializable]
    public struct HealthAction
    {
        public float Amount;

        /// <summary>
        /// Can act with different team indexs
        /// </summary>
        public uint[] TargetTeamIndexs;

        public HealthAction(float amount, params uint[] targetTeamIndexs)
        {
            Amount = amount;
            TargetTeamIndexs = targetTeamIndexs;
        }
    }
}
