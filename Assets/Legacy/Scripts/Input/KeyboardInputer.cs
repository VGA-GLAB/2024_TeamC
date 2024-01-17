using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// キーボードからの入力を受け取るクラス
    /// </summary>
    public class KeyboardInputer : MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private AlphaCameraMover _cameraMover;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {   //ジャンプ
                _playerMover.DoJump().Forget();
            }

            if (Input.GetKey(KeyCode.D))
            {   //右へ移動
                _playerMover.SetHorizontalInput(1);
                _cameraMover.RotateCam(true);
            }
            else if (Input.GetKey(KeyCode.A))
            {   //左へ移動
                _playerMover.SetHorizontalInput(-1);
                _cameraMover.RotateCam(false);
            }
            else
            {   //動かない
                _playerMover.SetHorizontalInput(0);
                _cameraMover.ResetCamRotate();
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {   //必殺技撃つ処理

            }
        }
    }
}
