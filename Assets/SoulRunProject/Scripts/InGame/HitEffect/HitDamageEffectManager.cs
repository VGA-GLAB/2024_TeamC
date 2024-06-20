using DG.Tweening;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 被ダメージ表現を行うクラス
    /// </summary>
    public class HitDamageEffectManager : MonoBehaviour
    {
        // 良い感じの白色
        static readonly Color WhiteColor = new(0.85f, 0.85f, 0.85f, 0.6f);
        [SerializeField, CustomLabel("点滅間隔"), Range(0, 0.1f)] float _duration;
        [SerializeField, CustomLabel("点滅回数")] int _loopCount;
        
        Renderer _renderer;
        Material _copyMaterial;
        Sequence _sequence;
        Color _defaultColor;
        bool _hitFadeBlinking;
        // シェーダーのカラープロパティ取得
        int _pramID = Shader.PropertyToID("_DamageColor");
        
        public Material CopyMaterial => _copyMaterial;

        /// <summary>
        /// マテリアルの複製を行う
        /// </summary>
        protected void Awake()
        {
            if (!TryGetComponent(out _renderer))
            {
                Debug.LogWarning($"{gameObject.name} のレンダラーがアタッチされていません");
                return;
            }
            
            var material = new Material(_renderer.material);
            _copyMaterial = material;

            if (!material.HasColor(_pramID)) // パラメータ名が存在しているか
            {
                _pramID = Shader.PropertyToID("_Color");
            }
            
            _defaultColor = material.GetColor(_pramID);
            _renderer.material = _copyMaterial;

            if (_copyMaterial == null)
            {
                Debug.LogWarning($"{gameObject.name}のMaterialが正常にコピー出来ていません");
            }
        }

        /// <summary>
        /// 色リセットメソッド
        /// </summary>
        public void ResetColor()
        {
            if (!_copyMaterial) return;
            _copyMaterial.SetColor(_pramID, _defaultColor);
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
            _copyMaterial.SetBool("_Boolean", true);
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(() => _defaultColor, c => _copyMaterial.SetColor(_pramID, c), color, _duration));
            _sequence.Append(DOTween.To(() => color, c => _copyMaterial.SetColor(_pramID, c), _defaultColor, _duration));
            _sequence.AppendCallback(() => _copyMaterial.SetBool("_Boolean", false));
            _sequence.SetLoops(_loopCount, LoopType.Restart);
            _sequence.SetLink(gameObject);//.SetLink(gameObject, LinkBehaviour.KillOnDisable);
            _sequence.Play();
            _hitFadeBlinking = true;
            _sequence.OnComplete(() => _hitFadeBlinking = false);
            _sequence.OnKill(ResetColor);
            _sequence.SetUpdate(true);
        }
    }
}

namespace SoulRunProject.Common
{
    public static class MaterialExtension
    {
        // 参考 : https://bravememo.hatenablog.com/entry/2023/11/13/220646
        /// <summary>
        /// マテリアルのboolプロパティに値をセットするメソッド
        /// </summary>
        /// <param name="material">セットする対象</param>
        /// <param name="name">セットするプロパティ名</param>
        /// <param name="flag">セットする値</param>
        public static void SetBool(this Material material, string name, bool flag)
        {
            var num = flag ? 1 : 0;
            material.SetInt(name, num);
        }
    }
}