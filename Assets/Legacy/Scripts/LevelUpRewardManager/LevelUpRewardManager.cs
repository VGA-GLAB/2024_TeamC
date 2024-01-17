using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// レベルアップ時に報酬テーブルからランダムに3つの報酬を提示する
    /// </summary>
    public class LevelUpRewardManager
    {
        List<IReward> _rewards;
        int _candidateNum = 3;  //一度に提示する報酬の数

        /// <summary>
        /// _rewardsに登録されているランダムな報酬を返す
        /// </summary>
        /// <returns></returns>
        public IReward[] GetRandomReward()
        {
            var candidates = new IReward[_candidateNum];
            for (int i = 0; i < _candidateNum; i++)
            {
                candidates[i] = _rewards[Random.Range(0, _rewards.Count)];
            }
            return candidates;
        }
    }

    /// <summary>
    /// 報酬がもつインターフェース
    /// </summary>
    public interface IReward
    {
        public void ApplyReward();
    }
}
