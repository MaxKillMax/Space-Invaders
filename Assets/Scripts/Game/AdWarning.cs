using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SI
{
    public class AdWarning : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private float _delay = 1.5f;
        [SerializeField] private int _ticks = 3;

        private Action _onEnded;
        private float TickDelay => _delay / _ticks;

        public void Launch(Action onEnded)
        {
            _onEnded = onEnded;

            gameObject.SetActive(true);
            StartCoroutine(WaitForTicks());
        }

        private IEnumerator WaitForTicks()
        {
            int currentTick = 0;

            while (currentTick < _ticks)
            {
                _text.text = (_ticks - currentTick).ToString();
                yield return new WaitForSeconds(TickDelay);
                currentTick++;
            }

            gameObject.SetActive(false);
            _onEnded.Invoke();
        }
    }
}
