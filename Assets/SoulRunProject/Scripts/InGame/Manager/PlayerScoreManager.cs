using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace SoulRunProject
{
    public class PlayerScoreManager : AbstractSingletonMonoBehaviour<PlayerScoreManager>
    {
        protected override bool UseDontDestroyOnLoad { get; } = false;
        // Start is called before the first frame update
        
        private IntReactiveProperty _score = new (0);
        public IReadOnlyReactiveProperty<int> OnScoreChanged => _score;

        public void AddScore(int score)
        {
            _score.Value += score;
        }
    }
}
