using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.TakechiTest
{
    public class TestPlayerManager: MonoBehaviour, IPlayerReference
    {
        [SerializeField] Transform _player;
        public Transform Player => _player;
    }
}