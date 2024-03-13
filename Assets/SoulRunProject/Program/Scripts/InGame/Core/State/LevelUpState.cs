using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class LevelUpState : State
    {
        private PlayerManager _playerManager;
        
        public LevelUpState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
    }
}
