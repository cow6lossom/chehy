using UnityEngine;

namespace Tyu.Scripts
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError($"There no Instance of {typeof(T)}");
                    return null;
                }
                return _instance;
            }
        }
        protected bool MakeSingleton() //Call it Awake's first line, copy it "if(!MakeSingleton()) return;"
                                       //이거 그거임 싱글톤 못 만들었으면 다음 초기화 코드 실행하지 말라고 하는거
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return false;
            }
            if (_instance == null)
                _instance = GetComponent<T>();

            return _instance != null;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}