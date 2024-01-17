using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun
{
    public class PlayerHPView : MonoBehaviour
    {
        [SerializeField] Text _hpText;

        public void SetHpText(float hp)
        {
            _hpText.text = $"{hp}";
        }
    }
}
