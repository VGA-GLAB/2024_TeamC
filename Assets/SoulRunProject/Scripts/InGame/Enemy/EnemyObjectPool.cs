using UnityEngine;

namespace SoulRunProject.Common
{
    public class EnemyObjectPool : AbstractSingletonMonoBehaviour<EnemyObjectPool>
    {
        [SerializeField] int _poolMaxSize;

        protected override bool UseDontDestroyOnLoad { get; }
        
        public GameObject CreatePoolObject(GameObject bulletObj, Vector3 pos)
        {
            return Instantiate(bulletObj, pos, Quaternion.Euler(-90, 0, 0));
        }

        public GameObject GetPoolObject(GameObject bulletObj, Vector3 pos)
        {
            var trans = transform.Find(bulletObj.name + "(Clone)");
            if (transform.childCount <= 0 || trans == null) return CreatePoolObject(bulletObj, pos);
            trans.parent = null;
            trans.position = pos;
            var obj = trans.gameObject;
            obj.SetActive(true);
            return obj;
        }

        public void ReleasePoolObject(GameObject bulletObj)
        {
            if (transform.childCount > _poolMaxSize)
            {
                Destroy(bulletObj);
                return;
            }

            bulletObj.transform.parent = transform;
            bulletObj.SetActive(false);
        }
    }
}