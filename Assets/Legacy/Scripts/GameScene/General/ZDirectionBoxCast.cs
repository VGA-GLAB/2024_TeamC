//using SimpleMan.VisualRaycast;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SoulRun
{
    /// <summary>
    /// Z軸を指定したボックスキャストクラス
    /// 前回の地点から現在の地点までBoxのRayを飛ばし、当たり判定を行う
    /// </summary>
    public class ZDirectionBoxCast : MonoBehaviour
    {
        Vector3 _prePos = Vector3.zero;
        RaycastHit[] _hits = null;
        BoxCollider _col;
        /// <summary> 現在当たっているオブジェクトの情報 </summary>
        public RaycastHit[] Hits => _hits;

        private void Start()
        {
            _prePos = transform.position;
            _col = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //当たり判定処理
            var movement = transform.position - _prePos;
            var center = _prePos + new Vector3(0, 0, movement.z / 2);   //中心(前回地点と今回地点の中間)
            var dist = movement.magnitude;  //レイの長さ
            var size = new Vector3(_col.bounds.size.x, _col.bounds.size.y, movement.z); //当たり判定のBoxのサイズ
            var dir = movement.normalized;  //レイの向き
            _hits = Physics.BoxCastAll(center, size, dir, Quaternion.identity, dist);
            //最後に現在の位置情報を以前の位置情報に
            _prePos = transform.position;
        }

        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    var movement = transform.position - _prePos;
        //    var center = _prePos + new Vector3(0, 0, movement.z / 2);
        //    var size = new Vector3(_col.bounds.size.x, _col.bounds.size.y, movement.z); //当たり判定のBoxのサイズ
        //    Gizmos.DrawWireCube(center, size);
        //}

        void OnDrawGizmos()
        {
            //　Cubeのレイを疑似的に視覚化
            Gizmos.color = Color.green;
            var movement = transform.position - _prePos;
            var center = _prePos + new Vector3(0, 0, movement.z / 2);   //中心(前回地点と今回地点の中間)
            if (_col is null) return;
            var size = new Vector3(_col.bounds.size.x, _col.bounds.size.y, movement.z); //当たり判定のBoxのサイズ
            Gizmos.DrawWireCube(center, size);
        }
    }
}
