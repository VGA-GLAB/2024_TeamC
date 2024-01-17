using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 生成を行うクラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public abstract class AbstractFactory<T> where T : MonoBehaviour
    {
        protected GameObjectPoolForUnity _pool;
        protected GameObject _instantiateObj;
        public GameObject InstantiateObj => _instantiateObj;

        public AbstractFactory(GameObjectPoolForUnity objectPoolForUnity, GameObject instantiateObj, int capacity = 10)
        {
            _pool = objectPoolForUnity;
            _instantiateObj = instantiateObj;
            _pool.CreatePool(instantiateObj, capacity);
        }

        /// <summary>
        /// 生成を行うメソッド
        /// </summary>
        /// <returns></returns>
        public GameObject Create()
        {
            var obj = _pool.GetObject();
            OnCreateMethod(obj);
            return obj;
        }

        /// <summary>
        /// プールに返却するメソッド
        /// </summary>
        public void ReturnObject(GameObject obj)
        {
            _pool.ReturnObject(obj);
        }

        /// <summary>
        /// 生成時に独自の処理を行う必要がある場合はここに書く
        /// </summary>
        public abstract void OnCreateMethod(GameObject gameObject);

        /// <summary>
        /// 返却時に独自の処理を行う必要がある場合にはここに書く
        /// </summary>
        /// <param name="gameObject"></param>
        public abstract void OnReturnMethod(GameObject gameObject);
    }
}
