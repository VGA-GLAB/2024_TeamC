using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// soulBulletを生成するFactoryメソッド
    /// </summary>
    public class SoulBulletFactory : AbstractFactory<SoulBulletObject>
    {
        GameObjectPoolForUnity _objectPoolForUnity;
        InGameManager _gameManager;

        public SoulBulletFactory(GameObjectPoolForUnity objectPoolForUnity, GameObject instantiateObj, InGameManager inGameManager, int capacity = 10) : base(objectPoolForUnity, instantiateObj, capacity)
        {
            _objectPoolForUnity = objectPoolForUnity;
            _gameManager = inGameManager;
        }

        public override void OnCreateMethod(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<SoulBulletObject>(out var soulBulletObject))
            {
                soulBulletObject.OnDeath += () => _objectPoolForUnity.ReturnObject(gameObject);
                _gameManager.CurrentState.Where(state => state == InGameManager.GameState.Playing)
                    .Subscribe(_ => soulBulletObject.Active()).AddTo(_gameManager);

                _gameManager.CurrentState.Where(state => state == InGameManager.GameState.Gacha)
                    .Subscribe(_ => soulBulletObject.Pause()).AddTo(_gameManager);
            }

        }

        public override void OnReturnMethod(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<SoulBulletObject>(out var soulBulletObject))
            {
                soulBulletObject.OnDeath -= () => _objectPoolForUnity.ReturnObject(gameObject);
            }
        }
    }
}
