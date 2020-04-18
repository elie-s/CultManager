using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class HexagonalGrid
{
    public float scale;
    public List<Vector2Int> nodes;
    public List<HexagonalGridSegment> segments;
    public int layers;

    public HexagonalGrid(float _scale, int _layers)
    {
        scale = _scale;
        layers = _layers;

        SetGrid();
    }

    public void SetGrid()
    {
        nodes = new List<Vector2Int>();
        segments = new List<HexagonalGridSegment>();

        nodes.Add(new Vector2Int(0, 0));

        for (int s = 0; s < 6; s++)
        {
            for (int i = 1; i < layers + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Vector2Int newNode = PosFromSlice(new Vector2Int(i, j), (HexagonalGridSlice)s);
                    if (!nodes.Contains(newNode)) nodes.Add(newNode);
                    else Debug.Log("bug !");
                }
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int[] neighbours = GetNeighbours(nodes[i]);

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

    public Vector2 NodeToWorldPosition(Vector2Int _node)
    {
        Vector2 xAxis = Vector2.right * (_node.x * scale);
        return xAxis - new Vector2(_node.y * scale * Mathf.Cos(-60 * Mathf.Deg2Rad), _node.y * scale * Mathf.Sin(-60 * Mathf.Deg2Rad));
    }

    public Vector2Int[] GetNeighbours(Vector2Int _node)
    {
        Vector2Int[] results = new Vector2Int[6]
        {
            new Vector2Int(_node.x + 1, _node.y + 0),
            new Vector2Int(_node.x + 1, _node.y + 1),
            new Vector2Int(_node.x + 0, _node.y + 1),
            new Vector2Int(_node.x - 1, _node.y + 0),
            new Vector2Int(_node.x - 1, _node.y - 1),
            new Vector2Int(_node.x + 0, _node.y - 1)
        };

        return results;
    }

    public HexagonalGridSegment[] SegmentsUsingNode(Vector2Int _node)
    {
        List<HexagonalGridSegment> results = new List<HexagonalGridSegment>();

        foreach (HexagonalGridSegment segment in segments)
        {
            if (segment.ConnectedTo(_node)) results.Add(segment);
        }

        return results.ToArray();
    }

    public HexagonalGridSegment RandomSegmentFromNode(Vector2Int _node)
    {
        HexagonalGridSegment[] segments = SegmentsUsingNode(_node);

        return segments[Random.Range(0, segments.Length)];
    }

    public Vector2Int GetRandomNode()
    {
        return nodes[Random.Range(0, nodes.Count)];
    }

    public Vector2Int GetVerticalAxialSimmetry(Vector2Int _node)
    {
        int x = 0;
        float middle = (float)_node.y / 2.0f;

        float difference = Mathf.Abs(_node.x < middle ? (float)_node.x - middle : middle - (float)_node.x);
        if (_node.y > 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
        else if (_node.y < 0) x = _node.x < middle ? _node.x + Mathf.RoundToInt(2 * difference) : _node.x - Mathf.RoundToInt(2 * difference);
        else x = -_node.x;

        return new Vector2Int(x, _node.y);
    }

    public HexagonalGridSegment GetVerticalAxialSimmetry(HexagonalGridSegment _segment)
    {
        return new HexagonalGridSegment(GetVerticalAxialSimmetry(_segment.a), GetVerticalAxialSimmetry(_segment.b));
    }

    public HexagonalGridSlice GetSlice(Vector2Int _node)
    {
        if (_node == Vector2Int.zero) return HexagonalGridSlice.Center;

        if(_node.x >= 0)
        {
            if (_node.y > 0)
            {
                if (_node.x == 0 || _node.x < _node.y) return HexagonalGridSlice.Top;
                else return HexagonalGridSlice.TopRight;
            }
            else if (_node.x != 0) return HexagonalGridSlice.BottomRight;
        }

        if(_node.x <= 0)
        {
            if(_node.y < 0)
            {
                if (_node.x == 0 || _node.y < _node.x) return HexagonalGridSlice.Bottom;
                else return HexagonalGridSlice.BottomLeft;
            }
        }

        return HexagonalGridSlice.TopLeft;
    }

    public Vector2Int PosInSlice(Vector2Int _node)
    {
        HexagonalGridSlice slice = GetSlice(_node);

        switch (slice)
        {
            case HexagonalGridSlice.Center:
                return Vector2Int.zero;
            case HexagonalGridSlice.Top:
                return new Vector2Int(_node.y, _node.x);
            case HexagonalGridSlice.TopRight:
                return new Vector2Int(_node.x, _node.x - _node.y);
            case HexagonalGridSlice.BottomRight:
                return new Vector2Int(_node.x - _node.y, -_node.y);
            case HexagonalGridSlice.Bottom:
                return new Vector2Int(-_node.y, -_node.x);
            case HexagonalGridSlice.BottomLeft:
                return new Vector2Int(-_node.x, _node.y - _node.x);
            case HexagonalGridSlice.TopLeft:
                return new Vector2Int(_node.y - _node.x, _node.y);
            default:
                return Vector2Int.zero;
        }
    }

    public Vector2Int PosFromSlice(Vector2Int _posInSlice, HexagonalGridSlice _slice)
    {
        switch (_slice)
        {
            case HexagonalGridSlice.Center:
                return Vector2Int.zero;
            case HexagonalGridSlice.Top:
                return new Vector2Int(_posInSlice.y, _posInSlice.x);
            case HexagonalGridSlice.TopRight:
                return new Vector2Int(_posInSlice.x, -(_posInSlice.y - _posInSlice.x));
            case HexagonalGridSlice.BottomRight:
                return new Vector2Int(_posInSlice.x - _posInSlice.y, -_posInSlice.y);
            case HexagonalGridSlice.Bottom:
                return new Vector2Int(-_posInSlice.y, -_posInSlice.x);
            case HexagonalGridSlice.BottomLeft:
                return new Vector2Int(-_posInSlice.x, _posInSlice.y - _posInSlice.x);
            case HexagonalGridSlice.TopLeft:
                return new Vector2Int(-(_posInSlice.x - _posInSlice.y), _posInSlice.y);
            default:
                return Vector2Int.zero;
        }
    }

    public Vector2Int[] NodesInSlice(HexagonalGridSlice _slice)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int i = 1; i < layers + 1; i++)
        {
            for (int j = 0; j < i; j++)
            {
                result.Add(PosFromSlice(new Vector2Int(i, j), _slice));
            }
        }

        return result.ToArray();
    }

    public Vector2Int MoveNodeAway(Vector2Int _node)
    {
        Vector2Int posInSlice = PosInSlice(_node);
        HexagonalGridSlice slice = GetSlice(_node);

        Vector2Int result = PosFromSlice(new Vector2Int(posInSlice.x * 2, posInSlice.y * 2), slice);

        Debug.Log(_node + " -> " + posInSlice + " (" + slice + ") -> " + result);

        return result;
    }

    public HexagonalGridSegment StretchSegment(HexagonalGridSegment _segment)
    {
        Vector2Int a = MoveNodeAway(_segment.a);
        Vector2Int b = MoveNodeAway(_segment.b);

        Debug.Log(_segment + " -> " + new HexagonalGridSegment(a, b));

        return new HexagonalGridSegment(a, b);
    }

    public HexagonalGridSegment[] CutSegment(HexagonalGridSegment _segment)
    {
        List<HexagonalGridSegment> result = new List<HexagonalGridSegment>();

        int x = Mathf.RoundToInt(_segment.a.x > _segment.b.x ? _segment.b.x + (float)(_segment.a.x - _segment.b.x) / 2.0f : _segment.a.x + (float)(_segment.b.x - _segment.a.x) / 2.0f);
        int y = Mathf.RoundToInt(_segment.a.y > _segment.b.y ? _segment.b.y + (float)(_segment.a.y - _segment.b.y) / 2.0f : _segment.a.y + (float)(_segment.b.y - _segment.a.y) / 2.0f);

        Vector2Int middle = new Vector2Int(x, y);
        result.Add(new HexagonalGridSegment(_segment.a, middle));
        result.Add(new HexagonalGridSegment(_segment.b, middle));

        return result.ToArray();
    }

    public Vector2Int TranslateInSlice(Vector2Int _node, int _amountOfSlices)
    {
        HexagonalGridSlice newSlice = (HexagonalGridSlice)(((int)GetSlice(_node) + _amountOfSlices) % 6);

        return PosFromSlice(PosInSlice(_node), newSlice);
    }

    public HexagonalGridSegment RotateSegment(HexagonalGridSegment _segment, int _rotations)
    {
        return new HexagonalGridSegment(TranslateInSlice(_segment.a, _rotations), TranslateInSlice(_segment.b, _rotations));
    }
}
