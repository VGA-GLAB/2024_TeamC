using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using Unity.Jobs;

namespace SoulRunProject.Common
{
    public class AdjacentGraphProcessor : BaseGraphProcessor
    {
        List<BaseNode> _processList;
        List<FieldSegmentNode> _fieldSegmentNodes;
        public List<FieldSegmentNode> FieldSegmentNodes => _fieldSegmentNodes;

        public AdjacentGraphProcessor(BaseGraph graph) : base(graph)
        {
        }

        public override void UpdateComputeOrder()
        {
            _processList = graph.nodes.OrderBy(n => n.computeOrder).ToList();
        }
        
        public override void Run()
        {
            var count = _processList.Count;

            // すべてのノードを順番に処理する
            for (var i = 0; i < count; i++)
            {
                _processList[i].OnProcess();
            }

            JobHandle.ScheduleBatchedJobs();

            _fieldSegmentNodes = _processList.OfType<FieldSegmentNode>().ToList();
        }
    }
}