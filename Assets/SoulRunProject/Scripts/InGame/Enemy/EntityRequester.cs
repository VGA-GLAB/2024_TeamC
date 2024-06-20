#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class EntityRequester : MonoBehaviour
    {
        [SerializeField] private DamageableEntity _entity;
        public DamageableEntity Entity => _entity;

        private void Awake()
        {
            if (_entity != null)
            {
                ObjectPoolManager.Instance.RequestInstance(_entity, transform.position, transform.rotation, transform.parent);
            }
            Destroy(gameObject);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0.76f, 0f);
            Gizmos.DrawSphere(transform.position, 1f);
        }
#endif
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(EntityRequester))]
    [CanEditMultipleObjects]
    public class EntityRequesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button($"生成する"))
            {
                foreach (var obj in targets)
                {
                    var requester = obj as EntityRequester;
                    if (requester.Entity != null)
                    {
                        var entity = Instantiate(requester.Entity, requester.transform.position,
                            requester.transform.rotation, requester.transform.root);
                        Undo.RegisterCreatedObjectUndo(entity.gameObject, "create " + entity.gameObject.name);
                    }
                }
            }
            if (GUILayout.Button($"{nameof(EntityRequester)}とTransform以外のコンポーネントを削除"))
            {
                foreach (var obj in targets)
                {
                    foreach (var component in obj.GetComponents<Component>()
                                 .Where(c => HasRequireComponent(c.GetType())))
                    {
                        if(component is EntityRequester or Transform) continue;
                        Undo.DestroyObjectImmediate(component);
                    }
                    foreach (var component in obj.GetComponents<Component>())
                    {
                        if(component is EntityRequester or Transform) continue;
                        Undo.DestroyObjectImmediate(component);
                    }
                }
            }

            if (GUILayout.Button("子オブジェクトを削除"))
            {
                foreach (var obj in targets)
                {
                    var requester = obj as EntityRequester;
                    foreach (Transform child in requester.transform)
                    {
                        Undo.DestroyObjectImmediate(child.gameObject);
                    }
                }
            }
        }

        bool HasRequireComponent(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(RequireComponent), true);
            return attributes.Length > 0;
        }
    }
    #endif
}
