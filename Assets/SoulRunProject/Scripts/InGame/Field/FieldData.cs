using UnityEngine;

namespace SoulRunProject.InGame.Field
{
    /// <summary>
    /// 複数のパーツをこれ1つで管理するためのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "SoulRunProject/Field/FieldData")]
    public class FieldData : ScriptableObject
    {
        [SerializeField, Tooltip("フィールドの部品リスト")]
        FieldPart[] _fieldParts;
        /// <summary>フィールドの部品リスト</summary>
        public FieldPart[] FieldParts => _fieldParts;
    }
}