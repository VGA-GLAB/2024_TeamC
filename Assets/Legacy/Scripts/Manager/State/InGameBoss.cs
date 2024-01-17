using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class InGameBoss : IInGameState
    {
        public event Func<UniTask> OnStateStart;
        public event Func<UniTask> OnStateEnd;
        public event Action<IInGameState> OnStateExit;

        public void OnEnterState()
        {
            throw new NotImplementedException();
        }

        public void OnExitState()
        {
            throw new NotImplementedException();
        }

        public void OnUpdateState()
        {
            throw new NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
