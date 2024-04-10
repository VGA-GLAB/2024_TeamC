using UnityEngine;
using Cinemachine;
namespace SoulRunProject
{
 
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class LockCameraY : CinemachineExtension
    {
        [Tooltip("カメラのY座標を固定する値")]
        public float m_ZPosition = 10;
        public float m_YPosition = 10;
 
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.y = m_YPosition;
                //pos.z = m_ZPosition;
                state.RawPosition = pos;
            }
        }
    }
}

