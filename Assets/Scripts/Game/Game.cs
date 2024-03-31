using CrazyGames;
using SI.Controllers.Players;
using SI.Interfaces;
using SI.Interfaces.EndGameInterfaces;
using SI.Interfaces.EnemiesPassedInterfaces;
using SI.Interfaces.GameInterfaces;
using SI.Interfaces.HealthOverInterfaces;
using SI.Interfaces.PauseInterfaces;
using SI.Interfaces.StoreInterfaces;
using SI.LiveObjects.LiveComponents.Healths;
using SI.Scores;
using SI.Sounds;
using SI.Waves;
using UnityEngine;

namespace SI
{
    public class Game : MonoBehaviour
    {
        private static Game Instance;

        [SerializeField] private Sound _sound;
        [SerializeField] private Backgrounds.Background _background;
        [SerializeField] private ResearchTree _researchTree;
        [SerializeField] private GameInterface _gameInterface;
        [SerializeField] private EndGameInterface _endGameInterface;
        [SerializeField] private HealthOverInterface _healthOverInterface;
        [SerializeField] private EnemiesPassedInterface _enemiesPassedInterface;
        [SerializeField] private PauseInterface _pauseInterface;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Controllers.Players.Player _player;
        [SerializeField] private PlayerComponentHandler _componentHandler;

        private readonly Score _score = new();
        private readonly Inputs.Input _input = new();
        private readonly Times.Time _time = new();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _componentHandler.Initialize();
            _researchTree.Initialize();
            _player.Initialize();
            _sound.Initialize();
            _background.Initialize();

            Subscribe();
            Next();
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

        public static void SetPauseState(bool state)
        {
            if (state)
            {
                Instance._time.Stop();
                Instance._input.Stop();
            }

            Instance.enabled = !state;
        }

        private void Subscribe()
        {
            _player.OnHealthChanged += OnPlayerHealthChanged;
            _player.OnDestroyed += OnPlayerDestroyed;

            _waveHandler.OnEnemyDestroyed += OnEnemyDestroyed;
            _waveHandler.OnWaveDestroyed += OnLevelPassed;
            _waveHandler.OnPathEndReached += OnPathEnded;

            _gameInterface.OnPauseButtonClicked += OnPauseButtonClicked;

            _endGameInterface.OnNextLevelButtonClicked += OnNextLevelButtonClicked;
            _endGameInterface.OnRestartLevelButtonClicked += OnRestartButtonClicked;
            _endGameInterface.OnMultiplyScoreButtonClicked += MultiplyScore;

            _healthOverInterface.OnRestartButtonClicked += OnRestartButtonClicked;
            _healthOverInterface.OnRestoreButtonClicked += OnRestoreButtonClicked;

            _enemiesPassedInterface.OnRestartButtonClicked += OnRestartButtonClicked;

            _pauseInterface.OnRestartButtonClicked += OnRestartButtonClicked;
            _pauseInterface.OnContinueButtonClicked += OnResumeButtonClicked;
            _pauseInterface.OnStoreButtonClicked += OnStoreButtonClicked;
        }

        private void Unsubscribe()
        {
            _player.OnHealthChanged -= OnPlayerHealthChanged;
            _player.OnDestroyed -= OnPlayerDestroyed;

            _waveHandler.OnEnemyDestroyed -= OnEnemyDestroyed;
            _waveHandler.OnWaveDestroyed -= OnLevelPassed;
            _waveHandler.OnPathEndReached -= OnPathEnded;

            _gameInterface.OnPauseButtonClicked -= OnPauseButtonClicked;

            _endGameInterface.OnNextLevelButtonClicked -= OnNextLevelButtonClicked;
            _endGameInterface.OnRestartLevelButtonClicked -= OnRestartButtonClicked;
            _endGameInterface.OnMultiplyScoreButtonClicked -= MultiplyScore;

            _healthOverInterface.OnRestartButtonClicked -= OnRestartButtonClicked;
            _healthOverInterface.OnRestoreButtonClicked -= OnRestoreButtonClicked;

            _enemiesPassedInterface.OnRestartButtonClicked -= OnRestartButtonClicked;

            _pauseInterface.OnRestartButtonClicked -= OnRestartButtonClicked;
            _pauseInterface.OnContinueButtonClicked -= OnResumeButtonClicked;
            _pauseInterface.OnStoreButtonClicked -= OnStoreButtonClicked;
        }

        private void OnPlayerHealthChanged()
        {
            Health health = _player.LiveObject.GetLiveComponent<Health>();
            _gameInterface.SetHealth(health.Amount, health.MaxAmount);
        }

        private void OnPlayerDestroyed()
        {
            Ad.Show(CrazyAdType.midgame, Interface.Single<HealthOverInterface>);
        }

        private void OnPathEnded()
        {
            Ad.Show(CrazyAdType.midgame, Interface.Single<EnemiesPassedInterface>);
        }

        private void OnEnemyDestroyed()
        {
            _score.Add(1);
        }

        private void OnLevelPassed()
        {
            Health health = _player.LiveObject.GetLiveComponent<Health>();
            int score = _score.GetFullScore(_waveHandler.TotalTime, health.MaxAmount - health.Amount);

            Controllers.Players.Player.Points += score;
            Ad.Show(CrazyAdType.midgame, () => _endGameInterface.ShowResults(_waveHandler.CountOfWaves, score));
        }

        private void OnPauseButtonClicked()
        {
            Interface.Single<PauseInterface>();
        }

        private void OnStoreButtonClicked()
        {
            Interface.Single<StoreInterface>();
        }

        private void OnResumeButtonClicked()
        {
            Interface.Single<GameInterface>();
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

        private void OnRestoreButtonClicked()
        {
            Ad.Show(CrazyAdType.rewarded, () =>
            {
                Interface.Single<GameInterface>();
                _player.RecreateView();
            });
        }

        private void MultiplyScore()
        {
            Ad.Show(CrazyAdType.rewarded, () =>
            {
                _score.Add(_score.GetNativeScore());
                Health health = _player.LiveObject.GetLiveComponent<Health>();
                _endGameInterface.ShowResults(_waveHandler.CountOfWaves, _score.GetFullScore(_waveHandler.TotalTime, health.MaxAmount - health.Amount));
            });
        }
    }
}
