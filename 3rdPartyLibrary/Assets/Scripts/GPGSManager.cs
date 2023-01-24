/*
* ��ó : �������غ��� ��α� 
* ���� : GooglePlayGameService ���̺귯���� ����ϴ� �Ŵ��� Ŭ���� ����
* ��ũ : https://blog.naver.com/socceri/222972190437
*/
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using UnityEngine.SceneManagement;

public enum EV_Leaderboard
{
    NONE = -1,
}

public class GPGSManager : MonoSingleton<GPGSManager>
{
    #region �������.
    /// <summary>
    /// Member Values
    /// </summary>
    static bool isSettingComplete = false;
    public static bool IsSettingComplete
    {
        get { return isSettingComplete; }
    }

    public static string UserID
    {
        get
        {
            if (IsSettingComplete)
                return PlayGamesPlatform.Instance.GetUserId();
            else
                return string.Empty;
        }
    }
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

    IEnumerator Start()
    {
        Initialize();
        yield return new WaitForEndOfFrame();
        Login();
    }
    #endregion

    #region ����Լ�
    /// <summary>
    /// functions - Member
    /// </summary>
    void Initialize()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
#else
        isSettingComplete = true;
#endif
    }

    /// <summary>
    /// Google Play Game Service �α��� ó�� ����
    /// </summary>
    public void Login()
    {
        try
        {
            if (!PlayGamesPlatform.Instance.IsAuthenticated())
                PlayGamesPlatform.Instance.Authenticate(OnFinish_Authentication);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// GooglePlay �α��� �ݹ� �Լ�
    /// </summary>
    /// <param name = "status" > �α��� ��� ��ȯ</param>
    internal void OnFinish_Authentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            isSettingComplete = true;
        }
        //cancel or error
        else
        {

        }
        //Login ����� ������� �� �̵�. -> ���� LeaderBoard ��ư ���ý� �� �α��� �õ�.
        SceneManager.LoadScene("PlayGame");
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    public void ShowLeaderboard()
    {
        if (IsSettingComplete)
            Social.ShowLeaderboardUI();
        else
            PlayGamesPlatform.Instance.Authenticate(OnFinish_Auth_When_Leaderboard);
    }

    internal void OnFinish_Auth_When_Leaderboard(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            isSettingComplete = true;
            Social.ShowLeaderboardUI();
        }
        //cancel or error
        else
        {

        }
    }

    /// <summary>
    /// �������� ����. ������ �۽�.
    /// </summary>
    /// <param name="_ev_leaderboard"></param>
    /// <param name="_amount"></param>
    public void Send_Leaderboard(EV_Leaderboard _ev_leaderboard, int _amount)
    {
        string _leaderboard_id = GetLeaderBoardID(_ev_leaderboard);

        if (!string.IsNullOrEmpty(_leaderboard_id) && _ev_leaderboard != EV_Leaderboard.NONE)
        {
            Social.ReportScore((long)_amount, _leaderboard_id, (bool success) =>
            {
                if (success)
                    Debug.Log("Post Success");
                else
                    Debug.Log("Post Fail");
            });
        }
        else
        {
            Debug.LogError("�ùٸ� �������� ���ǰ��� �Է����ּ��� !");
        }
    }
    /// <summary>
    /// leader board ID ��ȯ�Լ�
    /// </summary>
    /// <param name="_ev_leaderboard"></param>
    /// <returns></returns>
    static string GetLeaderBoardID(EV_Leaderboard _ev_leaderboard)
    {
        string _leaderboard_id = string.Empty;

        //to do :: leader board ID ��ȯ
        switch (_ev_leaderboard)
        {
            case EV_Leaderboard.NONE:
            default:
                break;
        }

        return _leaderboard_id;
    }
    #endregion
}
