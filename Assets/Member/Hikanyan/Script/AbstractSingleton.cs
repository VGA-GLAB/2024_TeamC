using UnityEngine;
namespace SoulRun.Core
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
                    if (_instance == null)
                    {
                        Debug.LogError("Instance of " + typeof(T).ToString() + " is not set. Make sure it's attached to a GameObject and initialized.");
                    }
                    return _instance;
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                GameObject singletonObject = gameObject;
                singletonObject.name = typeof(T).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            else if (_instance != this)
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