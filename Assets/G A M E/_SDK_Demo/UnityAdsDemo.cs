#if UNITY_ADS_SDK
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using SDK_Demo.Common;

namespace SDK_Demo.UnityAdsDemo // This script demonstrates how to integrate Unity Ads into a Unity project.
{
    public class UnityAdsDemo : MonoBehaviour, ISdkModule, IUnityAdsInitializationListener, IUnityAdsShowListener
    {
        [Header("UI")]
        public Button showAdButton;
        public Text statusText;

        [Header("Settings")]
        [SerializeField] private string androidGameId = "1234567";
        [SerializeField] private string iosGameId = "7654321";
        [SerializeField] private bool testMode = true;

        public string ModuleName => "Unity Ads";
        public bool IsInitialized { get; private set; }

        private string adUnitId = "Interstitial_Android";

        void Start()
        {
            showAdButton.interactable = false;
            showAdButton.onClick.AddListener(ShowAd);
            Initialize();
        }

        public void Initialize()
        {
            string gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosGameId : androidGameId;
            Advertisement.Initialize(gameId, testMode, this);
            statusText.text = "Initializing Unity Ads...";
        }

        public void OnInitializationComplete()
        {
            IsInitialized = true;
            showAdButton.interactable = true;
            statusText.text = "Unity Ads Ready!";
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            statusText.text = $"Init failed: {error.ToString()} - {message}";
        }

        public void ShowAd()
        {
            if (Advertisement.IsReady(adUnitId))
            {
                Advertisement.Show(adUnitId, this);
            }
            else
            {
                statusText.text = "Ad not ready yet.";
            }
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            statusText.text = $"Ad finished: {showCompletionState}";
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            statusText.text = $"Ad error: {error} - {message}";
        }

        public void OnUnityAdsShowStart(string placementId) { }
        public void OnUnityAdsShowClick(string placementId) { }
    }
}
#endif
