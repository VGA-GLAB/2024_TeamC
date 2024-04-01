using SoulRunProject.InGame;
using UniRx.Toolkit;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// ドロップクラスのObjectPool
    /// </summary>
    public class DropPool : ObjectPool<DropBase>
    {
        readonly DropBase _drop;
        readonly Transform _parent;
        
        public DropPool(Transform parent, DropBase drop)
        {
            _drop = drop;
            _parent = parent;
        }
        
        protected override DropBase CreateInstance()
        {
            return Object.Instantiate(_drop, _parent);;
        }
    }
}
