using DG.Tweening;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 被ダメージ表現を行うクラス
    /// </summary>
    public class HitDamageEffectManager : MonoBehaviour
    {
        // シェーダーのカラープロパティ取得
        static readonly int PramID = Shader.PropertyToID("_Color");
        // 良い感じの白色
        static readonly Color WhiteColor = new(0.85f, 0.85f, 0.85f, 0.6f);
        [SerializeField, Tooltip("点滅時間")] float _duration;
        [SerializeField, Tooltip("点滅回数")] int _loopCount;
        Renderer _renderer;
        Material _copyMaterial;
        Sequence _sequence;
        Color _defaultColor;

        /// <summary>
        /// マテリアルの複製を行う
        /// </summary>
        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            var material = _renderer.material;
            _copyMaterial = material;
            _defaultColor = material.color;
        }
        
        /// <summary>
        /// 白色点滅メソッド
        /// </summary>
        public void HitFadeBlinkWhite()
        {
            HitFadeBlink(WhiteColor);
        }

        /// <summary>
        /// 色指定点滅メソッド
        /// </summary>
        /// <param name="color">点滅色</param>
        public void HitFadeBlink(Color color)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(() => _defaultColor, c => _copyMaterial.SetColor(PramID, c), color, _duration));
            _sequence.Append(DOTween.To(() => color, c => _copyMaterial.SetColor(PramID, c), color, _duration));
            _sequence.SetLoops(_loopCount, LoopType.Restart);
            _sequence.SetLink(gameObject);
            _sequence.Play();
        }
#if UNITY_EDITOR
        /// <summary>
        /// ボタンでのテスト用メソッド
        /// </summary>
        public void HitFadeBlinkTest()
        {
            HitFadeBlinkWhite();
        }
#endif
    }
}