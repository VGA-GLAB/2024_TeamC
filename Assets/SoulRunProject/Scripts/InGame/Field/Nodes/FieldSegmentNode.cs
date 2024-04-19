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
        
        [Input(name = "In", allowMultiple = true), SerializeField]
        public InToOutPort In;
        
        [Output(name = "Out", allowMultiple = true), SerializeField]
        public InToOutPort Out;

        public override string name => "Field Segment";
        public HashSet<FieldSegment> OutSegments { get; set; } = new();

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
        
        protected override void Process()
        {
            OutSegments.Clear();
            if (LoopSelf)
            {
                OutSegments.Add(_fieldSegment);
            }
            foreach (var fieldSegment in outputPorts.First(port=>port.portData.displayType == typeof(InToOutPort))
                         .GetEdges().Select(edge=>edge.inputNode)
                         .OfType<FieldSegmentNode>()
                         .Select(node=>node.FieldSegment))
            {
                OutSegments.Add(fieldSegment);
            }
        }
    }

    public struct InToOutPort { }
}