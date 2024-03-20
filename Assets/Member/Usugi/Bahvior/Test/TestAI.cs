using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class TestAI : MonoBehaviour
    {
        public BehaviorRunner behaviorRunner;
        // Start is called before the first frame update
        void Start()
        {
            behaviorRunner.RunTree();
        }
    }
}
