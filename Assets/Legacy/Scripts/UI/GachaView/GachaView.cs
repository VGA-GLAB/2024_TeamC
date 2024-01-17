using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using UniRx;
using System.Linq;

namespace SoulRun.InGame
{
    /// <summary>
    /// 報酬画面の表示管理を行う
    /// </summary>
    public class GachaView : MonoBehaviour
    {
        [SerializeField] GameObject gachaPanel;
        [SerializeField] List<Button> gachaButtons;

        private void Start()
        {
            gachaPanel.SetActive(false);
        }

        public async UniTask SelectReward()
        {
            ShowRewardsPanel();
            var result = await Observable.Merge(
                gachaButtons.Select(button => button.OnClickAsObservable().Select(_ => button.name))
            )
            .ToUniTask(useFirstValue: true);
            HideRewardsPanel();
        }

        private void ShowRewardsPanel()
        {
            gachaPanel.SetActive(true);
        }

        private void HideRewardsPanel()
        {
            gachaPanel.SetActive(false);
        }
    }
}
