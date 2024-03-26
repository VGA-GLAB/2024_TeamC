using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject
{
    public class InGameExp : MonoBehaviour
    {
        [SerializeField] private int _exp;
        private void OnCollisionEnter(Collision other)
        {
            DebugClass.Instance.ShowLog("OnCollisionEnter");
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                playerManager.GetExp(_exp);
                Destroy(gameObject);
            }
        }
    }
}
