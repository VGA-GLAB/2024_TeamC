using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Framework;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class PauseManager : AbstractSingletonMonoBehaviour<PauseManager>
    {
        protected override bool UseDontDestroyOnLoad { get; } = true;
        private static List<IPausable> _pausables = new();

        public static void AddPausableObject(IPausable pausable)
        {
            _pausables.Add(pausable);
        }
        public static void RemovePausableObject(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public static void Pause(bool isPause)
        {
            foreach (var obj in Object.FindObjectsOfType<GameObject>())
            {
                obj.GetComponents<IPausable>()
                    .ToList().ForEach(x => x.Pause(isPause));
            }
            //_pausables.ForEach(x => Pause(isPause));
        }

        
    }
}