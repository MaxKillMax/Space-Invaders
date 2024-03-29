using System.Collections.Generic;
using UnityEngine;

namespace SI.Interfaces
{
    /// <summary>
    /// Base class for all comlex ui objects (for example: ShopInterface, GameInterface, SettingsInterface)
    /// </summary>
    public abstract partial class Interface : MonoBehaviour
    {
        public bool State { get; private set; }

        private void Awake()
        {
            Objects.Add(this);

            State = gameObject.activeSelf;
        }

        private void OnDestroy()
        {
            Objects.Remove(this);
        }

        public void SetState(bool state)
        {
            if (State == state)
                return;

            State = state;
            gameObject.SetActive(state);

            if (state)
                OnOpen();
            else
                OnClose();
        }

        protected virtual void OnInitialize() { }

        protected virtual void OnOpen() { }

        protected virtual void OnClose() { }
    }

    public partial class Interface
    {
        private static readonly List<Interface> Objects = new();

        public static void Initialize() => Objects.ForEach((i) => i.OnInitialize());

        public static void Single<T>() => Objects.ForEach((i) => i.SetState(i.GetType() == typeof(T)));
    }
}
