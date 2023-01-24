/*
* 출처 : 개발자준비중 블로그 
* 내용 : GooglePlayGameService 라이브러리를 사용하는 매니져 클래스 구현
* 링크 : https://blog.naver.com/socceri/222972190437
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
    #region 멤버변수.
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

    #region Unity 함수 구현
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

    #region 멤버함수
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
    /// Google Play Game Service 로그인 처리 진행
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
    /// GooglePlay 로그인 콜백 함수
    /// </summary>
    /// <param name = "status" > 로그인 결과 반환</param>
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
        //Login 결과에 상관없이 씬 이동. -> 추후 LeaderBoard 버튼 선택시 재 로그인 시도.
        SceneManager.LoadScene("PlayGame");
    }

    /// <summary>
    /// 리더보드 보기
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
    /// 리더보드 갱신. 데이터 송신.
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
            Debug.LogError("올바른 리더보드 정의값을 입력해주세요 !");
        }
    }
    /// <summary>
    /// leader board ID 반환함수
    /// </summary>
    /// <param name="_ev_leaderboard"></param>
    /// <returns></returns>
    static string GetLeaderBoardID(EV_Leaderboard _ev_leaderboard)
    {
        string _leaderboard_id = string.Empty;

        //to do :: leader board ID 반환
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
