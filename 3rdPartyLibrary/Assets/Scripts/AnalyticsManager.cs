/*
* 출처 : 개발자준비중 블로그 
* 내용 : Firebase Analytics 라이브러리를 사용하는 매니져 클래스 구현
* 링크 : https://blog.naver.com/socceri/222972194374
*/
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class AnalyticsManager : MonoSingleton<AnalyticsManager>
{
    #region 멤버변수.
    /// <summary>
    /// Member Values
    /// </summary>
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    #endregion

    #region Unity 함수 구현
    private void Awake()
    {
        //Regist Instance for MonoSingleton<>
        base.Regist(this);
    }

    private void Start()
    {
        Init();
    }
    #endregion

    #region 멤버함수
    /// <summary>
    /// Initialize 함수
    /// </summary>
    void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseAnalyticsInitialize();
                AnalyticsEvent(FirebaseAnalytics.EventAppOpen);
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    /// <summary>
    /// Initialize 완료 콜백 함수
    /// </summary>
    public void FirebaseAnalyticsInitialize()
    {
        if (dependencyStatus == DependencyStatus.Available)
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
    }

    /// <summary>
    /// Analytics Event 송신 함수
    /// </summary>
    public void AnalyticsEvent(string _event)
    {
        if (dependencyStatus == DependencyStatus.Available)
            FirebaseAnalytics.LogEvent(_event);
    }

    public void AnalyticsEvent(string _event, params Parameter[] parameters)
    {
        if (dependencyStatus == DependencyStatus.Available)
            FirebaseAnalytics.LogEvent(_event, parameters);
    }
    #endregion
}
