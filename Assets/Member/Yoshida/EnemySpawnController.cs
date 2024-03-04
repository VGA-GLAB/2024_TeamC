using SoulRunProject.Common;
using SoulRunProject.InGame.SpawnPattern;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] FieldEntityController _fieldEntity;
        [SerializeField] int _spawnCount;
        [SerializeReference, SubclassSelector] ISpawnPattern _spawnPattern;
        [SerializeField] float _spawnerEnableRange;

        void Start()
        {
            var collider = GetComponent<SphereCollider>();
            if (!collider.isTrigger)
            {
                Debug.LogError($"{gameObject.name} の SphereCollider が IsTrigger に設定されていません");
            }
            collider.radius = _spawnerEnableRange;
            
            foreach (var pos in _spawnPattern.GetSpawnPositions(_spawnCount))
            {
                Debug.Log(pos);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
            }
        }

        // bool CheckPlayerDistance(Transform playerTrans)
        // {
        //     return true;
        // }
    }
}