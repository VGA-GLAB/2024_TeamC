using UnityEngine;
using DG.Tweening;

public class ConcentrationLineController : MonoBehaviour
{
    [SerializeField] private Material _concentrationLineMaterial;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _minSpeed = 10f;
    [SerializeField] private float _fadeDuration = 0.5f;

    private float _currentSpeed;
    private static readonly int scrollSpeed = Shader.PropertyToID("_ScrollSpeed");
    private static readonly int noiseBlend = Shader.PropertyToID("_NoiseBlend");

    private void Start()
    {
        _currentSpeed = 0;
        SetConcentrationLine(0); // 初期は非表示
    }

    public void UpdateConcentrationLine(float playerSpeed)
    {
        // プレイヤーの速度に応じて段階的に集中線の強さを変える
        if (playerSpeed < _minSpeed)
        {
            SetConcentrationLine(0); // 出さない
        }
        else if (playerSpeed < (_maxSpeed + _minSpeed) / 2)
        {
            SetConcentrationLine(0.5f); // 少し出る
        }
        else
        {
            SetConcentrationLine(1f); // 最大
        }
    }

    private void SetConcentrationLine(float targetValue)
    {
        DOTween.To(() => _currentSpeed, x => _currentSpeed = x, targetValue, _fadeDuration).OnUpdate(() =>
        {
            // シェーダーの速度を制御する
            _concentrationLineMaterial.SetFloat(scrollSpeed, Mathf.Lerp(_minSpeed, _maxSpeed, _currentSpeed));
            _concentrationLineMaterial.SetFloat(noiseBlend, Mathf.Lerp(0, 0.5f, _currentSpeed));
        });
    }
}