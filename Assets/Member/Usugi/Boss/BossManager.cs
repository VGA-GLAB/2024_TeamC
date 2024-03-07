using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ボスの動きを管理するクラス
    /// </summary>
    public class BossManager : FieldEntityController
    {
        [SerializeField] BehaviorRunner _behaviorRunner;
        
        public void ActiveBoss()
        {
            _behaviorRunner.RunTree();
        }
    }
}
