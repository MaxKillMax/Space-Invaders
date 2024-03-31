using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace SI.Controllers.Players
{
    public class PlayerComponentHandler : MonoBehaviour
    {
        private static PlayerComponentHandler Instance;

        [SerializeField] private ScriptableObject[] _components;

        public void Initialize()
        {
            Assert.IsNull(Instance);

            Instance = this;
        }

        public static void ModifyComponent<T>(Action<T> action) where T : ScriptableObject
        {
            T component = Instance._components.OfType<T>().First();
            action.Invoke(component);
        }
    }
}
