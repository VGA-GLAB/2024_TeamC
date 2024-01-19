using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    public class SoulView : MonoBehaviour
    {
        // 名前、レベル、画像、能力を表示する
        
        [SerializeField] private Text soulNameText;
        [SerializeField] private Text soulLevelText;
        [SerializeField] private Sprite soulImage;
        [SerializeField] private List<TraitDefine> soulAbilityTextList;
        
        
    }
}