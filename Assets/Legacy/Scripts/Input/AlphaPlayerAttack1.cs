using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaPlayerAttack1 : MonoBehaviour
    {
        [SerializeField] GameObject _bullet;
        [SerializeField] float _bulletDur = 0.5f;
        [SerializeField] float _speed = 10;
        [SerializeField] Vector3 _bulletGeneratePos = Vector3.zero;
        [SerializeField] Vector3 _dir = Vector3.forward;

        // Start is called before the first frame update
        void Start()
        {
            Observable.Interval(System.TimeSpan.FromSeconds(_bulletDur))
                .Subscribe(_ =>
                {
                    var obj = Instantiate(_bullet, transform.position + _bulletGeneratePos, Quaternion.identity);
                    var skill = obj.GetComponent<SoulBulletObject>();
                    skill.SetBulletStatus(_speed, _bulletDur);
                    skill.dir = _dir;
                });
        }
    }
}
