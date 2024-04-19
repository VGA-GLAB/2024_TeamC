using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// ボスの行動を管理するクラス
    /// 行動様式
    /// 最初に入場ムーブ
    /// 一行動を起こすたびに次の行動からランダムで実行する、
    /// </summary>
    public class BossController : MonoBehaviour
    {
        [SerializeField, Header("スコア")]
        private int _score = 100;
        
        [SerializeField, Tooltip("敵のパラメータを設定する")]
        protected Status _status;
        
        [SerializeField] LootTable _lootTable;
        [Header("ボスの行動"), SerializeReference, SubclassSelector] List<IBossBahavior> _bossBahaviors;

        private void Start()
        {
            _status = _status.Copy();
        }
        
        private void Update()
        {
            
        }
    }
    
    /// <summary>
    /// ボスの行動が持つインターフェース
    /// </summary>
    public interface IBossBahavior
    {
        void DoAction();
    }
}
