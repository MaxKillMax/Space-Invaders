using SpaceInvaders.Interfaces;
using SpaceInvaders.Interfaces.GameInterfaces;
using SpaceInvaders.Scores;
using SpaceInvaders.Sounds;
using SpaceInvaders.UInputs;
using SpaceInvaders.Waves;
using UnityEngine;

namespace SpaceInvaders
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Sound _sound;
        [SerializeField] private Backgrounds.Background _background;
        [SerializeField] private GameInterface _gameInterface;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Controllers.Players.Player _player;

        private Score _score;
        private UInput _input;

        private void Start()
        {
            InitializeSystems();
            Subscribe();

            _waveHandler.LaunchNewWave();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Update()
        {
            _input.Update();
        }

        /// <summary>
        /// Resets all settings to defaults
        /// </summary>
        private void Restart()
        {
            _score.Clear();
            _waveHandler.ResetCounter();
            _player.Initialize();
            _waveHandler.LaunchNewWave();
        }

        private void InitializeSystems()
        {
            _input = new();
            _score = new();

            _sound.Initialize();
            _background.Initialize();
            Interface.Initialize();
            _player.Initialize();
        }

        private void Subscribe()
        {
            _waveHandler.OnWaveCountChanged += () => _gameInterface.SetWave(_waveHandler.CountOfWaves + 1);
            _score.OnUpdated += () => _gameInterface.SetScore(_score.Current);
            _player.OnHealthChanged += () => _gameInterface.SetHealth(_player.Health, _player.MaxHealth);

            _player.OnDestroyed += Restart;
            _waveHandler.OnWaveEnemyDestroyed += () => _score.Add(1);
            _waveHandler.OnWaveDestroyed += Start;
            _waveHandler.OnWavePathEndReached += Restart;

            UInput.OnEscDown += Application.Quit;
        }

        private void Unsubscribe()
        {
            _waveHandler.OnWaveCountChanged -= () => _gameInterface.SetWave(_waveHandler.CountOfWaves + 1);
            _score.OnUpdated -= () => _gameInterface.SetScore(_score.Current);
            _player.OnHealthChanged -= () => _gameInterface.SetHealth(_player.Health, _player.MaxHealth);

            _player.OnDestroyed -= Restart;
            _waveHandler.OnWaveEnemyDestroyed -= () => _score.Add(1);
            _waveHandler.OnWaveDestroyed -= _waveHandler.LaunchNewWave;
            _waveHandler.OnWavePathEndReached -= Restart;

            UInput.OnEscDown -= Application.Quit;
        }
    }
}
