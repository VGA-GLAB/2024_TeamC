using Cysharp.Threading.Tasks;
using SoulRunProject.Framework;
using SoulRunProject.InGame;
using UnityEngine;
using VContainer.Unity;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲームの進行を管理するクラス
    /// </summary>
    public class InGameManager : StateMachine, IStartable, ITickable
    {

        public InGameManager( GameObject owner, 
            EnterInGameState firstState,
            EnterStageState enterStageState,
            PlayingRunGameState playingRunGameState,
            GameOverState gameOverState,
            EnterBossStageState enterBossStageState,
            PlayingBossStageState playingBossStageState,
            GameClearState gameClearState,
            PauseState pauseState,
            LevelUpState levelUpState)
        {   //ステートの追加、遷移処理の設定を行う。
            _currentState = firstState;
            _owner = owner;
            AddState(0, firstState);
            AddState(1, enterStageState);
            AddState(2, playingRunGameState);
            AddState(3, gameOverState);
            AddState(4, enterBossStageState);
            AddState(5, playingBossStageState);
            AddState(6, gameClearState);
            AddState(7, pauseState);
            AddState(8, levelUpState);
            firstState.OnStateExit += _ => ChangeState(1);
            enterStageState.OnStateExit += _ => ChangeState(2);
            playingRunGameState.OnStateExit += _ =>
            {   
                if (playingRunGameState.ArrivedBossStagePosition) //Playerが生きていて、ボスステージに移行する場合
                    ChangeState(4);
                else if (playingRunGameState.SwitchToPauseState) // PauseStateへの移行
                {
                    ChangeState(7);
                    pauseState.StateToReturn = 2; // 同じStateに戻るための登録
                }
                else if (playingRunGameState.SwitchToLevelUpState)
                {
                    ChangeState(8);
                    levelUpState.StateToReturn = 2; // 同じStateに戻るための登録
                }
            };
            pauseState.OnStateExit += _ => ChangeState(pauseState.StateToReturn);
            levelUpState.OnStateExit += _ => ChangeState(levelUpState.StateToReturn);
            enterBossStageState.OnStateExit += _ => ChangeState(5);
            playingBossStageState.OnStateExit += state =>
            {
                if (playingBossStageState.IsBossDefeated) //ボスを倒した場合
                    ChangeState(6);
                // else if (playingBossStageState.IsPlayerDead) //プレイヤーが死んだ場合
                //     ChangeState(3);
                //プレイヤーが死んだ場合
            };
        }

        public void Start()
        {
            DebugClass.Instance.ShowLog("InGameManager起動");
            var token = _owner.GetCancellationTokenOnDestroy();
            _currentState.EnterAsync(null, token).Forget();
        }
        
        public void Tick()
        {
            _currentState.Update();
        }
    }
}
