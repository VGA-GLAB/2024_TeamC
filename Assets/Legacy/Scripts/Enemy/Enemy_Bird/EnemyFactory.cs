using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 敵を生成するクラス
    /// </summary>
    public class EnemyFactory : AbstractFactory<Enemy>
    {
        private PlayerScoreManager _scoreManager;
        private PlayerExpManager _expManager;

        public EnemyFactory(GameObjectPoolForUnity objectPoolForUnity, GameObject instantiateObj, PlayerScoreManager scoreManager, PlayerExpManager expManager, int capacity = 10)
            : base(objectPoolForUnity, instantiateObj, capacity)
        {
            _scoreManager = scoreManager;
            _expManager = expManager;
        }

        public override void OnCreateMethod(GameObject instantiateObject)
        {
            if (instantiateObject.TryGetComponent<Enemy>(out var enemy))
            {   //死亡時にスコアをスコアマネージャに加算する処理を追加
                enemy.OnDeathMethod += () => _scoreManager.AddScore(enemy.Score);
                enemy.OnDeathMethod += () => _expManager.AddExp(enemy.Exp);
            }
        }

        public override void OnReturnMethod(GameObject returnObject)
        {
            if (returnObject.TryGetComponent<Enemy>(out var enemy))
            {   //死亡時にスコアをスコアマネージャに加算する処理を追加
                enemy.OnReturnMethod -= () => _scoreManager.AddScore(enemy.Score);
                enemy.OnDeathMethod -= () => _expManager.AddExp(enemy.Exp);
            }
        }
    }
}
