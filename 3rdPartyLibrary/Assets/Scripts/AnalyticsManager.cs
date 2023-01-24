/*
* ��ó : �������غ��� ��α� 
* ���� : Firebase Analytics ���̺귯���� ����ϴ� �Ŵ��� Ŭ���� ����
* ��ũ : https://blog.naver.com/socceri/222972194374
*/
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class AnalyticsManager : MonoSingleton<AnalyticsManager>
{
    #region �������.
    /// <summary>
    /// Member Values
    /// </summary>
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    #endregion

    #region Unity �Լ� ����
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

    #region ����Լ�
    /// <summary>
    /// Initialize �Լ�
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
    /// Initialize �Ϸ� �ݹ� �Լ�
    /// </summary>
    public void FirebaseAnalyticsInitialize()
    {
        if (dependencyStatus == DependencyStatus.Available)
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
    }

    /// <summary>
    /// Analytics Event �۽� �Լ�
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
