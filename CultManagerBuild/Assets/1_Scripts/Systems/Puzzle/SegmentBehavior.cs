using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SegmentBehavior : MonoBehaviour
    {
        public bool segmentActive;
        public NodeBehavior[] nodes = new NodeBehavior[2];
        public Token[] tokens = new Token[2];
        public GameObject LineRendererPrefab;

        GameObject lineRendererGO;
        PuzzleManager puzzleManager;

        void Awake()
        {
            puzzleManager = FindObjectOfType<PuzzleManager>();
        }

        void Update()
        {
            CheckSegment();
        }

        void CheckSegment()
        {
            if (!segmentActive)
            {
                int ctr = 0;
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (nodes[i].active && nodes[i].currentNode.token.IncludesToken(tokens[i]))
                    {
                        ctr++;
                    }
                }
                if (ctr == nodes.Length)
                {
                    segmentActive = true;
                    ActivateSegment();
                }
            }
            
        }

        [ContextMenu("ActivateSegment")]
        public void ActivateSegment()
        {
            lineRendererGO = Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity, transform);
            LineRenderer lr = lineRendererGO.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            for (int i = 0; i < nodes.Length; i++)
            {
                lr.SetPosition(i, nodes[i].transform.position);
            }
        }
        [ContextMenu("DeactivateSegment")]
        public void DeactivateSegment()
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

