// using System;
// using SoulRunProject.Common;
// using SoulRunProject.SoulMixScene;
// using UnityEngine;
//
// namespace SoulRunProject.InGame
// {
//     [Serializable, Name("直進移動")]
//     public class StraightMover : EntityMover
//     {
//         public void GetMoveStatus(PlayerStatus playerStatus)
//         {
//             _moveSpeed = playerStatus.SpeedUpAtLevelUp;
//         }
//
//         public override void OnUpdateMove(Transform myTransform, Transform playerTransform)
//         {
//             myTransform.position += Vector3.back * (_moveSpeed * Time.deltaTime);
//         }
//     }
// }