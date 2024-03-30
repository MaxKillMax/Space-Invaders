using System;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.GameInterfaces
{
    public class GameInterface : Interface
    {
        [SerializeField] private Image _healthImage;
        [SerializeField] private Sprite[] _healthSprites;
        [SerializeField] private Button _pauseButton;

        public event Action OnPauseButtonClicked;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
        }

        private void OnPauseButtonClick() => OnPauseButtonClicked?.Invoke();

        public void SetHealth(float health, float maxHealth)
        {
            int index = Mathf.RoundToInt(health / maxHealth * _healthSprites.Length) - 1;

            if (index < 0)
                index = 0;

            _healthImage.sprite = _healthSprites[index];
        }
    }
}
