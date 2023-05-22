using System;

namespace SpaceInvaders.LiveObjects.LiveComponents.Healths
{
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
