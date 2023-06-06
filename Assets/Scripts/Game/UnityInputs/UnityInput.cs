namespace SpaceInvaders.UInputs
{
    /// <summary>
    /// Contain events on different inputs (as facade)
    /// </summary>
    public class UnityInput
    {
        /// <summary>
        /// Alternative for Unity Update method
        /// </summary>
        public static event Action OnUpdate;
        public static event Action OnLmbDown;
        public static event Action OnEscDown;

        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }

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
