using System;
using UnityEngine;
using UnityEngine.UI;

namespace SI.Interfaces.Elements
{
    public class SpriteToggle : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private Button _button;

        [Space]

        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _panelImage;

        [Space]

        [SerializeField] private Sprite _onIconSprite;
        [SerializeField] private Sprite _offIconSprite;

        [Space]

        [SerializeField] private Sprite _onPanelSprite;
        [SerializeField] private Sprite _offPanelSprite;

        private bool _isOn = true;

        public event Action<bool> OnValueChanged;

        private void Awake()
        {
            if (Saving.Load(_key, out bool result))
                _isOn = result;

            _button.onClick.AddListener(OnValueChange);
            UpdateState();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnValueChange);
        }

        private void OnValueChange()
        {
            _isOn = !_isOn;
            UpdateState();
        }

        private void UpdateState()
        {
            Saving.Save(_key, _isOn);

            _iconImage.sprite = _isOn ? _onIconSprite : _offIconSprite;
            _panelImage.sprite = _isOn ? _onPanelSprite : _offPanelSprite;

            OnValueChanged?.Invoke(_isOn);
        }
    }
}