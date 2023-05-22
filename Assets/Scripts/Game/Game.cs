using SpaceInvaders.Controllers.Players;
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
        private static Game Instance;

        [SerializeField] private Sound _sound;
        [SerializeField] private Backgrounds.Background _background;
        [SerializeField] private GameInterface _gameInterface;
        [SerializeField] private WaveHandler _waveHandler;
        [SerializeField] private Controllers.Players.Player _player;

        private Score _score;
        private UInput _input;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);

            _input = new();
            _score = new();

            _sound.Initialize();
            _background.Initialize();
            Interface.Initialize();
            _player.Initialize();

            #region Subscribes
            _score.OnUpdated += () => _gameInterface.SetScore(_score.Current());
            _waveHandler.OnWaveIndexChanged += () => _gameInterface.SetWave(_waveHandler.WaveIndex + 1);
            _player.OnHealthChanged += () => _gameInterface.SetHealth(_player.Health, _player.MaxHealth);
            _player.OnDestroyed += Restart;
            _waveHandler.OnEnemyDestroyed += _score.Add;
            _waveHandler.OnDestroyed += Start;
            _waveHandler.OnPathEndReached += Restart;
            UInput.OnEscDown += Application.Quit;
            #endregion
        }

        private void OnDestroy()
        {
            #region Unsubscribes
            _score.OnUpdated -= () => _gameInterface.SetScore(_score.Current());
            _waveHandler.OnWaveIndexChanged -= () => _gameInterface.SetWave(_waveHandler.WaveIndex + 1);
            _player.OnHealthChanged -= () => _gameInterface.SetHealth(_player.Health, _player.MaxHealth);
            _player.OnDestroyed -= Restart;
            _waveHandler.OnEnemyDestroyed -= _score.Add;
            _waveHandler.OnDestroyed -= Start;
            _waveHandler.OnPathEndReached -= Restart;
            UInput.OnEscDown -= Application.Quit;
            #endregion
        }

        /// <summary>
        /// Start new wave (first wave or next)
        /// </summary>
        private void Start()
        {
            _waveHandler.LaunchNewWave();
        }

        /// <summary>
        /// Resets all settings to defaults
        /// </summary>
        private void Restart()
        {
            _score.Clear();
            _waveHandler.ResetIndex();
            _player.Initialize();
            Start();
        }

        private void Update()
        {
            _input.Update();
        }
    }
}
