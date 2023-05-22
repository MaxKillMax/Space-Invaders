using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpaceInvaders.UInputs
{
    /// <summary>
    /// UnityInput. Contain events on different inputs (facade)
    /// </summary>
    public class UInput
    {
        private static UInput Instance;

        /// <summary>
        /// Alternative for Update method
        /// </summary>
        public static event Action OnUpdate;
        public static event Action OnLmbDown;
        public static event Action OnEscDown;

        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }

        public UInput()
        {
            Assert.IsNull(Instance);

            Instance = this;
        }

        public void Update()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            InvokeKeys();

            OnUpdate?.Invoke();
        }

        private void InvokeKeys()
        {
            if (Input.GetMouseButtonDown(0))
                OnLmbDown?.Invoke();

            if (Input.GetKeyDown(KeyCode.Escape))
                OnEscDown?.Invoke();
        }
    }
}
