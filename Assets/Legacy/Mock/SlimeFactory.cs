using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace SoulRun.InGame
{
    public class SlimeFactory : MonoBehaviour
    {
        [SerializeField] NumRange range;
        [SerializeField] GameObject _slime;
        [SerializeField] float _time;
        [SerializeField] GameObject _skull;
        [SerializeField] NumRange range2;
        [SerializeField] float _time2;
        [SerializeField] Vector3 _pos;

        private void Start()
        {
            Observable.Interval(System.TimeSpan.FromSeconds(_time))
                .Subscribe(_ =>
                {
                    var pos = Random.Range(range.Min, range.Max);
                    Instantiate(_slime, new Vector3(pos, _pos.y, _pos.z), Quaternion.identity);
                })
                .AddTo(this);
            
            //Observable.Interval(System.TimeSpan.FromSeconds(_time2))
            //    .Subscribe(_ =>
            //    {
            //        var pos = Random.Range(range.Min, range.Max);
            //        var posy = Random.Range(range2.Min, range2.Max);
            //        Instantiate(_skull, new Vector3(pos, posy, _pos.z), Quaternion.identity);
            //    })
            //    .AddTo(this);
        }
    }
}
