using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaPlayerAttack2 : MonoBehaviour
    {
        public GameObject objectPrefab; // 生成するPrefab
        public float spawnInterval = 2f; // 生成間隔（秒）
        [SerializeField] float _generateNum = 100;
        List<Transform> transforms = new List<Transform>();

        // Startメソッドでコルーチンを開始
        void Start()
        {
            StartCoroutine(SpawnObject());
            //Inpact();
        }

        // 指定した間隔でオブジェクトを生成するコルーチン
        IEnumerator SpawnObject()
        {
            while (true)
            {
                if (!AlphaExpAndScoreManager.Instance._paused)
                {
                    var obj = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                    var enemies = FindObjectsByType<TestSlimeMove>(sortMode: FindObjectsSortMode.None);
                    if (enemies.Length > 0)
                    {
                        obj.GetComponent<AlphaMissile>()._target = enemies[0].transform;

                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        //モック用
        void Inpact()
        {
            var target = FindObjectsByType<TestSlimeMove>(sortMode: FindObjectsSortMode.None);

            for (int i = 0; i < target.Length; i++)
            {
                var obj = Instantiate(objectPrefab, transform.position, Quaternion.identity);
                var bullet = obj.GetComponent<AlphaMissile>();
                bullet._target = target[i].transform;

                var num = Random.Range(0, 1f);
                bullet._period += num;
            }
        }
    }
}
