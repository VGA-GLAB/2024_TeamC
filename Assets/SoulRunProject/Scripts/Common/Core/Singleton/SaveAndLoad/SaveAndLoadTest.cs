using SoulRun.InGame;
using SoulRunProject.Common;
using UnityEngine;
using UniRx;

namespace SoulRunProject.SoulRunProject.Scripts.Common.Core.Singleton
{
    public class SaveAndLoadTest : MonoBehaviour
    {
        SaveAndLoadManager _saveAndLoadManager;
        [SerializeField] private InputUIButton _saveButton;
        [SerializeField] private InputUIButton _loadButton;

        private void Start()
        {
            _saveAndLoadManager = SaveAndLoadManager.Instance;

            // Saveボタンのクリックイベントを購読
            _saveButton.OnClickAsObservable()
                .Subscribe(_ => OnSaveButtonClicked())
                .AddTo(this); // このSubscribeをDisposeするために、MonoBehaviourに追加します

            // Loadボタンのクリックイベントを購読
            _loadButton.OnClickAsObservable()
                .Subscribe(_ => OnLoadButtonClicked())
                .AddTo(this);
        }

        // Saveボタンがクリックされたときの処理
        private void OnSaveButtonClicked()
        {
            // ここにSaveボタンが押されたときの処理を記述
        }

        // Loadボタンがクリックされたときの処理
        private void OnLoadButtonClicked()
        {
            // ここにLoadボタンが押されたときの処理を記述
        }
    }
}


/*

DataStorage{
    PlayerData
    MasterData
}


MasterData{
    SoulCardDataList
    SoulCardDataCombinations
    EnemyDataList
    ItemDataList
    PlayerDataList
}

MasterData
    Version v0.1
        SoulCardDataList
        SoulCardDataCombinations
        EnemyDataList
        ItemDataList
        PlayerDataList

PlayerData
    PlayerDataID (固有識別番号16進数の10桁)
    PlayerName
    Stage1
        MaxScore
    Stage2
        MaxScore
    CurrentMoney
    CurrentSoulCardDataList



SoulCardData
    CardID(固有番号)
    IndividualIdentificationNumber(固有識別番号16進数の10桁)
    Image
    SoulName
    SoulLevel
    SoulAbility
    Status
    TraitList

Combinations
    Ingredient1
    Ingredient2
    Result

EnemyData
    EnemyID
    EnemyImage
    EnemyName
    EnemyLevel
    EnemyAbility
    EnemyStatus

ItemDataList
    ItemID
    ItemImage
    ItemName
*/