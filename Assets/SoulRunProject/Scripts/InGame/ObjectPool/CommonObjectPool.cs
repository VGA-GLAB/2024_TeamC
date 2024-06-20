using UniRx.Toolkit;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// UniRxのObjectPoolクラスを継承したオブジェクトプール
    /// </summary>
    public class CommonObjectPool : ObjectPool<PooledObject>
    {
        private Transform _parent;
        private readonly PooledObject _pooledObject;

        public CommonObjectPool(Transform parent, PooledObject pooledObject)
        {
            _parent = parent;
            _pooledObject = pooledObject;
        }
        
        protected override PooledObject CreateInstance()
        {
            var pooledObject = Object.Instantiate(_pooledObject, _parent);
            pooledObject.IsPooled = true;
            return pooledObject;
        }
    }
}