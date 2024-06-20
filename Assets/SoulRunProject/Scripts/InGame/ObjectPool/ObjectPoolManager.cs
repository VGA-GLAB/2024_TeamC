using System.Collections.Generic;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class ObjectPoolManager : AbstractSingletonMonoBehaviour<ObjectPoolManager>
    {
        [SerializeField] int _preloadCount = 5;
        [SerializeField] int _threshold = 5;
        readonly Dictionary<PooledObject, CommonObjectPool> _poolDictionary = new();

        public PooledObject RequestInstance(PooledObject original, Transform parent)
        {
            var objectPool = RequestPool(original);
            var pooledObject = objectPool.Rent();
            var objectTransform = pooledObject.transform;
            var prevParent = objectTransform.parent;
            
            objectTransform.SetParent(parent);
            pooledObject.Initialize();
            pooledObject.OnFinishedAsync.Take(1).Subscribe(_=>
            {
                pooledObject.transform.SetParent(prevParent);
                objectPool.Return(pooledObject);
            });
            return pooledObject;
        }
        public PooledObject RequestInstance(PooledObject original, Vector3 position, Quaternion rotation)
        {
            var objectPool = RequestPool(original);
            var pooledObject = objectPool.Rent();
            var objectTransform = pooledObject.transform;
            
            objectTransform.position = position;
            objectTransform.rotation = rotation;
            pooledObject.Initialize();
            pooledObject.OnFinishedAsync.Take(1).Subscribe(_=> objectPool.Return(pooledObject));
            return pooledObject;
        }
        public PooledObject RequestInstance(PooledObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            var objectPool = RequestPool(original);
            var pooledObject = objectPool.Rent();
            var objectTransform = pooledObject.transform;
            var prevParent = objectTransform.parent;
            
            objectTransform.position = position;
            objectTransform.rotation = rotation;
            objectTransform.SetParent(parent);
            pooledObject.Initialize();
            pooledObject.OnFinishedAsync.Take(1).Subscribe(_=>
            {
                pooledObject.transform.SetParent(prevParent);
                objectPool.Return(pooledObject);
            });
            return pooledObject;
        }
        public CommonObjectPool RequestPool(PooledObject original)
        {
            //  既に指定されたIDのpoolが存在していればそのpoolを返す
            if (_poolDictionary.TryGetValue(original, out var value))
            {
                return value;
            }
            //  無ければ新しく生成
            var newParent = new GameObject().transform;
            newParent.name = original.name + " group";
            newParent.SetParent(transform);
            _poolDictionary.Add(original, new CommonObjectPool(newParent, original));
            _poolDictionary[original].PreloadAsync(_preloadCount, _threshold).Subscribe();

            return _poolDictionary[original];
        }

        protected override bool UseDontDestroyOnLoad => false;

        public override void OnDestroyed()
        {
            foreach (var pool in _poolDictionary)
            {
                pool.Value.Dispose();
            }
        }
    }
}
