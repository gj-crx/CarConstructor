using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        public AdsBanner BannerAd;

        [SerializeField]
        private string androidGameID;
        [SerializeField]
        private string IOSGameID;

        [SerializeField]
        private bool testMode = true;



        void Start() => StartDelayedInitialization();
        private async void StartDelayedInitialization()
        {
            await Task.Delay(1000);

            Advertisement.Initialize(Application.platform == RuntimePlatform.IPhonePlayer ? IOSGameID : androidGameID, testMode, this);

        }
        public void OnInitializationComplete()
        {
            BannerAd.StartAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError("Initialization failed");
        }
    }
}
