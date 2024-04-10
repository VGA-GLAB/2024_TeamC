using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField, Header("回転制限")] private float _crampRotateZ;
        [SerializeField , Header("上昇率")] private float _improveRate;
        [SerializeField , Header("減衰率")] private float _decreaseRate;

        private float _currentRotateZ;
        private void Update()
        {
            float input = Input.GetAxis("Horizontal");
            _currentRotateZ += -input * _improveRate * Time.deltaTime;
            //回転制限
            _currentRotateZ = Mathf.Clamp(_currentRotateZ, -_crampRotateZ, _crampRotateZ);
            //減衰補完
            _currentRotateZ = Mathf.Lerp(_currentRotateZ, 0f, _decreaseRate * Time.deltaTime);

            var myRotation = _cameraLookAt.transform.rotation;
            _cameraLookAt.transform.rotation  = Quaternion.AngleAxis(_currentRotateZ, Vector3.forward);
            
        }
    }
}
