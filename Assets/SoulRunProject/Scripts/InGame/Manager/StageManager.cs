using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// インゲーム中のステートの管理
    /// ステージデータの所持
    /// </summary>
    public class StageManager : MonoBehaviour, IPausable
    {
        [SerializeField, EnumDrawer(typeof(StageName))] private StageData[] _stageDatas;

        private bool _isPause;
        private int _stageDataIndex;
        private float _stageTimer;
        private StageState _currentStageState;

        /// <summary> 現在のFieldPattern </summary>
        public FieldCreatePattern CurrentFieldCreatePattern { get; private set; }
        public Action ToNextStage;
        public Action ToBossStage;

        private void Start()
        {
            CurrentFieldCreatePattern = _stageDatas[_stageDataIndex].FieldPattern;
            _currentStageState = StageState.PlayingRunGame;

            ToNextStage += NextStage;
            ToBossStage += BossStage;
        }

        /// <summary> PlayingRunGameStateでのUpdate </summary>
        public void PlayingRunGameUpdate()
        {
            // タイマー管理
            if (_currentStageState == StageState.PlayingRunGame)
            {
                _stageTimer += Time.deltaTime;

                if (_stageDatas[_stageDataIndex].PlayingRunTime <= _stageTimer) // 一定時間でボスステージに移行
                {
                    ToBossStage?.Invoke();
                }
            }
        }

        /// <summary> ボスステージに移行 </summary>
        void BossStage()
        {
            _currentStageState = StageState.PlayingBossStage;
            CurrentFieldCreatePattern = _stageDatas[_stageDataIndex].BossStageFieldCreatePattern;
            // ボスの生成と通知設定
            Instantiate(_stageDatas[_stageDataIndex].BossPrefab).GetComponent<DamageableEntity>().OnDead += () => ToNextStage?.Invoke();
        }
        
        /// <summary> 次のステージに移行 </summary>
        private void NextStage()
        {
            _currentStageState = StageState.PlayingRunGame;
            // づぎのステージデータに進む　最後のデータなら繰り返す
            CurrentFieldCreatePattern = 
                _stageDatas[_stageDataIndex = Mathf.Min(_stageDataIndex + 1, _stageDatas.Length - 1)].FieldPattern;
            _stageTimer = 0;
        }

        public void Pause(bool isPause) => _isPause = isPause;

        enum StageState
        {
            PlayingRunGame,
            PlayingBossStage
        }

        enum StageName
        {
            Stage1,
            Stage2
        }
    }

    /// <summary>
    /// 各ステージの情報
    /// </summary>
    [Serializable]
    public class StageData
    {
        [SerializeField, CustomLabel("ボスまでの時間")] private float _playingRunTime;
        [SerializeField, CustomLabel("フィールド生成パターン")] private FieldCreatePattern _fieldPattern;
        [SerializeField, CustomLabel("ボスステージのフィールド生成パターン")] private FieldCreatePattern _bossStageFieldPattern;
        [SerializeField, CustomLabel("ボス")] private BossController _bossPrefab; // 仮

        /// <summary> ボスまでの時間 </summary>
        public float PlayingRunTime => _playingRunTime;
        /// <summary> 通常時(PlayingRunGame)のFieldPattern </summary>
        public FieldCreatePattern FieldPattern => _fieldPattern;
        /// <summary> ボスステージのFieldPattern </summary>
        public FieldCreatePattern BossStageFieldCreatePattern => _bossStageFieldPattern;
        public BossController BossPrefab => _bossPrefab;
    }
}
