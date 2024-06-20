using GraphProcessor;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace SoulRunProject.Common
{
// CreateメニューからScriptableObjectのアセットを作れるように
    [CreateAssetMenu(menuName = "Adjacent Graph")]
    public class AdjacentGraph : BaseGraph
    {
        AdjacentGraphProcessor _processor;
        public AdjacentGraphProcessor Processor 
        {
            get
            {
                if (_processor == null)
                {
                    _processor = new AdjacentGraphProcessor(this);
                    _processor.Run();
                }

                return _processor;
            }
        }
#if UNITY_EDITOR
        // ダブルクリックでウィンドウが開かれるように
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as AdjacentGraph;

            if (asset == null) return false;
        
            var window = EditorWindow.GetWindow<AdjacentGraphWindow>();
            window.InitializeGraph(asset);
            return true;
        }
#endif
    }
}