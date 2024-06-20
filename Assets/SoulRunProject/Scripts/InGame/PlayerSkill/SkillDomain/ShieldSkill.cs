using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoulRunProject.Common;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class ShieldSkill : AbstractSkill
    {
        /// <summary>ダメージを受けた回数</summary>
        private int _damageCount;

        private Transform _instantiatedObject;
        private CancellationTokenSource _linkedTokenSource;
        private Tweener _tweener;

        public ShieldSkill(AbstractSkillData skillData, in PlayerManager playerManager, in Transform playerTransform)
            : base(skillData, in playerManager, in playerTransform)
        {
        }

        private ShieldSkillData SkillData => (ShieldSkillData)_skillData;
        private ShieldSkillParameter RuntimeParameter => (ShieldSkillParameter)_runtimeParameter;

        public override void StartSkill()
        {
            _instantiatedObject = Object.Instantiate(SkillData.Original).transform;
            _instantiatedObject.localScale = Vector3.zero;
            _playerManagerInstance.IgnoreDamagePredicates.Add(IncreaseDamageCount);
            ActiveSkill();
            _instantiatedObject.position = _playerTransform.position;
        }

        public override void UpdateSkill(float deltaTime)
        {
            _instantiatedObject.position = _playerTransform.position;
        }

        public override void OnLevelUp()
        {
            if (_damageCount < RuntimeParameter.ShieldCount)
            {
                _linkedTokenSource?.Cancel();
                _tweener?.Kill();
                _tweener = _instantiatedObject.DOScale(Vector3.one, 0.25f).SetLink(_instantiatedObject.gameObject);
            }
        }

        /// <summary>シールド展開</summary>
        private void ActiveSkill()
        {
            CriAudioManager.Instance.PlaySE("SE_Soulshell");
            _damageCount = 0;
            _tweener?.Kill();
            _tweener = _instantiatedObject.DOScale(Vector3.one, 0.25f).SetLink(_instantiatedObject.gameObject);
        }

        /// <summary>
        ///     被ダメージ回数をカウントアップする。
        ///     <br />カウントアップ時にシールド枚数以上になればシールドの演出を解除してクールタイムに入る。
        /// </summary>
        /// <returns>メソッド開始時に被ダメージ回数がシールド枚数以上にならfalse, でなければtrueを返す。</returns>
        private bool IncreaseDamageCount()
        {
            //  ダメージ回数がシールド枚数以上ならreturn false
            if (_damageCount >= RuntimeParameter.ShieldCount) return false;
            //  ダメージ回数を増やす
            _damageCount++;
            //  再度チェックしてシールド枚数以上ならクールダウンを開始してreturn true
            if (_damageCount >= RuntimeParameter.ShieldCount)
            {
                CriAudioManager.Instance.PlaySE("SE_DiableSoulShell");
                _linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                    _instantiatedObject.GetCancellationTokenOnDestroy());
                CoolDown(_linkedTokenSource.Token).Forget();
            }

            return true;
        }

        private async UniTaskVoid CoolDown(CancellationToken ct)
        {
            try
            {
                var timer = 0f;
                //  演出
                _tweener?.Kill();
                _tweener = _instantiatedObject.DOScale(Vector3.zero, 0.25f).SetLink(_instantiatedObject.gameObject);
                await UniTask.WaitForSeconds(RuntimeParameter.CoolTime, cancellationToken: ct);
                ActiveSkill();
            }
            catch
            {
                Debug.Log("キャンセルされた");
            }
        }
    }
}