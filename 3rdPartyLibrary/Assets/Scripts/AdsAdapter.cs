/*
 * 출처 : 개발자준비중 블로그 
 * 내용 : 인터페이스를 상속받아 구현하는 기본 어댑터 클래스 구현.
 * 링크 : https://blog.naver.com/socceri/222972177062
 */
using System;

public class AdsAdapter : IAdsAdapter
{
    protected bool IsSettingComplete = false;

    //광고 보기 종료시 호출할 Delegate
    protected Action<EV_RESULT_ADS> OnFinish_Show = null;

    public virtual void Init(Action<EV_RESULT_ADS> _OnFinish_Show) { OnFinish_Show += _OnFinish_Show; }
    public virtual bool IsInit()
    {
        return IsSettingComplete;
    }
    public virtual void Load() { }
    public virtual bool IsLoaded() { return false; }
    public virtual void Show() { }
}