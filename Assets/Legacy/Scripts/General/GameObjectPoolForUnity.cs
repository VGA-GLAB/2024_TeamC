using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// オブジェクトをプーリングする為のクラス
    /// </summary>
    public class GameObjectPoolForUnity : MonoBehaviour
    {
        /// <summary> 生成するオブジェクトの参照 </summary>
        protected GameObject _createObject;
        /// <summary> プーリングしているオブジェクトの最大値 </summary>
        protected int _capacity = 1;
        /// <summary> 生成したオブジェクトの参照をプールするキュー </summary>
        protected Queue<GameObject> _pool;

        /// <summary>
        /// プールを生成する
        /// </summary>
        /// <param name="createObject">プールして生成するオブジェクト</param>
        /// <param name="capacity">プールする数</param>
        public void CreatePool(GameObject createObject, int capacity)
        {
            _createObject = createObject;
            _capacity = capacity;
            _pool = new Queue<GameObject>(_capacity);

            for (int i = 0; i < _capacity; i++)
            {
                _pool.Enqueue(CreateObject(createObject));
            }
        }

        private GameObject CreateObject(GameObject createObject)
        {
            var obj = Instantiate(createObject, transform);
            obj.SetActive(false);
            return obj;
        }

        /// <summary>
        /// プールのインスタンスを破棄する
        /// </summary>
        public void DestroyPool()
        {
            foreach (var obj in _pool)
            {
                Destroy(obj);
            }
            _pool.Clear();
            _capacity = 0;
        }

        /// <summary>
        /// プールされているオブジェクトを取ってくる
        /// </summary>
        /// <returns></returns>
        public GameObject GetObject() 
        {
            if (_pool.Count == 0)
            {   //プールに取れるオブジェクトがなかったら生成
                _capacity++;
                _pool = new Queue<GameObject>(_capacity);
                _pool.Enqueue(CreateObject(_createObject));
            }
            var obj = _pool.Dequeue();
            obj.SetActive(true);
            obj.transform.parent = null;
            return obj;
        }

        /// <summary>
        /// プールから取ってきていたオブジェクトを返却する
        /// </summary>
        /// <param name="returnObj"></param>
        public void ReturnObject(GameObject returnObj)
        {
            returnObj.SetActive(false);

            if (_pool.Count >= _capacity) return;
            returnObj.transform.parent = this.transform;
            _pool.Enqueue(returnObj);
        }
    }
}
