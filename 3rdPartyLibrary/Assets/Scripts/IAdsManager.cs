/*
 * ��ó : �������غ��� ��α� 
 * ���� : �������� ���� ����� �ϳ��� ó���ϱ� ���� �������̽� ����.
 * ��ũ : https://blog.naver.com/socceri/222972177062
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