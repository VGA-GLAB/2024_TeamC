using UnityEngine;

namespace SoulRunProject.InGame.Field
{
    /// <summary>
    /// Field Moverで道を扱うためのクラス
    /// </summary>
    public class FieldPart : MonoBehaviour
    {
        [SerializeField] [Tooltip("パーツの終点となる場所のアンカー")]
        Transform _endAnchor;

        /// <summary>パーツの終点となる場所のアンカー</summary>
        public Transform EndAnchor => _endAnchor;
    }
}