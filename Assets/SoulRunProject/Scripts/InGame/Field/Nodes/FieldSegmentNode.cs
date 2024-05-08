using System;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    [NodeMenuItem("Field Segment", typeof(AdjacentGraph))]
    public class FieldSegmentNode : BaseNode
    {
        [SerializeField] FieldSegment _fieldSegment;
        [SerializeField] bool _loopSelf;
        [SerializeField] private bool _notRandomRandomInstantiate;
        
        [Input(name = "In", allowMultiple = true), SerializeField]
        public InToOutPort In;
        
        [Output(name = "Out", allowMultiple = true), SerializeField]
        public InToOutPort Out;

        public override string name => "Field Segment";
        public HashSet<FieldSegmentNode> OutSegments { get; set; } = new();

        public bool LoopSelf
        {
            get => _loopSelf;
            set => _loopSelf = value;
        }

        public FieldSegment FieldSegment 
        {
            get => _fieldSegment;
            set => _fieldSegment = value;
        }

        public bool NotRandomInstantiate
        {
            get => _notRandomRandomInstantiate;
            set => _notRandomRandomInstantiate = value;
        }

        protected override void Process()
        {
            OutSegments.Clear();
            if (LoopSelf)
            {
                OutSegments.Add(this);
            }
            foreach (var fieldSegmentNode in outputPorts.First(port=>port.portData.displayType == typeof(InToOutPort))
                         .GetEdges().Select(edge=>edge.inputNode)
                         .OfType<FieldSegmentNode>())
            {
                OutSegments.Add(fieldSegmentNode);
            }
        }
    }

    public struct InToOutPort { }
}