using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using SoulRunProject.SoulRunProject.Scripts.Common.Core.Singleton;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class BulletPoolManager : AbstractSingletonMonoBehaviour<BulletPoolManager>
    {
        List<SkillBase> _skillData;
        [SerializeField] int _preloadCount = 5;
        [SerializeField] int _threshold = 5;
        [SerializeField] bool _useDontDestroyOnLoad;
        readonly Dictionary<PlayerSkill, BulletPool> _bulletPoolDictionary = new();
        protected override bool UseDontDestroyOnLoad => _useDontDestroyOnLoad;

        void Start()
        {
            MyRepository.Instance.TryGetDataList(out _skillData);
        }
        
        public BulletPool Get(PlayerSkill skillId)
        {
            //  既に指定されたIDのpoolが存在していればそのpoolを返す
            if (_bulletPoolDictionary.TryGetValue(skillId, out var value))
            {
                return value;
            }
            //  無ければ新しく生成
            var bullet = _skillData
                .OfType<ProjectileSkill>()
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
