using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.Skill
{
    /// <summary>
    ///     ダメージ無効化スキル
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ShieldSkill")]
    public class ShieldSkill : SkillBase
    {
        static Transform _playerTransform;
        [SerializeField, Tooltip("プレハブ")] GameObject _original;
        PlayerManager _playerManager;
        Transform _instantiatedObject;
        CancellationTokenSource _linkedTokenSource;
        Tweener _tweener;
        ShieldSkillParameter _shieldSkillParameter;
        /// <summary>ダメージを受けた回数</summary>
        int _damageCount;

        ShieldSkill()
        {
            _skillParam = new ShieldSkillParameter();
            SkillLevelUpEvent = new SkillLevelUpEvent(new ShieldSkillLevelUpEventListList());
        }
        
        public override void StartSkill()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            _playerTransform = _playerManager.transform;
            _instantiatedObject = Instantiate(_original).transform;
            _instantiatedObject.localScale = Vector3.zero;
            if (_skillParam is ShieldSkillParameter param)
                _shieldSkillParameter = param;
            else
                Debug.LogError($"パラメータが　{nameof(ShieldSkillParameter)}　ではありません　");
            _playerManager.IgnoreDamagePredicates.Add(IncreaseDamageCount);
            ActiveSkill();
            _instantiatedObject.position = _playerTransform.position;
        }

        public override void UpdateSkill(float deltaTime)
        {
            _instantiatedObject.position = _playerTransform.position;
        }

        public override void OnLevelUp()
        {
            base.OnLevelUp();
            if (_damageCount < _shieldSkillParameter.ShieldCount)
            {
                _linkedTokenSource?.Cancel();
                _tweener?.Kill();
                _tweener = _instantiatedObject.DOScale(Vector3.one, 0.25f).SetLink(_instantiatedObject.gameObject);
            }
        }

        protected override void OnSwitchPause(bool toPause)
        {
            if (toPause)
            {
                _tweener?.Pause();
            }
            else
            {
                _tweener?.Play();
            }
        }

        /// <summary>シールド展開</summary>
        void ActiveSkill()
        {
            _damageCount = 0;
            _tweener?.Kill();
            _tweener = _instantiatedObject.DOScale(Vector3.one, 0.25f).SetLink(_instantiatedObject.gameObject);
        }

        /// <summary>
        ///     被ダメージ回数をカウントアップする。
        ///     <br />カウントアップ時にシールド枚数以上になればシールドの演出を解除してクールタイムに入る。
        /// </summary>
        /// <returns>メソッド開始時に被ダメージ回数がシールド枚数以上にならfalse, でなければtrueを返す。</returns>
        bool IncreaseDamageCount()
        {
            //  ダメージ回数がシールド枚数以上ならreturn false
            if (_damageCount >= _shieldSkillParameter.ShieldCount) return false;
            //  ダメージ回数を増やす
            _damageCount++;
            //  再度チェックしてシールド枚数以上ならクールダウンを開始してreturn true
            if (_damageCount >= _shieldSkillParameter.ShieldCount)
            {
                _linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                    _instantiatedObject.GetCancellationTokenOnDestroy());
                CoolDown(_linkedTokenSource.Token).Forget();
            }

            return true;
        }

        async UniTaskVoid CoolDown(CancellationToken ct)
        {
            var timer = 0f;
            //  演出
            _tweener?.Kill();
            _tweener = _instantiatedObject.DOScale(Vector3.zero, 0.25f).SetLink(_instantiatedObject.gameObject);
            //  タイマーループ開始
            while (true)
                try
                {
                    //  Pauseフラグがfalseなら1フレーム待つ(ちゃんと1フレーム待ってるか分からない)、
                    //  trueならfalseになるまで通さない
                    await UniTask.WaitUntil(() => !_isPause, PlayerLoopTiming.Update, ct);
                    timer += Time.deltaTime;
                    if (timer >= _shieldSkillParameter.CoolTime)
                    {
                        ActiveSkill();
                        break;
                    }
                }
                catch
                {
                    Debug.Log("キャンセルされた");
                    break;
                }
        }
    }
}