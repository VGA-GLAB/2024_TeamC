using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class StateMachine
    {
        protected State _currentState;
        protected GameObject _owner;
        protected CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        // ステートの登録や遷移に使用する辞書
        protected Dictionary<int, State> _states = new Dictionary<int, State>();

        public StateMachine() { }

        // ステートの追加
        public void AddState(int id, State state)
        {
            if (!_states.ContainsKey(id))
            {
                _states.Add(id, state);
            }
            else
            {
                DebugClass.Instance.ShowWarningLog("State with the same ID already exists.");
            }
        }

        // ステートの削除
        public void RemoveState(int id)
        {
            if (_states.ContainsKey(id))
            {
                _states.Remove(id);
            }
            else
            {
                DebugClass.Instance.ShowWarningLog("State with the given ID does not exist.");
            }
        }

        // ステートへの遷移（同期）
        public void ChangeState(int id)
        {
            if (_states.ContainsKey(id))
            {
                _currentState?.Exit(_states[id]);
                _currentState = _states[id];
                _currentState.Enter(_currentState);
            }
            else
            {
                DebugClass.Instance.ShowErrorLog("State with the given ID does not exist.");
            }
        }

        // ステートへの遷移（非同期）
        public async UniTask ChangeStateAsync(int id)
        {
            if (_states.ContainsKey(id))
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                var token = _cancellationTokenSource.Token;

                _currentState?.Exit(_states[id]);
                _currentState = _states[id];
                await _currentState.EnterAsync(_currentState, token);
            }
            else
            {
                Debug.LogError("State with the given ID does not exist.");
            }
        }

        // 現在のステートのUpdateを呼び出す
        public void UpdateCurrentState()
        {
            _currentState?.Update();
        }
    }
}
