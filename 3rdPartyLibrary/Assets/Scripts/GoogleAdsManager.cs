/*
* ��ó : �������غ��� ��α� 
* ���� : Google Admob ���̺귯���� ����ϴ� Ŭ���� ����
* ��ũ : https://blog.naver.com/socceri/222972178570
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
        // RewardedAd�� ��ȸ�� ��ü�Դϴ�. ��, ������ ���� ��µ� �Ŀ��� �� ��ü�� ����� �ٸ� ���� �ε��� �� �����ϴ�. 
        // �ٸ� ������ ���� ��û�Ϸ��� �� RewardedAd ��ü�� ������ �մϴ�.
        RewardedAd _rewardedAd = new RewardedAd(UNITID_REWARDED_VIDEO);

        // ���� �ε尡 �Ϸ�� �� ����˴ϴ�.
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

        // ���� �ε忡 ������ �� ����˴ϴ�. ������ AdErrorEventArgs�� Message �Ӽ��� �߻��� ������ ������ �����մϴ�.
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        // ���� ǥ�õ� �� ����Ǹ� ��� ȭ���� �����ϴ�. �̶� �ʿ��� ��� ���� ����� ��� �Ǵ� ���� ������ �Ͻ������ϴ� ���� �����ϴ�.
        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;

        // ���� ǥ�ÿ� ������ �� ����˴ϴ�. ������ AdErrorEventArgs�� Message �Ӽ��� �߻��� ������ ������ �����մϴ�.
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // ����ڰ� ������ ��û�� ���� ������ �޾ƾ� �� �� ����˴ϴ�. Reward �Ű������� ����ڿ��� �����Ǵ� ������ �����մϴ�.
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // ����ڰ� �ݱ� �������� ���ϰų� �ڷ� ��ư�� ����Ͽ� ������ ������ ���� ���� �� ����˴ϴ�.
        // �ۿ��� ����� ��� �Ǵ� ���� ������ �Ͻ��������� �� �� �޼ҵ�� �簳�ϸ� ���մϴ�.
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
