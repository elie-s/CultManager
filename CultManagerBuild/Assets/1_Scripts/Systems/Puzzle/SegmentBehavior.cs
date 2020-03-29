using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SegmentBehavior : MonoBehaviour
    {
        public NodeBehavior[] nodes = new NodeBehavior[2];
        public GameObject LineRendererPrefab;

        void Awake()
        {
        }

        [ContextMenu("ActivateSegment")]
        public void ActivateSegment()
        {
            GameObject lineRendererGO = Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity, transform);
            LineRenderer lr = lineRendererGO.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            for (int i = 0; i < nodes.Length; i++)
            {
                lr.SetPosition(i, nodes[i].transform.position);
            }
        }
    }
}

