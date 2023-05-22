using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvaders.Interfaces.GameInterfaces
{
    public class GameInterface : Interface
    {
        [SerializeField] private TMP_Text _waveValueText;
        [SerializeField] private TMP_Text _scoreValueText;

        [SerializeField] private Image _healthImage;
        [SerializeField] private Sprite[] _healthSprites;

        public void SetWave(int wave) => _waveValueText.text = wave.ToString();

        public void SetScore(int score) => _scoreValueText.text = score.ToString();

        public void SetHealth(float health, float maxHealth)
        {
            int index = Mathf.RoundToInt(health / maxHealth * _healthSprites.Length) - 1;

            if (index < 0)
                index = 0;

            _healthImage.sprite = _healthSprites[index];
        }
    }
}
