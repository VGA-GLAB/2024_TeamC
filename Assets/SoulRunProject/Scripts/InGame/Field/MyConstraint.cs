using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Targetsに追加したオブジェクトを自分の子オブジェクトにした時のように連動して動かすためのクラス
    /// </summary>
    public class MyConstraint : MonoBehaviour
    {
        List<Transform> _targets = new();
        Vector3 _prevPosition;
        public List<Transform> Targets => _targets;
        void Awake()
        {
            _prevPosition = transform.position;
            this.ObserveEveryValueChanged(self => self.transform.position)
                .Subscribe(newPosition =>
                {
                    var offset = newPosition - _prevPosition;
                    foreach (var target in _targets)
                    {
                        if (target != null)
                        {
                            target.position += offset;
                        }
                    }

                    _prevPosition = newPosition;
                }).AddTo(this);
        }
    }
}
