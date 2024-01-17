using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SoulRun.InGame
{
    /// <summary>
    /// インゲーム移行時
    /// 自身の起動,管理しているクラスの起動,ゲームスタート前の演出
    /// ゲームスタート(プレイヤー動き出す)
    /// ステージ開始演出,ポーズ処理,ボス戦前演出,ボス戦後演出
    /// ステージ切り替え演出,死亡時リザルト移行処理
    ///                                                                     　死亡　-----------------------------------------------------------------------------------------------------
    ///                                                                        ↑                                        ↑(死亡)                                                       ↓
    /// ゲームの流れ：開始演出(実行：イベント)　→　ゲーム開始(実行：リアクティブプロパティ　プレイヤーとか動き出す)　→　ゲーム中→　ボス演出(開始)　→　ボス戦　→　ボス終了　→　リザルト演出
    ///                                                                                                                   ↓　　↑
    ///                                                                                                                  ポーズ処理
    /// </summary>
    public class InGameManager : MonoBehaviour
    {
        public enum GameState
        {
            None,
            Initializing,
            Playing,
            Paused,
            Gacha,
            BossStage,
            Result,
        }

        [Header("GameMasterSetting")]
        [Tooltip("GameStartが呼ばれてからプレイヤーが動けるまでの時間を設定")] 
        [SerializeField] private float waitToGameStartTime = 3.0f;
        [Tooltip("InGameが終わってからResultSceneに遷移するまでの時間を設定")]
        [SerializeField] private float waitToGameEndTime = 3.0f;
        [Tooltip("InGameから遷移するシーンの名前を設定")]
        [SerializeField] private string inGameToResult = "Result";

        /// <summary> ゲームの一時停止状態を管理するプライベートリアクティブプロパティ </summary>
        private readonly ReactiveProperty<bool> _isGamePaused = new ReactiveProperty<bool>(false);
        /// <summary> ゲームの一時停止状態を読み取り専用で提供するプロパティ </summary>
        public IReadOnlyReactiveProperty<bool> IsGamePaused => _isGamePaused;
        /// <summary> ゲームの現在の状態を管理するプライベートリアクティブプロパティ </summary>
        private readonly ReactiveProperty<GameState> _currentState = new ReactiveProperty<GameState>(GameState.None);
        /// <summary> ゲームの現在の状態を読み取り専用で提供するプロパティ </summary>
        public IReadOnlyReactiveProperty<GameState> CurrentState => _currentState;

        public event Func<UniTask> OnGameStart;
        public event Func<UniTask> OnGacha;
        public event Func<UniTask> OnEnterBossStage;
        public event Func<UniTask> OnExitBossStage;
        public event Func<UniTask> OnStageClear;
        public event Func<UniTask> OnStagefailed;
        public event Func<UniTask> OnGameEnd;

        //[Header("Audio")]
        //[Tooltip("ゲームスタートしてからのBGMの名前")]
        //[SerializeField] private string inGameAudioBgmName;
        //[Tooltip("GameClearのBGMの名前")]
        //[SerializeField] private string inGameClearAudioBgmName;
        //[Tooltip("GameOverのBGMの名前")]
        //[SerializeField] private string inGameOverAudioBgmName;
        //[Tooltip("SEの名前")]
        //[SerializeField] private string inGameAudioSeName;

        private void Awake()
        {
            CurrentState.Subscribe(state => Debug.Log(state));
            Initialize();
        }

        /// <summary>初期化処理 </summary>
        private void Initialize()
        {
            ChangeState(GameState.Initializing);
            Application.targetFrameRate = 60;
            _isGamePaused.Value = false;
        }

        /// <summary>GameStateのステートを変更する </summary>
        /// <param name="newState">GameState</param>
        public void ChangeState(GameState newState)
        {
            // ステートを変更するためのメソッド
            _currentState.Value = newState;
        }

        private void Start()
        {
            GameStart().Forget();
        }

        /// <summary>ゲームをスタートします。 </summary>
        public async UniTask GameStart()
        {
            if (OnGameStart != null) await OnGameStart();
            ChangeState(GameState.Playing);//プレイヤーを動けるようにする
        }

        /// <summary> ゲームを一時停止または再開します。 </summary>
        /// <param name="isPaused">ゲームを一時停止する場合は true、再開する場合は false</param>
        public void GamePause(bool isPaused)
        {
            _currentState.Value = isPaused ? GameState.Paused : GameState.Playing;
        }

        public async void GamePauseEvent()
        {

        }

        /// <summary> ガチャを実行する </summary>
        public async void DoGahcaEvent()
        {
            _currentState.Value = GameState.Gacha;
            if (OnGacha != null) await OnGacha();
            ChangeState(GameState.Playing);
        }

        /// <summary> ボスの召喚演出 </summary>
        public async UniTask BossStart()
        {
            if (OnEnterBossStage != null) await OnEnterBossStage();
            ChangeState(GameState.BossStage);
        }
        /// <summary> ボスの撃破演出 </summary>
        public async UniTask BossEnd()
        {
            if (OnExitBossStage != null) await OnExitBossStage();
            ChangeState(GameState.Result);
        }

        /// <summary> ゲームオーバーの演出 </summary>
        public async void GameOver()
        {
            if (OnStagefailed != null) await OnStagefailed();
            GameEnd();
        }

        /// <summary> ゲームクリア演出 </summary>
        public async void GameClear()
        {
            if (OnStageClear != null) await OnStageClear();
            GameEnd();
        }

        /// <summary> ゲームを終わり、Resultを表示 </summary>
        private void GameEnd()
        {
            OnGameEnd?.Invoke();
        }

        public void ExitInGame()
        {
            SceneManager.LoadScene(inGameToResult);
        }
    }
}