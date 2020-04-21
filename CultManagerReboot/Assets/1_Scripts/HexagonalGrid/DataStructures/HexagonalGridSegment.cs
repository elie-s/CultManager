using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct HexagonalGridSegment : IEquatable<HexagonalGridSegment>
    {
        public HexagonalGridNode a;
        public HexagonalGridNode b;

        public HexagonalGridSegment(HexagonalGridNode _aNode, HexagonalGridNode _bNode)
        {
            a = _aNode;
            b = _bNode;
        }

        public bool ConnectedTo(HexagonalGridNode _node)
        {
            return _node == a || _node == b;
        }

        public bool ConnectedTo(HexagonalGridSegment _segment)
        {
            return a == _segment.a || a == _segment.b || b == _segment.a || b == _segment.b;
        }

        public bool Equals(HexagonalGridSegment other)
        {
            return (a == other.a && b == other.b) || (a == other.b && b == other.a);
        }

        public bool Contains(HexagonalGridNode _node)
        {
            return a == _node || b == _node;
        }

        public int Length(HexagonalGrid _grid)
        {
            int result = 0;



            return result;
        }

        public HexagonalGridSegment StretchSegment()
        {
            HexagonalGridNode _a = a.ShiftFromCenter();
            HexagonalGridNode _b = b.ShiftFromCenter();

            return new HexagonalGridSegment(_a, _b);
        }

        public HexagonalGridSegment[] CutSegment()
        {
            List<HexagonalGridSegment> result = new List<HexagonalGridSegment>();

            int x = Mathf.RoundToInt(a.x > b.x ? b.x + (float)(a.x - b.x) / 2.0f : a.x + (float)(b.x - a.x) / 2.0f);
            int y = Mathf.RoundToInt(a.y > b.y ? b.y + (float)(a.y - b.y) / 2.0f : a.y + (float)(b.y - a.y) / 2.0f);

            HexagonalGridNode middle = new HexagonalGridNode(x, y);

            result.Add(new HexagonalGridSegment(a, middle));
            result.Add(new HexagonalGridSegment(b, middle));

            return result.ToArray();
        }

        public override string ToString()
        {
            return "Segment(" + a + " ," + b + ")";
        }
    }
}