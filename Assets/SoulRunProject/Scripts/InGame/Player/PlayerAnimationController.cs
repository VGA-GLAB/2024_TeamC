using UnityEngine;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// PlayerのAnimationを制御する
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private void Awake()
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            Animator playerAnimator = GetComponent<Animator>();

            playerMovement.OnIsGroundChanged.Subscribe(isGround =>
            {
                playerAnimator.SetBool("IsGround", isGround);
            });
            playerMovement.OnJumped += () => playerAnimator.SetTrigger("OnJump");
        }
    }
}
