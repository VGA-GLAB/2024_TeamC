using System;
using SoulRunProject.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SoulRunProject
{
    /// <summary>
    /// 表現を加える際にはさらにこれを継承する
    /// </summary>
    [Serializable]
    public class ProjectileSkillBase : SkillBase
    {
        [SerializeField , Header("発射する弾のプレハブ")] private BulletController _bullet;
        private Transform _playerTransform;

        public override void Fire()
        {
            //プレイヤーのポジションから発射させたい
            _playerTransform ??= Object.FindObjectOfType<PlayerManager>().transform;
            if (SkillBaseParam is ProjectileSkillParameter param)
            {
                // 弾のInstantiate
                var bullet = Object.Instantiate(_bullet , _playerTransform.position , Quaternion.identity);
                bullet.Initialize(param.LifeTime , param.AttackDamage , param.Range , param.Speed , param.Penetration);
            }
            else
            {
                Debug.LogError("パラメータが　ProjectionSkillParameter　ではありません　");
            }
        }
    }
}
