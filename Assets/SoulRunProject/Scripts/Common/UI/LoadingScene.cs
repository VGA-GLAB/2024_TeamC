using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoulRunProject.Common;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SoulRunProject
{
    public class LoadingScene : AbstractSingletonMonoBehaviour<LoadingScene>
    {
        [Tooltip("ロード中に表示するUI")] [SerializeField]
        private GameObject _loadingUI;

        [Tooltip("フェードに使用するマテリアル")] [SerializeField]
        private Material _fadeMaterial;

        [Tooltip("フェードに使用するマスクテクスチャ")] [SerializeField]
        private Texture _maskTexture;

        [Tooltip("フェードの範囲")] [SerializeField, Range(0, 1)]
        private float _cutoutRange;

        [Tooltip("プログレスバー")] [SerializeField] private Slider _slider;
        [Tooltip("フェード時間")] [SerializeField] private float _fadeDuration = 1.0f;
        [Tooltip("最低ロード時間")] [SerializeField] private float _minimumLoadTime = 2.0f;


        protected override bool UseDontDestroyOnLoad => true;
        private AsyncOperation _async;
        private Action _onComplete;

        // プロパティIDをキャッシュ
        private static readonly int MaskTex = Shader.PropertyToID("_MaskTex");
        private static readonly int Range1 = Shader.PropertyToID("_Range");

#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (_fadeMaterial == null) return;
            _fadeMaterial.SetFloat(Range1, 1 - _cutoutRange);
            Debug.Log($"{Range1}");
        }
#endif


        private void Start()
        {
            _fadeMaterial.SetTexture(MaskTex, _maskTexture);
        }

        /// <summary> 次のシーンをロードする </summary>
        public async UniTask LoadNextScene(string sceneName)
        {
            await FadeOut();

            _loadingUI.SetActive(true);
            var startTime = Time.time;
            var targetProgress = 0f;
            var displayProgress = 0f;

            _async = await SceneController.Instance.LoadSceneAsync(sceneName);

            while (Time.time - startTime < _minimumLoadTime || _async is { isDone: false })
            {
                if (_async != null)
                {
                    targetProgress = Mathf.Clamp01(_async.progress);
                }

                // プログレスバーを滑らかに進める
                displayProgress = Mathf.MoveTowards(
                    displayProgress, targetProgress, Time.deltaTime / _minimumLoadTime);
                _slider.value = displayProgress;

                await UniTask.DelayFrame(1);
            }

            _loadingUI.SetActive(false);
            _onComplete?.Invoke();

            await FadeIn();
        }

        /// <summary> ロード完了時に実行する処理を登録する </summary>
        private async UniTask FadeOut()
        {
            // DOTweenを使用してRangeパラメータをアニメーション
            DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 1, _fadeDuration)
                .OnUpdate(() => _fadeMaterial.SetFloat(Range1, 1 - _cutoutRange))
                .SetEase(Ease.Linear);

            // フェードアウトが終わるまで待機する
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));
        }

        private async UniTask FadeIn()
        {
            // DOTweenアニメーションを待機する
            DOTween.To(() => _cutoutRange, x => _cutoutRange = x, 0, _fadeDuration)
                .OnUpdate(() => _fadeMaterial.SetFloat(Range1, 1 - _cutoutRange))
                .SetEase(Ease.Linear);
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));
        }
    }
}