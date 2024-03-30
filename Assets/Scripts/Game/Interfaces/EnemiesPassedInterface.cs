using System;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.EnemiesPassedInterfaces
{
    public class EnemiesPassedInterface : Interface
    {
        [SerializeField] private Button _restartButton;

        public event Action OnRestartButtonClicked;

        protected override void OnInitialize()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
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
    }
}
