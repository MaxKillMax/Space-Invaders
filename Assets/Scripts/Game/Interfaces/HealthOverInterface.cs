using System;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.HealthOverInterfaces
{
    public class HealthOverInterface : Interface
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _restoreHealthButton;

        public event Action OnRestartButtonClicked;
        public event Action OnRestoreButtonClicked;

        protected override void OnInitialize()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _restoreHealthButton.onClick.AddListener(OnRestoreButtonClick);
        }

        protected override void OnOpen()
        {
            Game.SetPauseState(true);
        }

        protected override void OnClose()
        {
            Game.SetPauseState(false);
        }

        private void OnRestartButtonClick() => OnRestartButtonClicked?.Invoke();

        private void OnRestoreButtonClick() => OnRestoreButtonClicked?.Invoke();
    }
}