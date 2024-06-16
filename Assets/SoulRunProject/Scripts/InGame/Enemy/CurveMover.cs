using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    [Name("カーブ移動")]
    public class CurveMover : EntityMover
    {
        [SerializeField] [CustomLabel("経由地点")] private List<GameObject> _posMarkers;

        [SerializeField] [CustomLabel("終了地点と始点を繋げる")]
        private bool _isPosLoop;

        [SerializeField] [CustomLabel("Loop回数指定")] [Tooltip("-1で無限ループ")]
        private int _loopCount;

        [SerializeField] [CustomLabel("LoopType設定")]
        private LoopType _type;

        private Tweener _tweener;
        private Vector3[] _posArr;

        public override void OnStart(Transform myTransform = null, PlayerManager pm = null)
        {
            if (myTransform == null) return;
            _posArr = _posMarkers.Select(target => target.transform.position).ToArray();
            foreach (var item in _posMarkers) item.SetActive(false);
            _tweener = myTransform.DOLocalPath
                (
                    _posArr,
                    _moveSpeed,
                    PathType.CatmullRom,
                    gizmoColor: Color.red
                )
                .SetOptions
                (
                    false,
                    lockRotation: AxisConstraint.X
                )
                .SetOptions(_isPosLoop)
                .SetLoops(_loopCount, _type)
                .SetEase(Ease.Linear)
                .SetLink(myTransform.gameObject, LinkBehaviour.KillOnDisable)
                .SetLink(myTransform.gameObject);
        }
    }
}