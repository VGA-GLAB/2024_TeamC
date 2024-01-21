using System;
using System.Collections.Generic;
using CriWare;
using SoulRunProject.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRun.Core
{
    public class CriAudioManager : AbstractSingleton<CriAudioManager>
    {
        [SerializeField] string streamingAssetsPathAcf = "";
        [SerializeField] string cueSheetBGM = "CueSheet_BGM"; //.acb
        [SerializeField] string cueSheetSe = "CueSheet_SE"; //.acb
        [SerializeField] string cueSheetVoice = "CueSheet_Voice"; //.acb
    

        private float _masterVolume = 1F;
        private float _bgmVolume = 1F;
        private float _seVolume = 1F;
        private float _voiceVolume = 1F;
        private const float Diff = 0.01F;

        /// <summary>マスターボリュームが変更された際に呼ばれるEvent</summary>
        public Action<float> MasterVolumeChanged;

        /// <summary>BGMボリュームが変更された際に呼ばれるEvent</summary>
        public Action<float> BGMVolumeChanged;

        /// <summary>SEボリュームが変更された際に呼ばれるEvent</summary>
        public Action<float> SEVolumeChanged;

        /// <summary>Voiceボリュームが変更された際に呼ばれる処理</summary>
        public Action<float> VoiceVolumeChanged;

        private CriAtomExPlayer _bgmPlayer = new CriAtomExPlayer();
        private CriAtomExPlayback _bgmPlayback;

        private CriAtomExPlayer _sePlayer = new CriAtomExPlayer();
        private CriAtomExPlayer _loopSEPlayer = new CriAtomExPlayer();
        private List<CriPlayerData> _seData = new List<CriPlayerData>();

        private CriAtomExPlayer _voicePlayer = new CriAtomExPlayer();
        private List<CriPlayerData> _voiceData = new List<CriPlayerData>();

        private string _currentBGMCueName = "";
        private CriAtomExAcb _currentBGMAcb = null;

        private CueSheet _cueSheet = CueSheet.None;

        /// <summary>
        /// CueSheetの選択
        /// </summary>
        public enum CueSheet
        {
            None,
            Bgm,
            Se,
            Voice
        }


        /// <summary>
        /// enum からstringを返す
        /// </summary>
        /// <param name="cueSheet"></param>
        /// <returns></returns>
        string GetCueSheetString(CueSheet cueSheet)
        {
            if (cueSheet == CueSheet.Bgm)
            {
                return cueSheetBGM;
            }
            else if (cueSheet == CueSheet.Se)
            {
                return cueSheetSe;
            }
            else if (cueSheet == CueSheet.Voice)
            {
                return cueSheetVoice;
            }

            return null;
        }

        /// <summary>CriAtom の追加。acb追加</summary>
        protected override void OnAwake()
        {
            // acf設定
            string path = CriWare.Common.streamingAssetsPath + $"/{streamingAssetsPathAcf}.acf";
            CriAtomEx.RegisterAcf(null, path);
            // CriAtom作成
            new GameObject().AddComponent<CriAtom>();
            // BGM acb追加
            CriAtom.AddCueSheet(cueSheetBGM, $"{cueSheetBGM}.acb", null, null);
            // SE acb追加
            CriAtom.AddCueSheet(cueSheetSe, $"{cueSheetSe}.acb", null, null);
            //Voice acb追加
            CriAtom.AddCueSheet(cueSheetVoice, $"{cueSheetVoice}.acb", null, null);
        }

        /// <summary>マスターボリューム</summary>
        /// <value>変更したい値</value>
        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                if (_masterVolume + Diff < value || _masterVolume - Diff > value)
                {
                    MasterVolumeChanged.Invoke(value);
                    _masterVolume = value;
                }
            }
        }

        /// <summary>BGMボリューム</summary>
        /// <value>変更したい値</value>
        public float BGMVolume
        {
            get => _bgmVolume;
            set
            {
                if (_bgmVolume + Diff < value || _bgmVolume - Diff > value)
                {
                    BGMVolumeChanged.Invoke(value);
                    _bgmVolume = value;
                }
            }
        }

        /// <summary>マスターボリューム</summary>
        /// <value>変更したい値</value>
        public float SEVolume
        {
            get => _seVolume;
            set
            {
                if (_seVolume + Diff < value || _seVolume - Diff > value)
                {
                    SEVolumeChanged.Invoke(value);
                    _seVolume = value;
                }
            }
        }

        public float VoiceVolume
        {
            get => _voiceVolume;
            set
            {
                if (_voiceVolume + Diff < value || _voiceVolume - Diff > value)
                {
                    VoiceVolumeChanged.Invoke(value);
                    _voiceVolume = value;
                }
            }
        }

        /// <summary>SEのPlayerとPlaback</summary>
        private struct CriPlayerData
        {
            private CriAtomExPlayback _playback;
            private CriAtomEx.CueInfo _cueInfo;


            public CriAtomExPlayback Playback
            {
                get => _playback;
                set => _playback = value;
            }

            public CriAtomEx.CueInfo CueInfo
            {
                get => _cueInfo;
                set => _cueInfo = value;
            }

            public bool IsLoop
            {
                get => _cueInfo.length < 0;
            }
        }

        private CriAudioManager()
        {
            MasterVolumeChanged += volume =>
            {
                _bgmPlayer.SetVolume(volume * _bgmVolume);
                _bgmPlayer.Update(_bgmPlayback);

                for (int i = 0; i < _seData.Count; i++)
                {
                    if (_seData[i].IsLoop)
                    {
                        _loopSEPlayer.SetVolume(volume * _seVolume);
                        _loopSEPlayer.Update(_seData[i].Playback);
                    }
                    else
                    {
                        _sePlayer.SetVolume(volume * _seVolume);
                        _sePlayer.Update(_seData[i].Playback);
                    }
                }

                for (int i = 0; i < _voiceData.Count; i++)
                {
                    _voicePlayer.SetVolume(_masterVolume * volume);
                    _voicePlayer.Update(_voiceData[i].Playback);
                }
            };

            BGMVolumeChanged += volume =>
            {
                _bgmPlayer.SetVolume(_masterVolume * volume);
                _bgmPlayer.Update(_bgmPlayback);
            };

            SEVolumeChanged += volume =>
            {
                for (int i = 0; i < _seData.Count; i++)
                {
                    if (_seData[i].IsLoop)
                    {
                        _loopSEPlayer.SetVolume(_masterVolume * volume);
                        _loopSEPlayer.Update(_seData[i].Playback);
                    }
                    else
                    {
                        _sePlayer.SetVolume(_masterVolume * volume);
                        _sePlayer.Update(_seData[i].Playback);
                    }
                }
            };

            VoiceVolumeChanged += volume =>
            {
                for (int i = 0; i < _voiceData.Count; i++)
                {
                    _voicePlayer.SetVolume(_masterVolume * volume);
                    _voicePlayer.Update(_voiceData[i].Playback);
                }
            };

            SceneManager.sceneUnloaded += Unload;
        }

        ~CriAudioManager()
        {
            SceneManager.sceneUnloaded -= Unload;
        }
        // ここに音を鳴らす関数を書いてください

        /// <summary>BGMを開始する</summary>
        /// <param name="cueSheet">流したいキューシートの名前</param>
        /// <param name="cueName">流したいキューの名前</param>
        public void PlayBGM(CueSheet cueSheet, string cueName)
        {
            string cueSheetName = GetCueSheetString(cueSheet);
            if (cueSheetName == null)
            {
                Debug.LogWarning("CueSheetがNullです。");
                return;
            }
        
            var temp = CriAtom.GetCueSheet(cueSheetName).acb;

            if (_currentBGMAcb == temp && _currentBGMCueName == cueName &&
                _bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                return;
            }

            StopBGM();

            _bgmPlayer.SetCue(temp, cueName);
            _bgmPlayback = _bgmPlayer.Start();
            _currentBGMAcb = temp;
            _currentBGMCueName = cueName;
        }

        /// <summary>BGMを中断させる</summary>
        public void PauseBGM()
        {
            if (_bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                _bgmPlayer.Pause();
            }
        }

        /// <summary>中断したBGMを再開させる</summary>
        public void ResumeBGM()
        {
            _bgmPlayer.Resume(CriAtomEx.ResumeMode.PausedPlayback);
        }

        /// <summary>BGMを停止させる</summary>
        public void StopBGM()
        {
            if (_bgmPlayer.GetStatus() == CriAtomExPlayer.Status.Playing)
            {
                _bgmPlayer.Stop();
            }
        }

        /// <summary>SEを流す関数</summary>
        /// <param name="cueSheet">流したいキューシートの名前</param>
        /// <param name="cueName">流したいキューの名前</param>
        /// <returns>停止する際に必要なIndex</returns>
        public int PlaySE(CueSheet cueSheet, string cueName, float volume = 1f)
        {
            CriAtomEx.CueInfo cueInfo;
            CriPlayerData newAtomPlayer = new CriPlayerData();
        
            string cueSheetName = GetCueSheetString(cueSheet);
            if (cueSheetName == null)
            {
                Debug.LogWarning("CueSheetがNullです。");
                return -1;
            }

            var tempAcb = CriAtom.GetCueSheet(cueSheetName).acb;
            tempAcb.GetCueInfo(cueName, out cueInfo);

            newAtomPlayer.CueInfo = cueInfo;

            if (newAtomPlayer.IsLoop)
            {
                _loopSEPlayer.SetCue(tempAcb, cueName);
                _loopSEPlayer.SetVolume(volume * _seVolume * _masterVolume);
                newAtomPlayer.Playback = _loopSEPlayer.Start();
            }
            else
            {
                _sePlayer.SetCue(tempAcb, cueName);
                _sePlayer.SetVolume(volume * _seVolume * _masterVolume);
                newAtomPlayer.Playback = _sePlayer.Start();
            }

            _seData.Add(newAtomPlayer);
            return _seData.Count - 1;
        }

        /// <summary>SEをPauseさせる </summary>
        /// <param name="index">一時停止させたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void PauseSE(int index)
        {
            if (index < 0) return;

            _seData[index].Playback.Pause();
        }

        /// <summary>PauseさせたSEを再開させる</summary>
        /// <param name="index">再開させたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void ResumeSE(int index)
        {
            if (index < 0) return;

            _seData[index].Playback.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }

        /// <summary>SEを停止させる </summary>
        /// <param name="index">止めたいPlaySE()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void StopSE(int index)
        {
            if (index < 0) return;

            _seData[index].Playback.Stop();
        }

        /// <summary>ループしているすべてのSEを止める</summary>
        public void StopLoopSE()
        {
            _loopSEPlayer.Stop();
        }

        /// <summary>Voiceを流す関数</summary>
        /// <param name="cueSheet">流したいキューシートの名前</param>
        /// <param name="cueName">流したいキューの名前</param>
        /// <returns>停止する際に必要なIndex</returns>
        public int PlayVoice(CueSheet cueSheet, string cueName, float volume = 1f)
        {
            CriAtomEx.CueInfo cueInfo;
            CriPlayerData newAtomPlayer = new CriPlayerData();
        
            string cueSheetName = GetCueSheetString(cueSheet);
            if (cueSheetName == null)
            {
                Debug.LogWarning("CueSheetがNullです。");
                return -1;
            }

            var tempAcb = CriAtom.GetCueSheet(cueSheetName).acb;
            tempAcb.GetCueInfo(cueName, out cueInfo);

            newAtomPlayer.CueInfo = cueInfo;

            _voicePlayer.SetCue(tempAcb, cueName);
            _voicePlayer.SetVolume(volume * _masterVolume * _voiceVolume);
            newAtomPlayer.Playback = _voicePlayer.Start();

            _voiceData.Add(newAtomPlayer);
            return _voiceData.Count - 1;
        }

        /// <summary>VoiceをPauseさせる </summary>
        /// <param name="index">一時停止させたいPlayVoice()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void PauseVoice(int index)
        {
            if (index < 0) return;

            _voiceData[index].Playback.Pause();
        }

        /// <summary>PauseさせたVoiceを再開させる</summary>
        /// <param name="index">再開させたいPlayVoice()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void ResumeVoice(int index)
        {
            if (index < 0) return;

            _voiceData[index].Playback.Resume(CriAtomEx.ResumeMode.AllPlayback);
        }

        /// <summary>Voiceを停止させる </summary>
        /// <param name="index">止めたいPlayVoice()の戻り値 (-1以下を渡すと処理を行わない)</param>
        public void StopVoice(int index)
        {
            if (index < 0) return;

            _voiceData[index].Playback.Stop();
        }

        private void Unload(Scene scene)
        {
            StopLoopSE();

            var removeIndex = new List<int>();
            for (int i = _seData.Count - 1; i >= 0; i--)
            {
                if (_seData[i].Playback.GetStatus() == CriAtomExPlayback.Status.Removed)
                {
                    removeIndex.Add(i);
                }
            }

            foreach (var i in removeIndex)
            {
                _seData.RemoveAt(i);
            }
        }
    }
}