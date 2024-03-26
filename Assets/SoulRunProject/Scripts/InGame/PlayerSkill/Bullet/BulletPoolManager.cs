using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class BulletPoolManager : AbstractSingletonMonoBehaviour<BulletPoolManager>
    {
        [SerializeField] SkillDataSet _skillDataSet;
        [SerializeField] int _preloadCount = 5;
        [SerializeField] int _threshold = 5;
        [SerializeField] bool _useDontDestroyOnLoad;
        readonly Dictionary<PlayerSkill, BulletPool> _bulletPoolDictionary = new();
        protected override bool UseDontDestroyOnLoad => _useDontDestroyOnLoad;
        
        public BulletPool Get(PlayerSkill skillId)
        {
            //  既に指定されたIDのpoolが存在していればそのpoolを返す
            if (_bulletPoolDictionary.TryGetValue(skillId, out var value))
            {
                return value;
            }
            //  無ければ新しく生成
            var bullet = _skillDataSet.Skills.OfType<ProjectileSkillBase>()
                .First(skill => skill.SkillType.Equals(skillId)).Bullet;

            var newParent = new GameObject().transform;
            newParent.name = skillId.ToString();
            newParent.SetParent(transform);
            _bulletPoolDictionary.Add(skillId, new BulletPool(newParent, bullet));
            _bulletPoolDictionary[skillId].PreloadAsync(_preloadCount, _threshold).Subscribe();

            return _bulletPoolDictionary[skillId];
        }
        public override void OnDestroyed()
        {
            foreach (var pool in _bulletPoolDictionary)
            {
                pool.Value.Dispose();
            }
        }
    }
}
