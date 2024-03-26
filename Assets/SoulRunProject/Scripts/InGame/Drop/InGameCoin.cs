using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class InGameCoin : MonoBehaviour
    {
        [SerializeField] private int _exp;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                //playerManager.(_exp);
                Destroy(gameObject);
            }
        }
    }
}
