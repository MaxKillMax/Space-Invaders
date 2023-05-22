using System;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    /// <summary>
    /// Allow to wait with boolean conditions
    /// </summary>
    public static class WaitTask
    {
        public static async Task While(Func<bool> condition)
        {
            if (condition is null)
                return;

            while (condition.Invoke())
                await Task.Yield();
        }
    }
}
