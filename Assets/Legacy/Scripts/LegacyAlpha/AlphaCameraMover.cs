using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace SoulRun.InGame
{
    public class AlphaCameraMover : MonoBehaviour
    {
        private Vector3 _cameraOffset = new(0, 2f, -5f);
        [SerializeField] private GameObject _player;
        [SerializeField] private float zRotateNum = 10;
        [SerializeField] private float yRotateNum = 10;
        [SerializeField] private float _rotateTime = 0.5f;
        private float _startY;

        // Start is called before the first frame update
        void Start()
        {
            _cameraOffset = Camera.main.transform.localPosition - _player.gameObject.transform.position;
            _startY = _cameraOffset.y;
        }

        private void LateUpdate()
        {
            Camera.main.transform.position = _player.transform.position + _cameraOffset;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, _startY, Camera.main.transform.position.z);
        }

        public void RotateCam(bool isRight)
        {
            if (isRight)
            {
                Camera.main.transform.DORotate(new Vector3(0, -yRotateNum, -zRotateNum), _rotateTime, RotateMode.Fast);
            }
            else
            {
                Camera.main.transform.DORotate(new Vector3(0, yRotateNum, zRotateNum), _rotateTime, RotateMode.Fast);
            }
        }

        public void ResetCamRotate()
        {
            if (Camera.main.transform.rotation.z == 0) return; 
            Camera.main.transform.DORotate(new Vector3(0, 0, 0), _rotateTime, RotateMode.Fast);
        }
    }
}
