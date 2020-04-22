using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager.HexagonalGrid
{
    [System.Serializable]
    public class HexGrid
    {
        public float scale;
        public List<Node> nodes;
        public List<Segment> segments;
        public int layers;

        public HexGrid(float _scale, int _layers)
        {
            scale = _scale;
            layers = _layers;

            SetGrid();
        }

        public HexGrid(Pattern _pattern)
        {
            scale = _pattern.grid.scale;
            layers = _pattern.grid.layers;

            nodes = _pattern.nodes;
            segments = _pattern.segments;
        }

        public void SetGrid()
        {
            nodes = new List<Node>();
            segments = new List<Segment>();

            nodes.Add(Node.zero);

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
                Node[] sliceNodes = Node.NodesInSlice((Slice)s, layers);

                foreach (Node node in sliceNodes)
                {
                    if (!nodes.Contains(node)) nodes.Add(node);
                     else Debug.Log("bug !");
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                Node[] neighbours = nodes[i].GetNeighbours();

                for (int j = 0; j < neighbours.Length; j++)
                {
                    Segment segment = new Segment(nodes[i], neighbours[j]);
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

        public Vector2 NodeToWorldPosition(Node _node)
        {
            Vector2 xAxis = Vector2.right * (_node.x * scale);
            return xAxis - new Vector2(_node.y * scale * Mathf.Cos(-60 * Mathf.Deg2Rad), _node.y * scale * Mathf.Sin(-60 * Mathf.Deg2Rad));
        }

        public Segment[] SegmentsUsingNode(Node _node)
        {
            List<Segment> results = new List<Segment>();

            foreach (Segment segment in segments)
            {
                if (segment.ConnectedTo(_node)) results.Add(segment);
            }

            return results.ToArray();
        }

        public Segment RandomSegmentFromNode(Node _node)
        {
            Segment[] segments = SegmentsUsingNode(_node);

            return segments[Random.Range(0, segments.Length)];
        }

        public Node GetRandomNode()
        {
            return nodes[Random.Range(0, nodes.Count)];
        }

        public Node GetVerticalAxialSymmetry(Node _node)
        {
            int x = 0;
            float middle = (float)_node.y / 2.0f;

            float difference = Mathf.Abs(_node.x < middle ? (float)_node.x - middle : middle - (float)_node.x);
            if (_node.y > 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
            else if (_node.y < 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
            else x = -_node.x;

            return new Node(x, _node.y);
        }

        public Segment GetVerticalAxialSimmetry(Segment _segment)
        {
            return new Segment(GetVerticalAxialSymmetry(_segment.a), GetVerticalAxialSymmetry(_segment.b));
        }

        public Node TranslateInSlice(Node _node, int _amountOfSlices)
        {
            Slice newSlice = (Slice)(((int)_node.slice + _amountOfSlices) % 6);

            return Node.PosFromSlice(_node.PosInSlice(), newSlice);
        }

        public Segment RotateSegment(Segment _segment, int _rotations)
        {
            return new Segment(TranslateInSlice(_segment.a, _rotations), TranslateInSlice(_segment.b, _rotations));
        }
    }
}