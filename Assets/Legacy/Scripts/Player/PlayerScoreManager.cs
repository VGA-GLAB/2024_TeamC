using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーのスコアを管理するクラス
    /// </summary>
    public class PlayerScoreManager
    {
        private IntReactiveProperty _score = new(0);
        public IReadOnlyReactiveProperty<int> Score => _score;

        public PlayerScoreManager() 
        {

        }

        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="addScore"></param>
        public void AddScore(int addScore)
        {
            _score.Value += addScore;
        }
    }
}
