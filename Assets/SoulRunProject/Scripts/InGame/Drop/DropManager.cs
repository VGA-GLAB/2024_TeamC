using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵が死んだときにドロップするアイテムを管理するクラス
    /// </summary>
    public class DropManager : AbstractSingletonMonoBehaviour<DropManager>
    {
        [SerializeField, CustomLabel("コインプレハブ")] private InGameCoin _coinPrefab;
        [SerializeField, CustomLabel("経験値プレハブ")] private InGameExp _expPrefab;
        private FieldMover _fieldMover;
        private PlayerManager _playerManager;
        public override void OnAwake()
        {
            _fieldMover = FindObjectOfType<FieldMover>();
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        public void RequestDrop(LootTable lootTable, Vector3 position, PlayerStatus playerPlayerStatus = null)
        {
            ScoreManager.Instance.AddScore(lootTable.Score);
            MakeDropObjects(_coinPrefab , position , lootTable.Coin);
            MakeDropObjects(_expPrefab , position , lootTable.Exp);
            
            if (Random.Range(0 , 100) < lootTable.SoulCardDropRate)
            {
                if (SoulCard.TryCreateCard(lootTable.SoulCardID ,Random.Range(0 ,  lootTable.SoulCardExperienceAmount + 1) , out var soulCard))
                {
                    //Debug.Log($"ソウルカード獲得 {soulCard.SoulName} {soulCard.CurrentExperience}");
                    //TODO ソウルカードをインベントリーに登録
                }
            }
            
        }

        /// <summary>
        /// ドロップさせるオブジェクトをプールから呼び出す
        /// </summary>
        private void MakeDropObjects(PooledObject prefab , Vector3 position , int count)
        {
            var pool = ObjectPoolManager.Instance.RequestPool(prefab);
                
            for (int i = 0; i < count; i++)
            {
                var drop = (DropBase)pool.Rent();
                drop.transform.position = position;
                drop.OnFinishedAsync.Take(1).Subscribe(_ => pool.Return(drop));
                drop.ApplyReference(_playerManager, _fieldMover);
                drop.Initialize();
                if (_fieldMover.MoveSegments.Count > 0)
                {
                    var parent = _fieldMover.MoveSegments[^1].transform;
                    //  親オブジェクトにMyConstraintが付いてなければ新しく追加する
                    if (!parent.TryGetComponent(out MyConstraint constraint))
                    {
                        constraint = parent.AddComponent<MyConstraint>();
                    }
                    //  疑似的にフィールドの子オブジェクトにする(フィールドが破棄されても残る。座標は連動して動く。)
                    constraint.Targets.Add(drop.transform);
                    //  親オブジェクトが破棄されたら、ドロップアイテムをプールに戻す
                    constraint.gameObject.OnDestroyAsObservable()
                        .Subscribe(_ => drop.Finish()).AddTo(constraint.gameObject);
                }
            }
        }
        
        protected override bool UseDontDestroyOnLoad { get; } = false;
    }
}
