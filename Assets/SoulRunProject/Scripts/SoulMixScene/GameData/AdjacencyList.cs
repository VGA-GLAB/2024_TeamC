using System.Collections;
using System.Collections.Generic;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> 隣接リストを保持するクラス </summary>
    public class AdjacencyList : MonoBehaviour
    {
        // 隣接リストを保持する辞書
        private Dictionary<SoulCardData, List<SoulCardData>> _adjacencyList;

        // 追加するためのSoulCardList
        [SerializeField] private List<SoulCardData> soulCardList;
        
        
        void Start()
        {
            _adjacencyList = new Dictionary<SoulCardData, List<SoulCardData>>();

            // ここで隣接リストを初期化
            AddCombination(soulCardList[0], soulCardList[1]);
        }

        // 2つのSoulCard間の組み合わせを追加するメソッド
        public void AddCombination(SoulCardData card1, SoulCardData card2)
        {
            if (!_adjacencyList.ContainsKey(card1))// もし辞書にキーが存在しない場合は、新しいリストを作成
            {
                _adjacencyList[card1] = new List<SoulCardData>();
            }

            _adjacencyList[card1].Add(card2);

            // もし無向グラフ（双方向の関係）の場合は、逆の組み合わせも追加
            if (!_adjacencyList.ContainsKey(card2))
            {
                _adjacencyList[card2] = new List<SoulCardData>();
            }

            _adjacencyList[card2].Add(card1);
        }

        // 2つのSoulCardが合成可能か判定するメソッド
        public bool CanCombine(SoulCardData card1, SoulCardData card2)
        {
            if (!_adjacencyList.ContainsKey(card1))
            {
                return false;
            }

            return _adjacencyList[card1].Contains(card2);
        }
    }
}