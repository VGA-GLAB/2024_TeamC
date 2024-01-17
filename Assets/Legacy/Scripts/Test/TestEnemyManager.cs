using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class TestEnemyManager : MonoBehaviour
    {
        [SerializeField] PlayerManager playerManager;
        [SerializeField] float distance = 40;
        [SerializeField] List<GameObject> gameObjects = new List<GameObject>();

        private void Start()
        {
            foreach (var item in gameObjects)
            {
                item.SetActive(false);
            }
        }

        private void Update()
        {
            foreach (var item in gameObjects)
            {
                if (Vector3.Distance(item.transform.position, playerManager.transform.position) <= distance)
                {
                    item.SetActive(true);
                } 
             
            }
        }
    }
}
