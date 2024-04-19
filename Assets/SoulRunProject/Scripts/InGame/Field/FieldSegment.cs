using UnityEditor;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     Field Moverで道を扱うためのクラス
    /// </summary>
    public class FieldSegment : MonoBehaviour
    {
        [SerializeField, Header("タイルの始点")] Vector3 _startPos;
        [SerializeField, Header("タイルの終点")] Vector3 _endPos;
        int _originalInstanceID;

        public void ApplyOriginalInstanceID(FieldSegment fieldSegment)
        {
            _originalInstanceID = fieldSegment.GetInstanceID();
        }

        public int OriginalInstanceID => _originalInstanceID;
        public Vector3 StartPos
        {
            get => _startPos;
            set => _startPos = value;
        }
        public Vector3 EndPos
        {
            get => _endPos;
            set => _endPos = value;
        }
    }

    [CustomEditor(typeof(FieldSegment))]
    public class FieldSegmentEditor : UnityEditor.Editor
    {
        FieldSegment _fieldSegment;

        void OnEnable()
        {
            _fieldSegment = target as FieldSegment;
        }

        void OnSceneGUI()
        {
            //  ローカル座標をワールド座標に変換してくれる
            using (new Handles.DrawingScope(_fieldSegment.transform.localToWorldMatrix))
            {
                EditorGUI.BeginChangeCheck();
                var newStartPos = Handles.PositionHandle(_fieldSegment.StartPos, Quaternion.identity);
                Handles.Label(_fieldSegment.StartPos, "始点");
                var newEndPos = Handles.PositionHandle(_fieldSegment.EndPos, Quaternion.identity);
                Handles.Label(_fieldSegment.EndPos, "終点");
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Anchor Position");
                    _fieldSegment.StartPos = newStartPos;
                    _fieldSegment.EndPos = newEndPos;
                }
            }
        }
    }
}