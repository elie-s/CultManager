using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager.HexagonalGrid
{
    [System.Serializable]
    public struct Segment : IEquatable<Segment>
    {
        public Node a;
        public Node b;

        public Segment(Node _aNode, Node _bNode)
        {
            a = _aNode;
            b = _bNode;
        }

        public bool ConnectedTo(Node _node)
        {
            return _node == a || _node == b;
        }

        public bool ConnectedTo(Segment _segment)
        {
            return a == _segment.a || a == _segment.b || b == _segment.a || b == _segment.b;
        }

        public bool Equals(Segment other)
        {
            return (a == other.a && b == other.b) || (a == other.b && b == other.a);
        }

        public bool Contains(Node _node)
        {
            return a == _node || b == _node;
        }

        public int Length(HexGrid _grid)
        {
            int result = 0;



            return result;
        }

        public Segment StretchSegment()
        {
            Node _a = a.ShiftFromCenter();
            Node _b = b.ShiftFromCenter();

            return new Segment(_a, _b);
        }

        public Segment[] CutSegment()
        {
            List<Segment> result = new List<Segment>();

            int x = Mathf.RoundToInt(a.x > b.x ? b.x + (float)(a.x - b.x) / 2.0f : a.x + (float)(b.x - a.x) / 2.0f);
            int y = Mathf.RoundToInt(a.y > b.y ? b.y + (float)(a.y - b.y) / 2.0f : a.y + (float)(b.y - a.y) / 2.0f);

            Node middle = new Node(x, y);

            result.Add(new Segment(a, middle));
            result.Add(new Segment(b, middle));

            return result.ToArray();
        }

        public override string ToString()
        {
            return "Segment(" + a + " ," + b + ")";
        }
    }
}