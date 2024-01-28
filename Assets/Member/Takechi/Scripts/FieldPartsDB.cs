using UnityEngine;

namespace SoulRun
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FieldPartsDB")]
    public class FieldPartsDB : ScriptableObject
    {
        [SerializeField] [Tooltip("フィールドの部品リスト")]
        FieldParts[] _fieldParts;
        /// <summary>フィールドの部品リスト</summary>
        public FieldParts[] FieldParts => _fieldParts;
    }
}