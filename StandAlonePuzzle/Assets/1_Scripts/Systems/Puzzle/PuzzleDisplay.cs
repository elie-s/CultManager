using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;


namespace CultManager
{
    public class PuzzleDisplay : MonoBehaviour
    {
        [SerializeField] private PuzzleData data = default;
        [SerializeField] private GameObject segmentPrefab = default;
        [SerializeField] private GameObject nodePrefab = default;
        [SerializeField] private Transform parent = default;

        private SegmentBehaviour[] segments;
        private GameObject[] nodes;

        public void DisplayPuzzle(float _scale)
        {
            ResetNodes();
            ResetSegments();

            segments = new SegmentBehaviour[data.puzzle.Count];
            List<GameObject> tmpNodes = new List<GameObject>();
            List<Node> instantiatedNodes = new List<Node>();

            _scale = _scale * (data.puzzle.Count < 4 ? 4 : (data.puzzle.Count < 30 ? 2 : 1));

            for (int i = 0; i < data.puzzle.Count; i++)
            {
                //Debug.Log(i+"/"+segments.Length);
                segments[i] = Instantiate(segmentPrefab, Node.WorldPosition(data.puzzle[i].b, _scale) + (Vector2)parent.position, Quaternion.identity, parent).GetComponent<SegmentBehaviour>();
                segments[i].Init(data.puzzle[i], _scale);

                if (nodePrefab)
                {
                    if (!instantiatedNodes.Contains(data.puzzle[i].a))
                    {
                        tmpNodes.Add(Instantiate(nodePrefab, Node.WorldPosition(data.puzzle[i].a, _scale) + (Vector2)parent.position, Quaternion.identity, parent));
                        instantiatedNodes.Add(data.puzzle[i].a);
                    }

                    if (!instantiatedNodes.Contains(data.puzzle[i].b))
                    {
                        tmpNodes.Add(Instantiate(nodePrefab, Node.WorldPosition(data.puzzle[i].b, _scale) + (Vector2)parent.position, Quaternion.identity, parent));
                        instantiatedNodes.Add(data.puzzle[i].b);
                    }
                }

            }

            nodes = tmpNodes.ToArray();
        }

        public void DisplayPuzzle(float _scale, List<PuzzleSegment> _data)
        {
            ResetNodes();
            ResetSegments();

            segments = new SegmentBehaviour[_data.Count];
            List<GameObject> tmpNodes = new List<GameObject>();
            List<Node> instantiatedNodes = new List<Node>();

            _scale = _scale * (_data.Count < 4 ? 4 : (_data.Count < 30 ? 2 : 1));

            for (int i = 0; i < _data.Count; i++)
            {
                //Debug.Log(i+"/"+segments.Length);
                segments[i] = Instantiate(segmentPrefab, Node.WorldPosition(_data[i].b, _scale) + (Vector2)parent.position, Quaternion.identity, parent).GetComponent<SegmentBehaviour>();
                segments[i].Init(_data[i], _scale);

                if (nodePrefab)
                {
                    if (!instantiatedNodes.Contains(_data[i].a))
                    {
                        tmpNodes.Add(Instantiate(nodePrefab, Node.WorldPosition(_data[i].a, _scale) + (Vector2)parent.position, Quaternion.identity, parent));
                        instantiatedNodes.Add(_data[i].a);
                    }

                    if (!instantiatedNodes.Contains(_data[i].b))
                    {
                        tmpNodes.Add(Instantiate(nodePrefab, Node.WorldPosition(_data[i].b, _scale) + (Vector2)parent.position, Quaternion.identity, parent));
                        instantiatedNodes.Add(_data[i].b);
                    }
                }

            }

            nodes = tmpNodes.ToArray();
        }

        public void HighlightShape(Segment[] _shape)
        {
            Debug.Log("Highlighted");
            foreach (SegmentBehaviour segment in segments)
            {
                segment.LocalSelect(false);

                for (int i = 0; i < _shape.Length; i++)
                {
                    if(segment.segment.IsSegment(_shape[i]))
                    {
                        segment.LocalSelect(true);
                        break;
                    }
                }
            }
        }

        public void ResetSegments()
        {
            if (segments == null) return;

            foreach (SegmentBehaviour segment in segments)
            {
                Destroy(segment.gameObject);
            }
        }

        public void ResetNodes()
        {
            if (nodes == null) return;

            foreach (GameObject gameObject in nodes)
            {
                Destroy(gameObject);
            }
        }

        public void UnselectAll()
        {
            foreach (SegmentBehaviour segment in segments)
            {
                segment.Select(false);
            }
        }
    }
}