/*
* 출처 : 개발자준비중 블로그 
* 내용 : 여러개의 광고 모듈을 번갈아 가며 사용하는 매니져 클래스 구현
* 링크 : https://blog.naver.com/socceri/222972184913
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoSingleton<AdsManager>
{
    #region 멤버변수. 참조변수.
    /// <summary>
    /// Const Values
    /// </summary>
    const int INDEX_NOT_FIND = -1;

    /// <summary>
    /// Member Values
    /// </summary>
    int m_index_show = INDEX_NOT_FIND;

    public bool IsInitFinish
    {
        get
        {
            bool _retval = true;
            foreach (var _adsmgr in r_list_Managers)
            {
                if (!_adsmgr.IsInit())
                    _retval = false;
            }
            return _retval;
        }
    }

    /// <summary>
    /// Reference values
    /// </summary>
    List<IAdsAdapter> r_list_Managers = new List<IAdsAdapter>();
    #endregion

    #region Unity 함수 구현
    /// <summary>
    /// functions - Unity
    /// </summary>
    void Awake()
    {
        //Regist Instance for MonoSingleton<>
        base.Regist(this);
    }
    void Start()
    {
        Init();
    }
    #endregion

    #region 멤버함수
    /// <summary>
    /// functions - Member
    /// </summary>
    void Init()
    {
        r_list_Managers.Add(new GoogleAdsManager());
        r_list_Managers.Add(new UnityAdsManager());

        StartCoroutine(InitAll_Cor());
    }

    IEnumerator InitAll_Cor()
    {
        //init all
        foreach (var _adsmgr in r_list_Managers)
            _adsmgr.Init(OnFinishShowAds);

        //wait for Initailizing
        while (!IsInitFinish)
            yield return null;

        LoadAll();
    }
    void LoadAll()
    {
        //Load All
        foreach (var _adsmgr in r_list_Managers)
            _adsmgr.Load();
    }

    /// <summary>
    /// 리워드 광고를 보여주는 함수.
    /// </summary>
    public void ShowRewardAds()
    {
        // 인터넷 연결 상태 체크.
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            m_index_show = NextIndex();
            if (m_index_show != INDEX_NOT_FIND)
            {
                Time.timeScale = 0f;
                AnalyticsManager.Instance.AnalyticsEvent("Show_Ad");
                r_list_Managers[m_index_show].Show();
            }
            else //시청 가능한 광고가 없을 때.
            {
                Debug.LogError("현재 시청 가능한 광고가 없습니다.");
                LoadAll();
            }
        }
        else
        {
            Debug.LogError("인터넷에 연결되어있지 않습니다.");
        }
    }
    //시청 가능한 광고가 있는지 index로 반환하는 함수.
    int NextIndex()
    {
        int _retval = INDEX_NOT_FIND;

        for (int i = 0; i < r_list_Managers.Count; i++)
        {
            int _index_check = m_index_show + 1 + i;

            if (_index_check >= r_list_Managers.Count)
                _index_check -= r_list_Managers.Count;

            if (r_list_Managers[_index_check].IsLoaded())
            {
                _retval = _index_check;
                break;
            }
        }
        return _retval;
    }

    /// <summary>
    /// 동영상 시청 완료시 호출되는 결과 함수
    /// </summary>
    /// <param name="_result"></param>
    void OnFinishShowAds(EV_RESULT_ADS _result)
    {
        Time.timeScale = 1f;

        switch (_result)
        {
            // 동영상을 끝까지 본 경우
            case EV_RESULT_ADS.Finished:
                AnalyticsManager.Instance.AnalyticsEvent("Finished_Ad");
                break;

            // 유저가 중간에 동영상을 스킵한 경우
            case EV_RESULT_ADS.Skipped:
                AnalyticsManager.Instance.AnalyticsEvent("Skipped_Ad");
                break;

            // 동영상 재생 실패
            case EV_RESULT_ADS.Failed:
                AnalyticsManager.Instance.AnalyticsEvent("Failed_Ad");
                break;

            case EV_RESULT_ADS.NONE:
            default:
                Debug.LogError("ERROR :: 예기치 못한 결과.");
                break;
        }

        r_list_Managers[m_index_show].Load();
    }
    #endregion
}
