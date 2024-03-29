using System;
using System.Collections.Generic;
using UnityEngine;

namespace SI.Inputs
{
    public class Input
    {
        private static readonly List<KeyValuePair<KeyCode, Action>> KeyCodeDownListeners = new();

        public static event Action OnEscapeClicked;
        public static event Action OnPointerDown;
        public static event Action OnPointerUp;

        public static float Horizontal => UnityEngine.Input.GetAxis("Horizontal");
        public static float Vertical => UnityEngine.Input.GetAxis("Vertical");

        public static bool IsPointerHolding { get; private set; }

        public static void SetKeyCodeDownListenState(bool state, KeyCode keyCode, Action action)
        {
            KeyValuePair<KeyCode, Action> pair = new(keyCode, action);

            if (state && !KeyCodeDownListeners.Contains(pair))
                KeyCodeDownListeners.Add(pair);
            else if (!state && KeyCodeDownListeners.Contains(pair))
                KeyCodeDownListeners.Remove(pair);
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                OnEscapeClicked?.Invoke();

            IsPointerHolding = UnityEngine.Input.GetMouseButton(0) || (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved);

            if (UnityEngine.Input.GetMouseButtonDown(0) || (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began))
                OnPointerDown?.Invoke();

            if (UnityEngine.Input.GetMouseButtonUp(0) || (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended))
                OnPointerUp?.Invoke();

            for (int i = 0; i < KeyCodeDownListeners.Count; i++)
            {
                try
                {
                    if (UnityEngine.Input.GetKeyDown(KeyCodeDownListeners[i].Key))
                        KeyCodeDownListeners[i].Value?.Invoke();
                }
                catch (Exception exception)
                {
                    Debug.LogWarning(exception);
                }
            }
        }
    }
}