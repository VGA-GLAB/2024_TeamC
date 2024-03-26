using UniRx.Toolkit;
using UnityEngine;
using Object = UnityEngine.Object;
using SoulRunProject.InGame;

namespace SoulRunProject.Common
{
    public class BulletPool : ObjectPool<BulletController>
    {
        readonly BulletController _bulletController;
        readonly Transform _parent;
        
        public BulletPool(Transform parent, BulletController bulletController)
        {
            _bulletController = bulletController;
            _parent = parent;
        }
        
        protected override BulletController CreateInstance()
        {
            var bullet = Object.Instantiate(_bulletController, _parent);
            return bullet;
        }
    }
}
