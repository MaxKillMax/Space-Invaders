using System;
using UnityEngine;

namespace SI.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action, params object[] args)
        {
            Delegate[] delegates = action.GetInvocationList();

            for (int i = 0; i < delegates.Length; i++)
            {
                try
                {
                    delegates[i]?.DynamicInvoke(args);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}
