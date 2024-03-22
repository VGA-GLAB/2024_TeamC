using System.Collections.Generic;
using SoulRun.InGame;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixView : MonoBehaviour
    {
        [SerializeField] private InputUIButton _soulMixButton;
        public InputUIButton SoulMixButton => _soulMixButton;

        [SerializeField] private Text _logText;
        [SerializeField] private Transform _cardContainer; // GridLayoutGroupが適用されているパネルのTransform
        [SerializeField] private GameObject _soulCardPrefab; // ソウルカードのプレファブ

        public void DisplayLogMessage(string message)
        {
            if (_logText != null)
            {
                _logText.text = message;
            }
        }
        public void ClearCards()
        {
            foreach (Transform child in _cardContainer)
            {
                Destroy(child.gameObject);
            }
        }

        // ownedCards のリストを受け取り、UI上に表示する
        public void AddCard(SoulCardData cardData)
        {
            var cardObject = Instantiate(_soulCardPrefab, _cardContainer);
            cardObject.name = cardData.SoulName;
            var cardView = cardObject.GetComponentInChildren<Image>(); // カードのImageコンポーネントを取得
            cardView.sprite = cardData.Image; // カードデータの画像をセット

            var button = cardObject.GetComponentInChildren<Button>(); // Buttonコンポーネントを取得
            if (button != null)
            {
                button.onClick.AddListener(() => Debug.Log("クリックされたカードのデータ: " + cardData.SoulName));
            }
        }
    }
}