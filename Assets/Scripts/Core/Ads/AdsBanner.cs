using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    [System.Serializable]
    public class AdsBanner
    {
        [SerializeField]
        private BannerPosition position;

        [SerializeField]
        private string androidID;
        [SerializeField]
        private string IOSID;


        public void StartAd()
        {
            Advertisement.Banner.SetPosition(position);

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnLoaded,
                errorCallback = OnError
            };

            Advertisement.Banner.Load(Application.platform == RuntimePlatform.IPhonePlayer ? IOSID : androidID, options);
        }

        private void OnLoaded()
        {
            ShowBannerAd();
        }
        private void OnError(string errorMessage)
        {
            Debug.LogError("Ads banner error: " + errorMessage);
        }
        private void ShowBannerAd()
        {
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            Advertisement.Banner.Show(Application.platform == RuntimePlatform.IPhonePlayer ? IOSID : androidID, options);
        }

        private void OnBannerShown()
        {

        }

        private void OnBannerHidden()
        {

        }

        private void OnBannerClicked()
        {

        }
    }
}
