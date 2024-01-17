using System.Collections.Generic;
using UnityEngine;

namespace SoulRun
{
    /// <summary>
    /// 床を手前に動かし前に進んでいるように見せるスクリプト
    /// 床は二枚あり、手前の床が見えなくなったら奥の床の後ろに移動させ、
    /// 手前に動かし続ける
    /// </summary>
    public class FieldMover : MonoBehaviour
    {
        [SerializeField] List<GameObject> _moveFloors;
        float _floorMoveSpeed = 20;
        /// <summary> 動かす床をリセットする位置 </summary>
        float _floorResetZPos = -35;
        /// <summary> 動かす床をリセットした後の位置 </summary>
        float _floorAfterResetZPos = 65;
        // Start is called before the first frame update

        private void Update()
        {
            MoveFloor();
        }

        /// <summary>
        /// すべての床を手前に動かす
        /// </summary>
        private void MoveFloor()
        {
            foreach (var floor in _moveFloors)
            {
                floor.transform.position -= new Vector3(0, 0, _floorMoveSpeed) * Time.deltaTime;
            }

            ResetFloorPos(_moveFloors[0]);
        }

        /// <summary>
        /// リストの0番目の床の位置を確認し、一定以下になったら後ろに下げる処理
        /// </summary>
        /// <param name="floor"></param>
        private void ResetFloorPos(GameObject floor)
        {
            if (floor.transform.position.z < _floorResetZPos)
            {
                floor.transform.position = new Vector3(
                    floor.transform.position.x,
                    floor.transform.position.y,
                    _floorAfterResetZPos);
                (_moveFloors[0], _moveFloors[1]) = (_moveFloors[1], _moveFloors[0]);    //Swap処理
            }
        }
    }
}
