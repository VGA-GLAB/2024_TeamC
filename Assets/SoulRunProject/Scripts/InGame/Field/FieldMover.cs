using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// フィールドを動かすクラス
    /// </summary>
    public class FieldMover : MonoBehaviour, IPausable
    {
        [SerializeField, Header("生成パターン")] List<FieldCreatePattern> _patterns;
        [SerializeField, CustomLabel("最小タイル数")] int _minSegmentCount = 5;
        [SerializeField, CustomLabel("スクロール速度")] float _scrollSpeed = 5f;

        bool _isPause;
        CancellationToken _ctOnDestroy;
        CancellationTokenSource _cts;
        List<FieldSegment> _moveSegments = new();
        /// <summary>ボスステージが始まった時に呼ばれるデリゲート</summary>
        public Action StartBossStage { get; set; }

        public List<FieldSegment> MoveSegments => _moveSegments;

        void Awake()
        {
            _ctOnDestroy = this.GetCancellationTokenOnDestroy();
            FieldControl().Forget(); //  処理開始
        }

        async UniTaskVoid FieldControl()
        {
            foreach (var pattern in _patterns)
            {
                _cts = CancellationTokenSource.CreateLinkedTokenSource(_ctOnDestroy);
                if(pattern.Mode == FieldMoverMode.Boss) StartBossStage?.Invoke();
                
                if (!await FieldScroll(pattern, _cts.Token))
                {
                    //  Exceptionが発生しているなら処理を強制終了させる
                    break;
                }
            }
        }
        /// <summary>
        /// フィールドをスクロールさせる
        /// </summary>
        /// <returns>Exceptionをcatchした時のみfalseを返す</returns>
        async UniTask<bool> FieldScroll(FieldCreatePattern pattern, CancellationToken ct)
        {
            var timer = 0f;
            int segmentIndex = 0;
            bool firstSegmentCreated;
            //  タイマーループ開始
            while (true)
            {
                try
                {
                    //  Pauseフラグがfalseなら1フレーム待つ(ちゃんと1フレーム待ってるか分からない)、
                    //  trueならfalseになるまで通さない
                    await UniTask.WaitUntil(() => !_isPause, PlayerLoopTiming.Update, ct);
                    timer += Time.deltaTime;
                    int loopCount = Mathf.Clamp(_minSegmentCount - _moveSegments.Count, 0, _minSegmentCount);
                    
                    for (int i = 0; i < loopCount; i++)
                    {
                        //  生成したタイルが0個の時
                        if (_moveSegments.Count <= 0)
                        {
                            FieldSegment originalSegment;
                            if (pattern.Mode == FieldMoverMode.Random)
                            {
                                var processor = pattern.AdjacentGraph.Processor;
                                originalSegment = processor.FieldSegmentNodes
                                    [Random.Range(0, processor.FieldSegmentNodes.Count)].FieldSegment;
                            }
                            else
                            {
                                originalSegment = pattern.FieldSegments[segmentIndex];
                            }
                            var segment = Instantiate(originalSegment, transform);
                            segment.ApplyOriginalInstanceID(originalSegment);
                            segment.transform.position = segment.transform.TransformPoint(-segment.StartPos);
                            _moveSegments.Add(segment);
                        }
                        else
                        {   //  生成されたタイルがリストに残ってるとき
                            var prevSegment = _moveSegments[^1];    //  一番後ろの要素を取得
                            var prevEndPos = prevSegment.transform.TransformPoint(prevSegment.EndPos);
                            FieldSegment originalSegment;
                            if (pattern.Mode == FieldMoverMode.Random)
                            {
                                var processor = pattern.AdjacentGraph.Processor;
                                var node = processor.FieldSegmentNodes
                                    .FirstOrDefault(node => node.FieldSegment.GetInstanceID() == prevSegment.OriginalInstanceID);
                                if (node != null)
                                {
                                    var outSegments = node.OutSegments.ToList();
                                    originalSegment = outSegments[Random.Range(0, outSegments.Count)];
                                }
                                else
                                {
                                    originalSegment = processor.FieldSegmentNodes
                                        [Random.Range(0, processor.FieldSegmentNodes.Count)].FieldSegment;
                                }
                            }
                            else
                            {
                                originalSegment = pattern.FieldSegments[segmentIndex];
                            }
                            var segment = Instantiate(originalSegment, transform);
                            segment.ApplyOriginalInstanceID(originalSegment);
                            segment.transform.position = prevEndPos;
                            segment.transform.position = segment.transform.TransformPoint(-segment.StartPos);
                            _moveSegments.Add(segment);
                        }

                        if (pattern.Mode == FieldMoverMode.Random) continue;
                        segmentIndex++;
                        segmentIndex %= pattern.FieldSegments.Count;
                    }

                    List<FieldSegment> list = new();
                    //  フィールドタイル移動処理
                    for (int i = 0; i < _moveSegments.Count; i++)
                    {
                        _moveSegments[i].transform.position += Vector3.back * _scrollSpeed * Time.deltaTime;
                        //  FieldMoverのz座標より後ろに行ったら消す
                        if (_moveSegments[i].transform.TransformPoint(_moveSegments[i].EndPos).z < transform.position.z)
                        {
                            //  破棄する。
                            _moveSegments[i].gameObject.SetActive(false);
                            Destroy(_moveSegments[i].gameObject);
                        }
                        else
                        {
                            list.Add(_moveSegments[i]);
                        }
                    }
                    //  Destroyした要素を取り除く
                    _moveSegments = list;
                    
                    //  制限時間を超えていて生成されているタイルが設定の一番最後なら終了
                    //  一番最後ならインクリメントされて循環バッファで0になっているので0と比較する
                    if (timer >= pattern.Seconds && segmentIndex == 0 && pattern.Mode != FieldMoverMode.Boss)
                    {
                        //  スクロール終了、正常終了
                        return true;
                    }
                }
                catch (OperationCanceledException e)
                {
                    if (_ctOnDestroy.IsCancellationRequested)
                    {
                        Debug.Log($"{nameof(FieldMover)}が正常に破棄された");
                        return false;
                    }

                    Debug.Log($"{nameof(FieldMover)}でキャンセルが呼ばれた。次の生成パターンに移動します。");
                    return true;
                }
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
        /// <summary>
        /// 今動いている生成パターンを強制終了させて次の生成パターンに移る
        /// </summary>
        public void NextField()
        {
            _cts?.Cancel();
        }
    }
}