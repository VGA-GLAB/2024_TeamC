using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using GraphProcessor;
using SoulRunProject.Common;
using SoulRunProject.InGame;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SoulRunProject.EditorExtension
{
    [NodeCustomEditor(typeof(FieldSegmentNode))]
    public class FieldSegmentNodeView : BaseNodeView
    {
        public override void Enable()
        {
            var fieldSegmentNode = nodeTarget as FieldSegmentNode;

            var objectField = new ObjectField()
            {
                objectType = typeof(FieldSegment),
                allowSceneObjects = false,
                value = fieldSegmentNode.FieldSegment,
            };
            var loopSelfToggle = new Toggle("Loop Self")
            {
                value = fieldSegmentNode.LoopSelf
            };
            var notRandomInstantiateToggle = new Toggle("Not Random Instantiate")
            {
                value = fieldSegmentNode.NotRandomInstantiate
            };

            var preview = new Image();

            objectField.RegisterValueChangedCallback(v => {
                fieldSegmentNode.FieldSegment = v.newValue as FieldSegment;
                UpdatePreviewImage(preview, v.newValue);
            });
            loopSelfToggle.RegisterValueChangedCallback(v =>
            {
                fieldSegmentNode.LoopSelf = v.newValue;
            });
            notRandomInstantiateToggle.RegisterValueChangedCallback(v =>
            {
                fieldSegmentNode.NotRandomInstantiate = v.newValue;
            });
            //  ウィンドウを開いている間1秒に一回プレビューの描画を実行する
            schedule.Execute(() =>
            {
                if (fieldSegmentNode.FieldSegment != null)
                {
                    UpdatePreviewImage(preview, fieldSegmentNode.FieldSegment.gameObject);
                }
            }).Every(1000);

            controlsContainer.Add(loopSelfToggle);
            controlsContainer.Add(notRandomInstantiateToggle);
            controlsContainer.Add(objectField);
            controlsContainer.Add(preview);
            inputPortViews.First(port=>port.portType == typeof(InToOutPort)).portColor = Color.blue;
            outputPortViews.First(port=>port.portType == typeof(InToOutPort)).portColor = Color.red;
        }
        void UpdatePreviewImage(Image image, Object obj)
        {
            image.image = AssetPreview.GetAssetPreview(obj) ?? AssetPreview.GetMiniThumbnail(obj);
        }
    }
}