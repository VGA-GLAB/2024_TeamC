using System;

namespace SoulRunProject.Common
{
    public abstract class AbstractSingleton<T> where T : class
    {
        /// <summary> シングルトンのインスタンス </summary>
        private static readonly Lazy<T> _instance =
            new Lazy<T>(() =>
                Activator.CreateInstance(typeof(T), true) as T);

        protected AbstractSingleton()
        {
            // コンストラクタはprotectedにすることで、外部からのインスタンス化を防ぎます。
        }

        public static T Instance => _instance.Value;
    }
}