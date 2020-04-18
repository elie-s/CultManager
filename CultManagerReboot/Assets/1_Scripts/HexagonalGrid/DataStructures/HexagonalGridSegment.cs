using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexagonalGridSegment : IEquatable<HexagonalGridSegment>
{
    public Vector2Int a;
    public Vector2Int b;

    public HexagonalGridSegment(Vector2Int _aNode, Vector2Int _bNode)
    {
        a = _aNode;
        b = _bNode;
    }

    public bool ConnectedTo(Vector2Int _node)
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

    public bool Contains(Vector2Int _node)
    {
        return a == _node || b == _node;
    }

    public int Length(HexagonalGrid _grid)
    {
        int result = 0;



        return result;
    }

    public override string ToString()
    {
        return "Segment("+a+" ,"+b+")";
    }
}
