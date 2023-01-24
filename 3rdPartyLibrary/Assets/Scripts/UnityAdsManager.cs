/*
* 출처 : 개발자준비중 블로그 
* 내용 : Unity Ads 라이브러리를 사용하는 클래스 구현
* 링크 : https://blog.naver.com/socceri/222972178074
*/
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : AdsAdapter, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //광고 SDK 사용할 ID 값.
    private const string GAME_ID = "1111111";
    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded";

    #region Interface Implementations - IAdsManager
    public override void Init(Action<EV_RESULT_ADS> _OnFinish_Show)
    {
        base.Init(_OnFinish_Show);

        if (Advertisement.isSupported)
        {
            Debug.Log(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(GAME_ID, false, false, this);
    }
    //public override bool IsInit() { }
    public override void Load()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }
    public override bool IsLoaded()
    {
        return Advertisement.IsReady(REWARDED_VIDEO_PLACEMENT);
    }
    public override void Show()
    {
        if (IsInit())
            Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }
    #endregion

    #region Interface Implementations / UnityAds
    public void OnInitializationComplete()
    {
        Debug.Log("Init Success");
        IsSettingComplete = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");

        EV_RESULT_ADS _result = EV_RESULT_ADS.NONE;
        switch (showCompletionState)
        {
            // 동영상을 끝까지 본 경우
            case UnityAdsShowCompletionState.COMPLETED:
                _result = EV_RESULT_ADS.Finished;
                break;

            // 유저가 중간에 동영상을 스킵한 경우
            case UnityAdsShowCompletionState.SKIPPED:
                _result = EV_RESULT_ADS.Skipped;
                break;

            // 동영상 재생 실패
            case UnityAdsShowCompletionState.UNKNOWN:
                _result = EV_RESULT_ADS.Failed;
                break;
        }

        if (OnFinish_Show != null)
            OnFinish_Show(_result);
    }
    #endregion
}
