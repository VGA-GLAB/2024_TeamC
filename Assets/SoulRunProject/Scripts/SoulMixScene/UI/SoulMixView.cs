using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixView : MonoBehaviour
    {
        [SerializeField] private InputUIButton soulMix;

        [SerializeField] private InputUIButton soulSelect;

        [SerializeField] private TMP_Text ownerCardList;

        [SerializeField] private TMP_Text ownerSelectCardList;


        public InputUIButton SoulMix => soulMix;
        public InputUIButton SoulSelect => soulSelect;

        public TMP_Text OwnerCardList => ownerCardList;
        public TMP_Text OwnerSelectCardList => ownerSelectCardList;


        // UniRxの購読をセットアップするメソッドを追加
        public void SetupReactiveUI(ReactiveProperty<SoulCardList> ownedSelectSouls, List<SoulCard> ownerCardList)
        {
            // 所持しているソウルリストの変更を購読
            ownedSelectSouls.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ => UpdateOwnerSelectCardListText(ownedSelectSouls.Value))
                .AddTo(this); // AddTo(this) を呼び出すことで、この GameObject が破棄されたときに購読を自動的に解除します

            // 所持カードリストが変更された場合（例えば、新しいソウルを獲得したとき）、
            // この部分にそのロジックを実装します。以下は、ownerCardListがただのList<T>であれば直接呼び出せる形式の例です。
            UpdateOwnerCardListText(ownerCardList);
        }

        private void UpdateOwnerSelectCardListText(SoulCardList soulCardDataList)
        {
            OwnerSelectCardList.text = string.Join("\n", soulCardDataList.soulCardList.Select(card => card.SoulName));
        }

        private void UpdateOwnerCardListText(List<SoulCard> soulCards)
        {
            OwnerCardList.text = string.Join("\n", soulCards.Select(card => card.SoulName));
        }
    }
}