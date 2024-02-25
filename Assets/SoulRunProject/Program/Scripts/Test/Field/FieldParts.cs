using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     Field Moverで道を扱うためのスクリプト
    /// </summary>
    public class FieldParts : MonoBehaviour
    {
        [SerializeField] [Tooltip("パーツの終点となる場所のアンカー")]
        Transform _endAnchor;

        /// <summary>パーツの終点となる場所のアンカー</summary>
        public Transform EndAnchor => _endAnchor;
    }
}