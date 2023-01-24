/*
 * ��ó : �������غ��� ��α� 
 * ���� : �������̽��� ��ӹ޾� �����ϴ� �⺻ ����� Ŭ���� ����.
 * ��ũ : https://blog.naver.com/socceri/222972177062
 */
using System;

public class AdsAdapter : IAdsAdapter
{
    protected bool IsSettingComplete = false;

    //���� ���� ����� ȣ���� Delegate
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