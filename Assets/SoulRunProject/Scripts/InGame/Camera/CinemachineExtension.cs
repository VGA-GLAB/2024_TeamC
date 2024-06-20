using UnityEngine;
using Cinemachine;
using SoulRunProject.InGame;

namespace SoulRunProject
{
 
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class LockCameraY : CinemachineExtension
    {
        private PlayerMovement _playerMovement;
        [Tooltip("カメラのY座標を固定する値")]
        public float m_ZPosition = 10;
        public float m_YPosition = 10;
        [SerializeField] private float _maxJumpedDistance;

        protected override void Awake()
        {
            base.Awake();
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y =  _playerMovement.GroundHeight + m_YPosition;
                
                state.RawPosition = pos;
            }
        }
    }
}

