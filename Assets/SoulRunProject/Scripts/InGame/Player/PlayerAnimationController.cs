using SoulRunProject.Common;
using UnityEngine;
using UniRx;
using Unity.Mathematics;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// PlayerのAnimationを制御する
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerRagdoll;
        [SerializeField] private float _ragdollFallSpeed = 2f;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerManager _playerManager;
        private SpriteRenderer _sr;
        private GameObject _ragdoll;
        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            Animator playerAnimator = GetComponent<Animator>();
            _playerMovement.IsGround.Subscribe(isGround =>
                {
                    playerAnimator.SetBool("IsGround", isGround);
                })
                .AddTo(this);
            _playerMovement.OnJumped += () => playerAnimator.SetTrigger("OnJump");
            _playerManager.OnDead += () =>
            {
                _ragdoll = Instantiate(_playerRagdoll, transform.position, quaternion.identity);
                _sr.enabled = false;
            };
        }

        private void Update()
        {
            //  死亡時の演出
            if (!_playerMovement || !_ragdoll) return;
            
            var threshold = _playerMovement.GroundHeight + _playerMovement.DistanceBetweenPivotAndGroundPoint;
            if (_ragdoll.transform.position.y > threshold)
            {
                _ragdoll.transform.position += Vector3.down * (_ragdollFallSpeed * Time.unscaledDeltaTime);
            }
            else if(_ragdoll.transform.position.y < threshold)
            {
                var temp = _ragdoll.transform.position;
                temp.y = threshold;
                _ragdoll.transform.position = temp;
            }
        }

        /// <summary>
        /// プレイヤーの足音再生
        /// AnimationEventから呼び出される
        /// </summary>
        public void PlayRunSound()
        {
            CriAudioManager.Instance.PlaySE("SE_Run");
        }
    }
}
