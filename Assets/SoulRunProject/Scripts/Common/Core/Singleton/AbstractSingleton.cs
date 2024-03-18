using System;

namespace SoulRunProject.Common
{
    public abstract class AbstractSingleton<T> : ISingleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new T();
                (_instance as AbstractSingleton<T>)?.OnAwake();
                return _instance;
            }
        }

        public virtual void OnAwake()
        {
        }

        public virtual void OnDestroyed()
        {
        }
    }
}