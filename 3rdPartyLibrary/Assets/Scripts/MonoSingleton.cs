/*
 * ��ó : �������غ��� ��α� C# Unity �̱������� Singleton Pattern ����
 * ��ũ : https://blog.naver.com/socceri/222698718905
 */


using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T r_instance = null;
    public static T Instance
    {
        get
        {//instance �� null �ΰ�� ���� �� �ʱ�ȭ ó�� ����.
            if (r_instance == null)
            {
                //component �� �߰��� ���� ������Ʈ ���� �� Dontdestroy ó��
                GameObject _object = new GameObject();
                GameObject.DontDestroyOnLoad(_object);

                //component �߰��� �� ���̹�ó��
                r_instance = _object.AddComponent<T>();
                _object.name = r_instance.GetType().ToString();
            }
            return r_instance;
        }
    }
    protected void Regist(T _instance)
    {
        if (r_instance == null)
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            r_instance = _instance;
            this.gameObject.name = r_instance.GetType().ToString();
            //Debug.Log("Regist Instance Finish");
        }
    }
}
//How To Use ::
// �ڽ� ��ü�� Awake �Լ� ���� �� Regist �Լ� ȣ�� / Instance ��ü ���
//void Awake()
//{
//    base.Regist(this);
//    Debug.Log("Regist Instance Finish");
//}
