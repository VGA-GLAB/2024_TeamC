using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class CriTest : MonoBehaviour
    {
        private CriAudioManager _audioManager;

        [SerializeField] private List<string> _seCueNames;
        private List<int> _seIndexes;

        void Start()
        {
            // CriAudioManagerのインスタンスを取得
            _audioManager = CriAudioManager.Instance;

            // ボリューム変更のイベントリスナーを設定
            _audioManager.MasterVolumeChanged += volume => Debug.Log($"Master Volume Changed to: {volume}");
            _audioManager.BGMVolumeChanged += volume => Debug.Log($"BGM Volume Changed to: {volume}");
            _audioManager.SEVolumeChanged += volume => Debug.Log($"SE Volume Changed to: {volume}");
            _audioManager.MEVolumeChanged += volume => Debug.Log($"ME Volume Changed to: {volume}");

            // テストケースを実行
            _ = TestPlayBGM();
                //_ = TestPlaySE();
            //TestPlayVoice();
            //TestVolumeChanges();
        }

        async UniTaskVoid TestPlayBGM()
        {
            Debug.Log("Playing BGM...");
            _audioManager.PlayBGM("BGM_Title");
            // BGMの一時停止
            //await UniTask.Delay(1000);
            //_audioManager.PauseBGM();
            // BGMの再開
            //await UniTask.Delay(1000);
            //_audioManager.ResumeBGM();
            //await UniTask.Delay(1000);
            // BGMの停止
            //_audioManager.StopBGM();
        }

        async UniTaskVoid TestPlaySE()
        {
            _seIndexes = new List<int>();

            Debug.Log("Playing SE...");
            foreach (var cueName in _seCueNames)
            {
                int seIndex = _audioManager.PlaySE(cueName, 0.8f);
                _seIndexes.Add(seIndex);
            }

            // SEの一時停止
            foreach (var index in _seIndexes)
            {
                _audioManager.PauseSE(index);
                Debug.Log("Paused SE");
            }

            await UniTask.Delay(1000);

            // SEの再開
            foreach (var index in _seIndexes)
            {
                _audioManager.ResumeSE(index);
                Debug.Log("Resumed SE");
            }

            await UniTask.Delay(1000);

            // SEの停止
            foreach (var index in _seIndexes)
            {
                _audioManager.StopSE(index);
                Debug.Log("Stopped SE");
            }
        }

        async UniTaskVoid TestPlayME()
        {
            Debug.Log("Playing Voice...");
            int voiceIndex = _audioManager.PlayME("voice_cue_name", 0.9f);
            // Voiceの一時停止
            await UniTask.Delay(1000);
            _audioManager.PauseME(voiceIndex);
            // Voiceの再開
            await UniTask.Delay(1000);
            _audioManager.ResumeME(voiceIndex);
            await UniTask.Delay(1000);
            // Voiceの停止
            _audioManager.StopME(voiceIndex);
        }

        async UniTaskVoid TestVolumeChanges()
        {
            Debug.Log("Changing Volumes...");
            // マスターボリュームの変更
            await UniTask.Delay(1000);
            _audioManager.MasterVolume = 0.5f;
            // BGMボリュームの変更
            await UniTask.Delay(1000);
            _audioManager.BGMVolume = 0.6f;
            // SEボリュームの変更
            await UniTask.Delay(1000);
            _audioManager.SEVolume = 0.7f;
            // Voiceボリュームの変更
            await UniTask.Delay(1000);
            _audioManager.MEVolume = 0.8f;
        }
    }
}