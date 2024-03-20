using System.Collections.Generic;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 衝突情報の基底クラス
    /// </summary>
    public abstract class ColliderBase : MonoBehaviour
    {
        [SerializeField] bool _isTrigger;
        [SerializeField] bool _isKinematic;
        public bool IsTrigger => _isTrigger;
        public bool IsKinematic => _isKinematic;
        public readonly Subject<ColliderBase> Enter = new();
        public readonly Subject<ColliderBase> Exit = new();
        public readonly HashSet<ColliderBase> Contacts = new();
        static bool _isQuitting;    //  再生終了は全オブジェクト共通なのでstaticにした。

        void Awake()
        {
            Enter.Subscribe(c=>Debug.Log(gameObject.name + "が" + c.gameObject.name + "と衝突した。")).AddTo(this);
            Exit.Subscribe(c=>Debug.Log(gameObject.name + "が" + c.gameObject.name + "から離れた。")).AddTo(this);
        }

        void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        void OnEnable()
        {
            //  アクティブになった時、マネージャークラスに自身を追加する。
            CollisionManager.Instance.Colliders.Add(this);
        }
        void OnDisable()
        {
            //  再生終了時、AbstractSingletonMonoBehaviourのgetterの処理で新しくインスタンスが生成されて、
            //  Some objects were not cleaned up when closing the scene. (Did you spawn new GameObjects from OnDestroy?)
            //  というエラーが出るので再生終了時はマネージャークラスにアクセスしないような処理を書いた。
            if (!_isQuitting)
            {
                //  非アクティブ又は破棄する時は、マネージャークラスから自身を破棄する。
                CollisionManager.Instance.Colliders.Remove(this);
            }
        }

        /// <summary>接触判定</summary>
        public abstract bool CheckContacts(ColliderBase other);
    }
}
