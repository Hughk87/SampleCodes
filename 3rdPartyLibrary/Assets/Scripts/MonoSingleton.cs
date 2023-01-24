/*
 * 출처 : 개발자준비중 블로그 C# Unity 싱글톤패턴 Singleton Pattern 구현
 * 링크 : https://blog.naver.com/socceri/222698718905
 */


using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T r_instance = null;
    public static T Instance
    {
        get
        {//instance 가 null 인경우 생성 및 초기화 처리 진행.
            if (r_instance == null)
            {
                //component 를 추가할 게임 오브젝트 생성 및 Dontdestroy 처리
                GameObject _object = new GameObject();
                GameObject.DontDestroyOnLoad(_object);

                //component 추가한 후 네이밍처리
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
// 자식 객체의 Awake 함수 구현 및 Regist 함수 호출 / Instance 객체 등록
//void Awake()
//{
//    base.Regist(this);
//    Debug.Log("Regist Instance Finish");
//}
