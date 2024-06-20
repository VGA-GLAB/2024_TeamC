using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// インゲーム中のステートの管理
    /// ステージデータの所持
    /// </summary>
    public class StageManager : MonoBehaviour
    {
        [SerializeField, EnumDrawer(typeof(StageName))] private StageDatum[] _stageData;
        [SerializeField] private FieldMover _fieldMover;
        private int _stageDataIndex;
        private int _fieldPatternIndex;
        private int _fieldSegmentIndex;
        private float _fieldTimer;
        private StageState _currentStageState;

        public Action ToNextStage;
        public Action ToBossStage;

        private void Start()
        {
            _currentStageState = StageState.PlayingRunGame;

            ToNextStage += NextStage;
            ToBossStage += BossStage;
        }

        /// <summary> PlayingRunGameStateでのUpdate </summary>
        public void PlayingRunGameUpdate()
        {
            if (_currentStageState != StageState.PlayingRunGame) return;
            
            var pattern = _stageData[_stageDataIndex].FieldPatterns[_fieldPatternIndex];
            if (pattern.Mode == FieldMoverMode.Order)
            {
                for (int i = 0; i < _fieldMover.FreeMoveSegmentsCount; i++)
                {
                    GenerateFieldToOrder(pattern, ref _fieldSegmentIndex);
                }
                //  生成パターンの時間を超えている && 現在生成されているタイルが生成パターンの順番の一番後ろかどうか
                if (_fieldTimer >= pattern.Seconds && _fieldSegmentIndex == 0)
                {
                    NextPattern();
                }
                else
                {
                    _fieldTimer += Time.deltaTime;
                }
            }
            else if (pattern.Mode == FieldMoverMode.Random)
            {
                for (int i = 0; i < _fieldMover.FreeMoveSegmentsCount; i++)
                {
                    GenerateFieldToRandom(pattern);
                }
                //  生成パターンの制限時間を超えているかどうか
                if (_fieldTimer >= pattern.Seconds)
                {
                    FieldSegment nextSegment = null;
                    bool nextIsRandom = false;
                
                    if (_stageData[_stageDataIndex].FieldPatterns.Count > _fieldPatternIndex + 1)
                    {
                        var nextPattern = _stageData[_stageDataIndex].FieldPatterns[_fieldPatternIndex + 1];
                        nextIsRandom = nextPattern.Mode == FieldMoverMode.Random;
                        if (!nextIsRandom && nextPattern.FieldSegments.Count > 0)
                        {
                            nextSegment = nextPattern.FieldSegments[0];
                        }
                    }
                    else
                    {
                        nextSegment = _stageData[_stageDataIndex].BossStageField;
                    }
                
                    var processor = pattern.AdjacentGraph.Processor;
                    var lastSegment = _fieldMover.MoveSegments[^1];
                
                    //  次に流れる予定のタイルがリストに登録されているかどうか
                    bool registeredNextSegment = processor.FieldSegmentNodes.Any(node=>node.FieldSegment == nextSegment);
                    //  最後のタイルが次のタイルに繋げられるかどうか
                    bool canTheNextSegmentBeConnected = true;
                    var lastSegmentNode = processor.FieldSegmentNodes.FirstOrDefault(node=>node.FieldSegment == lastSegment);
                    if (lastSegmentNode != null)
                    {
                        canTheNextSegmentBeConnected = lastSegmentNode.OutSegments.Any(node=>node.FieldSegment == nextSegment);
                    }
                    
                    //  次の生成パターンがランダムかどうか
                    //  or 次に生成されるタイルが隣接リストに登録されていない
                    //  or 現在生成されているタイルが次のタイルと繋がるかどうか
                    if (nextIsRandom || !registeredNextSegment || canTheNextSegmentBeConnected)
                    {
                        NextPattern();
                    }
                }
                else
                {
                    _fieldTimer += Time.deltaTime;
                }
            }
        }

        void NextPattern()
        {
            if (_stageData[_stageDataIndex].FieldPatterns.Count > _fieldPatternIndex + 1)
            {
                //  次の生成パターンへ移行
                _fieldPatternIndex++;
            }
            else
            {
                //  次の生成パターンが無いならボスステージへ移行する
                ToBossStage?.Invoke();
            }

            _fieldTimer = 0;
        }

        public void PlayingBossStageUpdate()
        {
            if (_currentStageState != StageState.PlayingBossStage) return;
            
            for (int i = 0; i < _fieldMover.FreeMoveSegmentsCount; i++)
            {
                if (_fieldMover.MoveSegments.Count <= 0)
                {
                    var instantiatedSegment = InstantiateSegment(_stageData[_stageDataIndex].BossStageField, true);
                    _fieldMover.MoveSegments.Add(instantiatedSegment);
                }
                else
                {
                    //  一番後ろのタイルを取得
                    var prevSegment = _fieldMover.MoveSegments[^1]; 
                    //  一番後ろのタイルのEndPosの座標を保存する
                    var prevEndPos = prevSegment.transform.TransformPoint(prevSegment.EndPos);
                    var instantiatedSegment = InstantiateSegment(_stageData[_stageDataIndex].BossStageField, false, prevEndPos);
                    _fieldMover.MoveSegments.Add(instantiatedSegment);
                }
            }
        }

        /// <summary> ボスステージに移行 </summary>
        void BossStage()
        {
            _currentStageState = StageState.PlayingBossStage;
            Invoke(nameof(SpawnBoss), _stageData[_stageDataIndex].DelayBossSpawn);
        }

        /// <summary> ボス生成 </summary>
        void SpawnBoss()
        {
            // ボスの生成と通知設定
            BossController boss = Instantiate(_stageData[_stageDataIndex].BossPrefab);
            boss.InitializePosition(_fieldMover.MoveSegments[^1].transform.TransformPoint(_fieldMover.MoveSegments[^1].StartPos).x);
            boss.GetComponent<DamageableEntity>().OnDead += () => ToNextStage?.Invoke();
            CriAudioManager.Instance.PlayBGM("BGM_Boss"); // 同じタイミングでBGM始める
        }
        
        /// <summary> 次のステージに移行 </summary>
        private void NextStage()
        {
            _currentStageState = StageState.PlayingRunGame;
            _stageDataIndex++;
            _stageDataIndex %= _stageData.Length;
            _fieldTimer = 0;
            _fieldPatternIndex = 0;
        }

        /// <summary>
        /// 設定された順番通りにフィールド生成
        /// </summary>
        private void GenerateFieldToOrder(FieldCreatePattern pattern, ref int segmentIndex)
        {
            if (pattern.Mode == FieldMoverMode.Random) return;
            
            FieldSegment originalSegment;
            Vector3 prevEndPos = Vector3.zero;
            bool isFirstSegment = false;
            if (_fieldMover.MoveSegments.Count <= 0)
            {   //  生成したタイルが0個の時
                //  FieldSegmentsに登録されているタイルの一番最初を選ぶ
                originalSegment = pattern.FieldSegments[segmentIndex];
                isFirstSegment = true;
            }
            else
            {   //  生成されたタイルが1つ以上あるとき
                //  一番後ろのタイルを取得
                var prevSegment = _fieldMover.MoveSegments[^1]; 
                //  一番後ろのタイルのEndPosの座標を保存する
                prevEndPos = prevSegment.transform.TransformPoint(prevSegment.EndPos);
                //  FieldSegmentsに設定されている次のタイルを持ってくる
                originalSegment = pattern.FieldSegments[segmentIndex];
            }
            
            var instantiatedSegment = InstantiateSegment(originalSegment, isFirstSegment, prevEndPos);
            _fieldMover.MoveSegments.Add(instantiatedSegment);
            segmentIndex++;
            segmentIndex %= pattern.FieldSegments.Count;
        }
        /// <summary>
        /// ランダムにフィールド生成
        /// </summary>
        private void GenerateFieldToRandom(FieldCreatePattern pattern)
        {
            if (pattern.Mode == FieldMoverMode.Order) return;
            
            FieldSegment originalSegment;
            Vector3 prevEndPos = Vector3.zero;
            bool isFirstSegment = false;
            
            if (_fieldMover.MoveSegments.Count <= 0)
            {
                //  AdjacentGraphの中のタイルからランダムで選ぶ
                var processor = pattern.AdjacentGraph.Processor;
                var allSegments = processor.FieldSegmentNodes.Where(node => !node.NotRandomInstantiate).ToList();
                originalSegment = allSegments[Random.Range(0, allSegments.Count)].FieldSegment;
                isFirstSegment = true;
            }
            else
            {
                //  一番後ろのタイルを取得
                var prevSegment = _fieldMover.MoveSegments[^1]; 
                //  一番後ろのタイルのEndPosの座標を保存する
                prevEndPos = prevSegment.transform.TransformPoint(prevSegment.EndPos);
                
                var processor = pattern.AdjacentGraph.Processor;
                //  AdjacentGraphに一番後ろのタイルが登録されているかを探す
                var node = processor.FieldSegmentNodes
                    .FirstOrDefault(node =>
                        node.FieldSegment.GetInstanceID() == prevSegment.OriginalInstanceID);
                if (node != null)
                {   //  AdjacentGraphに一番後ろのタイルが登録されていた場合
                    //  そのタイルの出口と隣接しているタイルからランダムにタイルを選ぶ
                    var outSegments = node.OutSegments.Where(node=>!node.NotRandomInstantiate).ToList();
                    originalSegment = outSegments[Random.Range(0, outSegments.Count)].FieldSegment;
                }
                else
                {   //  登録されていなかった場合
                    //  AdjacentGraphに登録されているタイルからランダムに選ぶ
                    var allSegments = processor.FieldSegmentNodes.Where(node => !node.NotRandomInstantiate).ToList();
                    originalSegment = allSegments[Random.Range(0, allSegments.Count)].FieldSegment;
                }
            }
            var instantiatedSegment = InstantiateSegment(originalSegment, isFirstSegment, prevEndPos);
            _fieldMover.MoveSegments.Add(instantiatedSegment);
        }

        /// <summary>
        /// フィールドを実体化させるメソッド
        /// </summary>
        /// <param name="original">実体化させるプレハブ</param>
        /// <param name="isFirstSegment">実体化しているフィールドが無い状態か</param>
        /// <param name="prevEndPos">実体化しているフィールドがある場合、一番後ろのフィールドのEndPosの座標</param>
        /// <returns>実体化したフィールド</returns>
        private FieldSegment InstantiateSegment(FieldSegment original, bool isFirstSegment, Vector3 prevEndPos = default)
        {
            //  タイル実体化(fieldMoverの子オブジェクトにする)
            var instantiatedSegment = Instantiate(original, _fieldMover.transform);
            //  タイルのプレハブのInstanceIDをメモする
            instantiatedSegment.ApplyOriginalInstanceID(original);
            if (!isFirstSegment)
            {   //  実体化しているタイルで一番最初のタイルじゃないのなら
                //  前のタイルのEndPosの座標に生成したタイルを移動させる
                instantiatedSegment.transform.position = prevEndPos;
            }
            //  タイルに設定された-StartPos分だけ座標をずらす
            instantiatedSegment.transform.position = instantiatedSegment.transform
                .TransformPoint(-instantiatedSegment.StartPos);

            return instantiatedSegment;
        }

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
    public class StageDatum
    {
        [SerializeField, EnumDrawer(typeof(Count))] private List<FieldCreatePattern> _fieldPatterns;
        [SerializeField, CustomLabel("ボスステージのタイル")] private FieldSegment _bossStageField;
        [SerializeField, CustomLabel("ボス出現までのディレイ")] private float _delayBossSpawn;
        [SerializeField, CustomLabel("ボス")] private BossController _bossPrefab; // 仮
        
        /// <summary> 通常時(PlayingRunGame)のFieldPattern </summary>
        public List<FieldCreatePattern> FieldPatterns => _fieldPatterns;
        /// <summary> ボスステージのFieldPattern </summary>
        public FieldSegment BossStageField => _bossStageField;
        public float DelayBossSpawn => _delayBossSpawn;
        public BossController BossPrefab => _bossPrefab;
    }

    public enum Count
    {
        pattern1,
        pattern2,
        pattern3,
        pattern4,
        pattern5,
        pattern6,
        pattern7,
        pattern8,
        pattern9,
        pattern10,
        pattern11,
        pattern12,
        pattern13,
        pattern14,
        pattern15,
        pattern16,
        pattern17,
        pattern18,
        pattern19,
    }
}
