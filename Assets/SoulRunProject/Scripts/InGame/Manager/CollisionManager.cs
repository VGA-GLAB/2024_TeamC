using System.Collections.Generic;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 衝突管理クラス
    /// </summary>
    public class CollisionManager : AbstractSingletonMonoBehaviour<CollisionManager>
    {
        [SerializeField] bool _useDontDestroyOnLoad;
        List<ColliderBase> _colliders = new();
        public List<ColliderBase> Colliders => _colliders;

        void Update()
        {
            for (int i = 0; i < _colliders.Count; i++)
            {
                for (int j = 0; j < _colliders.Count; j++)
                {
                    if (i == j) continue;
                    if (_colliders[i].CheckContacts(_colliders[j]))
                    {
                        if (!_colliders[i].Contacts.Add(_colliders[j])) continue;
                        _colliders[i].Enter.OnNext(_colliders[j]);
                    }
                    else if(_colliders[i].Contacts.Contains(_colliders[j]))
                    {
                        _colliders[i].Contacts.Remove(_colliders[j]);
                        _colliders[i].Exit.OnNext(_colliders[j]);
                    }
                }
            }
        }

        protected override bool UseDontDestroyOnLoad => _useDontDestroyOnLoad;
    }
}
