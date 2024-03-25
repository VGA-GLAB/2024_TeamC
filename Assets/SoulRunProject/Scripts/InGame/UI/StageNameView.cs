using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SoulRunProject.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject
{
    /// <summary>
    /// ステージ名を表示する
    /// </summary>
    public class StageNameView : MonoBehaviour
    {
        [SerializeField] private GameObject _stageNameObject;
        [SerializeField] private float _displayTime;
        [SerializeField] private Text _stageNameText;
        [SerializeField] private Image _stageNameImage;

        public void ShowStageName()
        {
            var sequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    _stageNameObject.SetActive(true);
                    _stageNameText.color = new Color(1, 1, 1, 0);
                    _stageNameImage.color = new Color(1, 1, 1, 0);
                })
                .Append(_stageNameText.DOFade(1f, _displayTime))
                .Join(_stageNameImage.DOFade(1f, _displayTime))
                .Append(_stageNameText.DOFade(0f, _displayTime))
                .Join(_stageNameImage.DOFade(0f, _displayTime))
                .OnComplete(() =>
                {
                    _stageNameObject.SetActive(false);
                });
        }
    }
}