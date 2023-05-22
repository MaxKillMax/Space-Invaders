using System;

namespace SpaceInvaders.LiveObjects.LiveComponents
{
    [Serializable]
    public abstract class LiveComponent
    {
        /// <summary>
        /// Allows to change the data to other data, without losing reference to the object instance
        /// </summary>
        /// <param name="component"></param>
        public abstract void TryReplace(LiveComponent component);
    }
}
