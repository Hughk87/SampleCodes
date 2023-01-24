/*
* ��ó : �������غ��� ��α� 
* ���� : �������� ���� ����� ������ ���� ����ϴ� �Ŵ��� Ŭ���� ����
* ��ũ : https://blog.naver.com/socceri/222972184913
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoSingleton<AdsManager>
{
    #region �������. ��������.
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

    #region Unity �Լ� ����
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

    #region ����Լ�
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
    /// ������ ���� �����ִ� �Լ�.
    /// </summary>
    public void ShowRewardAds()
    {
        // ���ͳ� ���� ���� üũ.
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            m_index_show = NextIndex();
            if (m_index_show != INDEX_NOT_FIND)
            {
                Time.timeScale = 0f;
                AnalyticsManager.Instance.AnalyticsEvent("Show_Ad");
                r_list_Managers[m_index_show].Show();
            }
            else //��û ������ ���� ���� ��.
            {
                Debug.LogError("���� ��û ������ ���� �����ϴ�.");
                LoadAll();
            }
        }
        else
        {
            Debug.LogError("���ͳݿ� ����Ǿ����� �ʽ��ϴ�.");
        }
    }
    //��û ������ ���� �ִ��� index�� ��ȯ�ϴ� �Լ�.
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
    /// ������ ��û �Ϸ�� ȣ��Ǵ� ��� �Լ�
    /// </summary>
    /// <param name="_result"></param>
    void OnFinishShowAds(EV_RESULT_ADS _result)
    {
        Time.timeScale = 1f;

        switch (_result)
        {
            // �������� ������ �� ���
            case EV_RESULT_ADS.Finished:
                AnalyticsManager.Instance.AnalyticsEvent("Finished_Ad");
                break;

            // ������ �߰��� �������� ��ŵ�� ���
            case EV_RESULT_ADS.Skipped:
                AnalyticsManager.Instance.AnalyticsEvent("Skipped_Ad");
                break;

            // ������ ��� ����
            case EV_RESULT_ADS.Failed:
                AnalyticsManager.Instance.AnalyticsEvent("Failed_Ad");
                break;

            case EV_RESULT_ADS.NONE:
            default:
                Debug.LogError("ERROR :: ����ġ ���� ���.");
                break;
        }

        r_list_Managers[m_index_show].Load();
    }
    #endregion
}
