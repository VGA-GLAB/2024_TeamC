using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// フィールドののアクティブを管理するクラス
    /// </summary>
    public class FieldActiveManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _fieldObjects;
        [SerializeField] private PlayerManager _playerManager;
        
        private void Start()
        {
            foreach (var fieldObject in _fieldObjects)
            {
                fieldObject.SetActive(false);
            }
        }
    }
}
