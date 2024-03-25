using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class FieldActiveManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _fieldObjects;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private float _activationDistance = 50.0f; // アクティブ化する距離

        private void Start()
        {
            foreach (var fieldObject in _fieldObjects)
            {
                fieldObject.SetActive(false);
            }
        }

        private void Update()
        {
            // プレイヤーのZポジションを取得
            float playerZPosition = _playerManager.transform.position.z;

            foreach (var fieldObject in _fieldObjects)
            {
                // プレイヤーからのオブジェクトの距離を計算
                float distanceToPlayer = Mathf.Abs(fieldObject.transform.position.z - playerZPosition);

                // 距離が_activationDistance以内の場合、オブジェクトをアクティブにする
                if (distanceToPlayer <= _activationDistance)
                {
                    fieldObject.SetActive(true);
                }
                else
                {
                    // それ以外の場合は非アクティブにする
                    fieldObject.SetActive(false);
                }
            }
        }
    }
}