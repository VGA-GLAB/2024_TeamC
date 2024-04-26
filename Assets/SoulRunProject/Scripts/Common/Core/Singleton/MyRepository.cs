using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Common.Core.ScriptableObject;
using SoulRunProject.SoulMixScene;

using UnityEngine;

namespace SoulRunProject.SoulRunProject.Scripts.Common.Core.Singleton
{
    public class MyRepository : AbstractSingletonMonoBehaviour<MyRepository>
    {
        [SerializeField] private RepositoryData _repositoryData;
        protected override bool UseDontDestroyOnLoad { get; } = false;
        
        public bool TryGetDataList<T>(out List<T> dataSet) where T : ScriptableObject
        {
            return _repositoryData.TryGetDataList(out dataSet);
        }

        public bool TryGetData<T>(out T data) where T : ScriptableObject
        {
            return _repositoryData.TryGetData(out data);
        }
    }
}