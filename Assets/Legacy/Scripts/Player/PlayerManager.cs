using UnityEngine;
using UniRx;
using VContainer;
using UniRx.Triggers;
using System;
using VContainer.Unity;
using Cysharp.Threading.Tasks;

namespace SoulRun.InGame
{
    /// <summary>
    /// Player周りのscriptを管理するマネージャクラス
    /// 他クラスからPlayer周りのscriptにアクセスする際はここを通す
    /// </summary>
    public class PlayerManager : MonoBehaviour, IDamage, IPausable
    {
        /// <summary> Playerの体力 </summary>
        private HitPoint _hitPoint = new HitPoint(ObjectDamageType.Player, 10);
        private Wallet _wallet;


        /// <summary> Playerオブジェクトを動かす </summary>
        [Inject] private PlayerMover _playerMover;
        /// <summary> Playerのスキルを管理する </summary>
        [Inject] private PlayerSkillManager _skillManager;
        public HitPoint HitPoint => _hitPoint;
        public PlayerMover PlayerMover => _playerMover;
        public PlayerSkillManager SkillManager => _skillManager;
        IDisposable _manualPlayerVerticalMoverUpdates = null;
        IDisposable _manualPlayerHorizontalMoveUpdates = null;

        /// <summary>
        /// プレイヤーの全機能を稼働させる
        /// </summary>
        public void Active()
        {
            _skillManager.ActivateAllSkills();
            _manualPlayerVerticalMoverUpdates = this.UpdateAsObservable().Subscribe(_ => _playerMover.MoveForward(Time.deltaTime)).AddTo(this);
            _manualPlayerHorizontalMoveUpdates = this.UpdateAsObservable().Subscribe(_ => _playerMover.ManualUpdate(Time.deltaTime)).AddTo(this);
        }

        /// <summary>
        /// プレイヤーの全機能を停止させる
        /// </summary>
        public void Pause()
        {
            _skillManager.DeactiveAllSkills();
            _manualPlayerVerticalMoverUpdates?.Dispose();
            _manualPlayerHorizontalMoveUpdates?.Dispose();
        }

        /// <summary>
        /// 前進するのを止める
        /// </summary>
        public void StopForwardMove()
        {
            _manualPlayerVerticalMoverUpdates?.Dispose();
        }
    }
}
