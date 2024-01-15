using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class MockFieldMover : MonoBehaviour
    {
        [SerializeField] List<GameObject> fields = new List<GameObject>();
        [SerializeField] float limit;
        [SerializeField] float originPos;

        // Update is called once per frame
        void FixedUpdate()
        {
            foreach (GameObject field in fields)
            {
                field.transform.position += Vector3.back * Settings.Instance.Speed;

                if (field.transform.position.z < limit)
                {
                    field.transform.position = new Vector3(field.transform.position.x, field.transform.position.y, originPos);
                }

            }
        }
    }
}
