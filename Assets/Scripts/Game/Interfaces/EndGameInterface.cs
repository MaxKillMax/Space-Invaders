using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.EndGameInterfaces
{
    public class EndGameInterface : Interface
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _scoreText;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _multiplyScoreButton;

        public event Action OnNextLevelButtonClicked;
        public event Action OnRestartLevelButtonClicked;
        public event Action OnMultiplyScoreButtonClicked;

        protected override void OnInitialize()
        {
            _nextLevelButton.onClick.AddListener(() => OnNextLevelButtonClicked?.Invoke());
            _restartLevelButton.onClick.AddListener(() => OnRestartLevelButtonClicked?.Invoke());
            _multiplyScoreButton.onClick.AddListener(() =>
            {
                _multiplyScoreButton.interactable = false;
                OnMultiplyScoreButtonClicked?.Invoke();
            });
        }

        public void ShowResults(int level, int score)
        {
            _levelText.text = (level + 1).ToString();
            _scoreText.text = score.ToString();

            Single<EndGameInterface>();
        }

        protected override void OnOpen()
        {
            _multiplyScoreButton.interactable = true;
            Game.SetPauseState(true);
        }

        protected override void OnClose()
        {
            Game.SetPauseState(false);
        }
    }
}
