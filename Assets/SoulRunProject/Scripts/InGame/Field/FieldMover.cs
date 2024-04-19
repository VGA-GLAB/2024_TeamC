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
        [SerializeField, Header("タイル同士の隣接リスト")] AdjacentGraph _adjacentGraph;
        [SerializeField, Header("最小タイル数")] int _minSegmentCount = 5;
        [SerializeField, Header("スクロール速度")] float _scrollSpeed = 5f;

        bool _isPause;
        CancellationToken _ct;
        List<FieldSegment> _moveSegments = new();
        AdjacentGraphProcessor _processor;

        void Awake()
        {
            //  タイルの隣接リスト取得
            _processor = new AdjacentGraphProcessor(_adjacentGraph);
            _processor.Run();
            
            _ct = this.GetCancellationTokenOnDestroy();
            FieldControl(_ct).Forget(); //  処理開始
        }

        async UniTaskVoid FieldControl(CancellationToken ct)
        {
            foreach (var pattern in _patterns)
            {
                if (await FieldScroll(pattern, ct))
                {
                    Debug.Log($"End Scroll: {pattern.Seconds}");
                }
                else
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
                        if (_moveSegments.Count <= 0)
                        {
                            FieldSegment segment;
                            if (pattern.IsRandom)
                            {
                                var randomSegment = _processor.FieldSegmentNodes
                                    [Random.Range(0, _processor.FieldSegmentNodes.Count)].FieldSegment;
                                segment = Instantiate(randomSegment, transform);
                                segment.ApplyOriginalInstanceID(randomSegment);
                            }
                            else
                            {
                                segment = Instantiate(pattern.FieldSegments[segmentIndex], transform);
                                segment.ApplyOriginalInstanceID(pattern.FieldSegments[segmentIndex]);
                            }
                            segment.transform.position = segment.transform.TransformPoint(-segment.StartPos);
                            _moveSegments.Add(segment);
                        }
                        else
                        {
                            var prevSegment = _moveSegments[^1];
                            var prevEndPos = prevSegment.transform.TransformPoint(prevSegment.EndPos);
                            FieldSegment segment;
                            if (pattern.IsRandom)
                            {
                                var node = _processor.FieldSegmentNodes
                                    .FirstOrDefault(node => node.FieldSegment.GetInstanceID() == prevSegment.OriginalInstanceID);
                                if (node != null)
                                {
                                    var outSegments = node.OutSegments.ToList();
                                    var randomSegment = outSegments[Random.Range(0, outSegments.Count)];
                                    segment = Instantiate(randomSegment, transform);
                                    segment.ApplyOriginalInstanceID(randomSegment);
                                }
                                else
                                {
                                    var randomSegment = _processor.FieldSegmentNodes
                                        [Random.Range(0, _processor.FieldSegmentNodes.Count)].FieldSegment;
                                    segment = Instantiate(randomSegment, transform);
                                    segment.ApplyOriginalInstanceID(randomSegment);
                                }
                            }
                            else
                            {
                                segment = Instantiate(pattern.FieldSegments[segmentIndex], transform);
                                segment.ApplyOriginalInstanceID(pattern.FieldSegments[segmentIndex]);
                            }
                            segment.transform.position = prevEndPos;
                            segment.transform.position = segment.transform.TransformPoint(-segment.StartPos);
                            _moveSegments.Add(segment);
                        }

                        if (!pattern.IsRandom)
                        {
                            segmentIndex++;
                            segmentIndex %= pattern.FieldSegments.Count;
                        }
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
                    if (timer >= pattern.Seconds && segmentIndex == 0 && !pattern.IsInfinity)
                    {
                        //  スクロール終了、正常終了
                        return true;
                    }
                }
                catch (OperationCanceledException e)
                {  
                    //  キャンセル時処理、異常終了
                    return false;
                }
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}