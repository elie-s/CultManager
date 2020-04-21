using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class HexagonalGrid
    {
        public float scale;
        public List<HexagonalGridNode> nodes;
        public List<HexagonalGridSegment> segments;
        public int layers;

        public HexagonalGrid(float _scale, int _layers)
        {
            scale = _scale;
            layers = _layers;

            SetGrid();
        }

        public HexagonalGrid(HexagonalGridPattern _pattern)
        {
            scale = _pattern.grid.scale;
            layers = _pattern.grid.layers;

            nodes = _pattern.nodes;
            segments = _pattern.segments;
        }

        public void SetGrid()
        {
            nodes = new List<HexagonalGridNode>();
            segments = new List<HexagonalGridSegment>();

            nodes.Add(HexagonalGridNode.zero);

            for (int s = 0; s < 6; s++)
            {
                //for (int i = 1; i < layers + 1; i++)
                //{
                //    for (int j = 0; j < i; j++)
                //    {
                //        HexagonalGridNode newNode = HexagonalGridNode.PosFromSlice(new HexagonalGridNode(i, j));
                //        if (!nodes.Contains(newNode)) nodes.Add(newNode);
                //        else Debug.Log("bug !");
                //    }
                //}
                HexagonalGridNode[] sliceNodes = HexagonalGridNode.NodesInSlice((HexagonalGridSlice)s, layers);

                foreach (HexagonalGridNode node in sliceNodes)
                {
                    if (!nodes.Contains(node)) nodes.Add(node);
                     else Debug.Log("bug !");
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                HexagonalGridNode[] neighbours = nodes[i].GetNeighbours();

                for (int j = 0; j < neighbours.Length; j++)
                {
                    HexagonalGridSegment segment = new HexagonalGridSegment(nodes[i], neighbours[j]);
                    if (nodes.Contains(neighbours[j]) && !segments.Contains(segment))
                    {
                        segments.Add(segment);
                    }
                }
            }
        }

        public void SetGrid(int _layers)
        {
            layers = _layers;

            SetGrid();
        }

        public void DoubleGrid()
        {
            SetGrid(layers * 2);
        }

        public Vector2 NodeToWorldPosition(HexagonalGridNode _node)
        {
            Vector2 xAxis = Vector2.right * (_node.x * scale);
            return xAxis - new Vector2(_node.y * scale * Mathf.Cos(-60 * Mathf.Deg2Rad), _node.y * scale * Mathf.Sin(-60 * Mathf.Deg2Rad));
        }

        public HexagonalGridSegment[] SegmentsUsingNode(HexagonalGridNode _node)
        {
            List<HexagonalGridSegment> results = new List<HexagonalGridSegment>();

            foreach (HexagonalGridSegment segment in segments)
            {
                if (segment.ConnectedTo(_node)) results.Add(segment);
            }

            return results.ToArray();
        }

        public HexagonalGridSegment RandomSegmentFromNode(HexagonalGridNode _node)
        {
            HexagonalGridSegment[] segments = SegmentsUsingNode(_node);

            return segments[Random.Range(0, segments.Length)];
        }

        public HexagonalGridNode GetRandomNode()
        {
            return nodes[Random.Range(0, nodes.Count)];
        }

        public HexagonalGridNode GetVerticalAxialSymmetry(HexagonalGridNode _node)
        {
            int x = 0;
            float middle = (float)_node.y / 2.0f;

            float difference = Mathf.Abs(_node.x < middle ? (float)_node.x - middle : middle - (float)_node.x);
            if (_node.y > 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
            else if (_node.y < 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
            else x = -_node.x;

            return new HexagonalGridNode(x, _node.y);
        }

        public HexagonalGridSegment GetVerticalAxialSimmetry(HexagonalGridSegment _segment)
        {
            return new HexagonalGridSegment(GetVerticalAxialSymmetry(_segment.a), GetVerticalAxialSymmetry(_segment.b));
        }

        public HexagonalGridNode TranslateInSlice(HexagonalGridNode _node, int _amountOfSlices)
        {
            HexagonalGridSlice newSlice = (HexagonalGridSlice)(((int)_node.slice + _amountOfSlices) % 6);

            return HexagonalGridNode.PosFromSlice(_node.PosInSlice(), newSlice);
        }

        public HexagonalGridSegment RotateSegment(HexagonalGridSegment _segment, int _rotations)
        {
            return new HexagonalGridSegment(TranslateInSlice(_segment.a, _rotations), TranslateInSlice(_segment.b, _rotations));
        }
    }
}