using SI.Interfaces;
using SI.Interfaces.EndGameInterfaces;
using SI.Interfaces.GameInterfaces;
using SI.LiveObjects.LiveComponents.Healths;
using SI.Scores;
using SI.Sounds;
using SI.Waves;
using UnityEngine;

namespace SI
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Sound _sound;
        [SerializeField] private Backgrounds.Background _background;
        [SerializeField] private GameInterface _gameInterface;
        [SerializeField] private EndGameInterface _endGameInterface;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Controllers.Players.Player _player;

        private readonly Score _score = new();
        private readonly Inputs.Input _input = new();
        private readonly Times.Time _time = new();

        private void Start()
        {
            _sound.Initialize();
            _background.Initialize();
            Interface.Initialize();
            _player.TryCreateView();

            Subscribe();
            _waveHandler.StartNewWave();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Update()
        {
            _input.Update();
            _time.Update();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                _time.Stop();
        }

        /// <summary>
        /// Launching of new wave
        /// </summary>
        private void Next()
        {
            _score.Clear();
            _player.RecreateView();
            _waveHandler.StartNewWave();
        }

        /// <summary>
        /// Resets all settings to defaults
        /// </summary>
        private void Restart()
        {
            _score.Clear();
            _player.RecreateView();
            _waveHandler.RestartWave();
        }

        private void Subscribe()
        {
            _player.OnHealthChanged += OnPlayerHealthChanged;
            _player.OnDestroyed += Restart;

            _waveHandler.OnEnemyDestroyed += OnEnemyDestroyed;
            _waveHandler.OnWaveDestroyed += OnLevelPassed;
            _waveHandler.OnPathEndReached += Restart;

            _endGameInterface.OnNextLevelButtonClicked += OnNextLevelButtonClicked;
            _endGameInterface.OnRestartLevelButtonClicked += OnRestartButtonClicked;
            _endGameInterface.OnMultiplyScoreButtonClicked += MultiplyScore;
        }

        private void Unsubscribe()
        {
            _player.OnHealthChanged -= OnPlayerHealthChanged;
            _player.OnDestroyed -= Restart;

            _waveHandler.OnEnemyDestroyed -= OnEnemyDestroyed;
            _waveHandler.OnWaveDestroyed -= OnLevelPassed;
            _waveHandler.OnPathEndReached -= Restart;

            _endGameInterface.OnNextLevelButtonClicked -= OnNextLevelButtonClicked;
            _endGameInterface.OnRestartLevelButtonClicked -= OnRestartButtonClicked;
            _endGameInterface.OnMultiplyScoreButtonClicked -= MultiplyScore;
        }

        private void OnPlayerHealthChanged()
        {
            _gameInterface.SetHealth(_player.LiveObject.GetLiveComponent<Health>().Amount, _player.LiveObject.GetLiveComponent<Health>().MaxAmount);
        }

        private void OnEnemyDestroyed()
        {
            _score.Add(1);
        }

        private void OnLevelPassed()
        {
            _endGameInterface.ShowResults(_waveHandler.CountOfWaves, _score.GetTimeScore(_waveHandler.TotalTime));
        }

        private void OnNextLevelButtonClicked()
        {
            Interface.Single<GameInterface>();
            Next();
        }

        private void OnRestartButtonClicked()
        {
            Interface.Single<GameInterface>();
            Restart();
        }

        private void MultiplyScore()
        {
            _score.Add(_score.GetNativeScore());
            _endGameInterface.ShowResults(_waveHandler.CountOfWaves, _score.GetTimeScore(_waveHandler.TotalTime));
        }
    }
}
