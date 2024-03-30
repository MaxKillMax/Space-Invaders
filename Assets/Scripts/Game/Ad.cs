using System;
using CrazyGames;
using UnityEngine;

namespace SI
{
    public class Ad : MonoBehaviour
    {
        [SerializeField] private CrazyBanner _crazyBanner;
        [SerializeField] private AdWarning _adWarning;

        private void Awake()
        {
            _crazyBanner.MarkVisible(true);

            CrazyAds.Instance?.listenToBannerError(OnBannerError);
            CrazyAds.Instance?.listenToBannerRendered(OnBannerRendered);

            CrazyAds.Instance?.updateBannersDisplay();
        }

        public void ShowAd(CrazyAdType type, Action onEnded) => _adWarning.Launch(() => CrazyAds.Instance.beginAdBreak(onEnded.Invoke, () =>
        {
            Debug.LogError("Ad error");
            onEnded.Invoke();
        }, type));

        private void OnBannerError(string id, string error)
        {
            Debug.Log("Banner error for id " + id + ": " + error);
        }

        private void OnBannerRendered(string id)
        {
            Debug.Log("Banner rendered for id " + id);
        }

        private void OnDestroy()
        {
            _crazyBanner.MarkVisible(false);
            CrazyAds.Instance?.updateBannersDisplay();
        }
    }
}
