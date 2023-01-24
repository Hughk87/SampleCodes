/*
* 출처 : 개발자준비중 블로그 
* 내용 : Google Admob 라이브러리를 사용하는 클래스 구현
* 링크 : https://blog.naver.com/socceri/222972178570
*/
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdsManager : AdsAdapter
{
    private const string UNITID_REWARDED_VIDEO = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;
    private EV_RESULT_ADS m_result = EV_RESULT_ADS.NONE;;

    #region Interface Implementations - IAdsManager
    public override void Init(Action<EV_RESULT_ADS> _OnFinish_Show)
    {
        base.Init(_OnFinish_Show);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(OnFinish_Initialize);
    }

    void OnFinish_Initialize(InitializationStatus _status)
    {
        IsSettingComplete = true;
    }

    //public override bool IsInit() { }
    public override void Load()
    {
        // RewardedAd는 일회용 객체입니다. 즉, 보상형 광고가 출력된 후에는 이 객체를 사용해 다른 광고를 로드할 수 없습니다. 
        // 다른 보상형 광고를 요청하려면 새 RewardedAd 객체를 만들어야 합니다.
        RewardedAd _rewardedAd = new RewardedAd(UNITID_REWARDED_VIDEO);

        // 광고 로드가 완료될 때 실행됩니다.
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        // 광고 로드에 실패할 때 실행됩니다. 제공된 AdErrorEventArgs의 Message 속성은 발생한 실패의 유형을 설명합니다.
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // 광고가 표시될 때 실행되며 기기 화면을 덮습니다. 이때 필요한 경우 앱의 오디오 출력 또는 게임 루프를 일시중지하는 것이 좋습니다.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        // 광고 표시에 실패할 때 실행됩니다. 제공된 AdErrorEventArgs의 Message 속성은 발생한 실패의 유형을 설명합니다.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // 사용자가 동영상 시청에 대한 보상을 받아야 할 때 실행됩니다. Reward 매개변수는 사용자에게 제공되는 보상을 설명합니다.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // 사용자가 닫기 아이콘을 탭하거나 뒤로 버튼을 사용하여 보상형 동영상 광고를 닫을 때 실행됩니다.
        // 앱에서 오디오 출력 또는 게임 루프를 일시중지했을 때 이 메소드로 재개하면 편리합니다.
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        _rewardedAd.LoadAd(request);

        rewardedAd = _rewardedAd;
    }
    public override bool IsLoaded()
    {
        if (rewardedAd == null)
            return false;
        else
            return rewardedAd.IsLoaded();
    }
    public override void Show()
    {
        if (IsLoaded())
        {
            m_result = EV_RESULT_ADS.NONE;
            rewardedAd.Show();
        }
    }
    #endregion

    #region Interface Implementations / GoogleAds
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        Debug.Log($"HandleRewardedAdFailedToLoad event received with message: {args.Message}");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log($"HandleRewardedAdFailedToShow event received with message: {args.Message}");
        CallBack_OnFinish_Show(EV_RESULT_ADS.Failed);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
        if (m_result == EV_RESULT_ADS.Finished)
            CallBack_OnFinish_Show(m_result);
        else
            CallBack_OnFinish_Show(EV_RESULT_ADS.Skipped);
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log($"HandleRewardedAdRewarded event received for {amount.ToString()} {type}");
        m_result = EV_RESULT_ADS.Finished;
    }

    void CallBack_OnFinish_Show(EV_RESULT_ADS _result)
    {
        if (OnFinish_Show != null)
            OnFinish_Show(_result);
    }


    #endregion
}
