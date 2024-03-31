using System;
using SI.Interfaces.Elements;
using SI.Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.PauseInterfaces
{
    public class PauseInterface : Interface
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _storeButton;

        [SerializeField] private SpriteToggle _soundToggle;
        [SerializeField] private string _soundKey;

        [SerializeField] private SpriteToggle _musicToggle;
        [SerializeField] private string _musicKey;

        public event Action OnRestartButtonClicked;
        public event Action OnContinueButtonClicked;
        public event Action OnStoreButtonClicked;

        protected override void OnInitialize()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _continueButton.onClick.AddListener(OnContinueButtonClick);
            _storeButton.onClick.AddListener(OnStoreButtonClick);

            _soundToggle.OnValueChanged += OnSoundToggleValueChanged;
            _musicToggle.OnValueChanged += OnMusicToggleValueChanged;
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _continueButton.onClick.RemoveListener(OnContinueButtonClick);
            _storeButton.onClick.RemoveListener(OnStoreButtonClick);

            _soundToggle.OnValueChanged -= OnSoundToggleValueChanged;
            _musicToggle.OnValueChanged -= OnMusicToggleValueChanged;
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

        private void OnContinueButtonClick() => OnContinueButtonClicked?.Invoke();

        private void OnStoreButtonClick() => OnStoreButtonClicked?.Invoke();

        private void OnSoundToggleValueChanged(bool state) => Sound.SetGroupVolume(_soundKey, state ? 0 : -80);

        private void OnMusicToggleValueChanged(bool state) => Sound.SetGroupVolume(_musicKey, state ? 0 : -80);
    }
}

namespace SI.Interfaces.StoreInterfaces
{
    public class StoreInterface : Interface
    {

    }
}