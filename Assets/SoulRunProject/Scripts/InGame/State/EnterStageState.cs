using SoulRunProject.Common;
using SoulRunProject.Framework;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ステージ開始前の処理を行う
    /// </summary>
    public class EnterStageState : State
    {
        private PlayingRunGameState _playingRunGameState;
        
        public EnterStageState(PlayingRunGameState playingRunGameState)
        {
            _playingRunGameState = playingRunGameState;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ステージ開始ステート開始");
            //TODO：ステージ名の表示
            StateChange();
        }
        
        protected override void OnExit(State nextState)
        {
    
        }
    }
}
