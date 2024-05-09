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
    public class ItemDropManager : AbstractSingletonMonoBehaviour<ItemDropManager>
    {
        private FieldMover _fieldMover;
        private PlayerManager _playerManager;
        public override void OnAwake()
        {
            _fieldMover = FindObjectOfType<FieldMover>();
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        public void RequestDrop(LootTable lootTable, Vector3 pos, Status playerStatus = null)
        {
            foreach (var dropData in lootTable.Choose(playerStatus))
            {
                var pool = ObjectPoolManager.Instance.RequestPool(dropData.Drop);
                
                for (int i = 0; i < dropData.Count; i++)
                {
                    var drop = (DropBase)pool.Rent();
                    drop.transform.position = pos;
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
                            .Subscribe(_ => drop.Finish()).AddTo(this);
                    }
                }
            }
        }
        protected override bool UseDontDestroyOnLoad { get; } = false;
    }
}
