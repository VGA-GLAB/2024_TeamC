using UnityEngine;
namespace SoulRunProject.Common
{
    /// <summary>
    /// 継承してSingleton使用します。
    /// 継承先でAwakeが必要な場合OnAwake()を呼んでください。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            OnAwake();
        }
        /// <summary>
        /// 継承先でAwakeが必要な場合
        /// </summary>
        protected virtual void OnAwake() { }
    }
}