/*
 * 출처 : 개발자준비중 블로그 
 * 내용 : 여러개의 광고 모듈을 하나로 처리하기 위한 인터페이스 구현.
 * 링크 : https://blog.naver.com/socceri/222972177062
 */

using System;

public interface IAdsAdapter
{
    public abstract void Init(Action<EV_RESULT_ADS> _OnFinish_Show);
    public abstract bool IsInit();
    public abstract void Load();
    public abstract bool IsLoaded();
    public abstract void Show();
}