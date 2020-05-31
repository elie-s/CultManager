using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager.HexagonalGrid
{
    [System.Serializable]
    public struct Node : System.IEquatable<Node>
    {
        public int x;
        public int y;
        public Slice slice;

        public Node(Vector2Int _pos)
        {
            x = _pos.x;
            y = _pos.y;
            slice = GetSlice(x, y);
        }

        public Node(int _x, int _y)
        {
            x = _x;
            y = _y;
            slice = GetSlice(x, y);
        }

        public Vector2Int PosInSlice()
        {
            switch (slice)
            {
                case Slice.Center:
                    return Vector2Int.zero;
                case Slice.Top:
                    return new Vector2Int(y, x);
                case Slice.TopRight:
                    return new Vector2Int(x, x - y);
                case Slice.BottomRight:
                    return new Vector2Int(x - y, -y);
                case Slice.Bottom:
                    return new Vector2Int(-y, -x);
                case Slice.BottomLeft:
                    return new Vector2Int(-x, y - x);
                case Slice.TopLeft:
                    return new Vector2Int(y - x, y);
                default:
                    return Vector2Int.zero;
            }
        }
        public Node[] GetNeighbours()
        {
            Node[] results = new Node[6]
            {
            new Node(x + 1, y + 0),
            new Node(x + 1, y + 1),
            new Node(x + 0, y + 1),
            new Node(x - 1, y + 0),
            new Node(x - 1, y - 1),
            new Node(x + 0, y - 1)
            };

            return results;
        }

        public Node ShiftFromCenter()
        {
            Vector2Int posInSlice = PosInSlice();

            Node result = PosFromSlice(new Vector2Int(posInSlice.x * 2, posInSlice.y * 2), slice);

            //Debug.Log(this + " -> " + result);

            return result;
        }

        #region Interfaces Implementations
        public bool Equals(Node other)
        {
            return x == other.x && y == other.y;
        }
        #endregion

        #region Static Methods
        public static Slice GetSlice(int _x, int _y)
        {
            if (_x == 0 && _y == 0) return Slice.Center;

            if (_x >= 0)
            {
                if (_y > 0)
                {
                    if (_x == 0 || _x < _y) return Slice.Top;
                    else return Slice.TopRight;
                }
                else if (_x != 0) return Slice.BottomRight;
            }

            if (_x <= 0)
            {
                if (_y < 0)
                {
                    if (_x == 0 || _y < _x) return Slice.Bottom;
                    else return Slice.BottomLeft;
                }
            }

            return Slice.TopLeft;
        }

        public static Node PosFromSlice(Vector2Int _posInSlice, Slice _slice)
        {
            switch (_slice)
            {
                case Slice.Center:
                    return Node.zero;
                case Slice.Top:
                    return new Node(_posInSlice.y, _posInSlice.x);
                case Slice.TopRight:
                    return new Node(_posInSlice.x, -(_posInSlice.y - _posInSlice.x));
                case Slice.BottomRight:
                    return new Node(_posInSlice.x - _posInSlice.y, -_posInSlice.y);
                case Slice.Bottom:
                    return new Node(-_posInSlice.y, -_posInSlice.x);
                case Slice.BottomLeft:
                    return new Node(-_posInSlice.x, _posInSlice.y - _posInSlice.x);
                case Slice.TopLeft:
                    return new Node(-(_posInSlice.x - _posInSlice.y), _posInSlice.y);
                default:
                    return Node.zero;
            }
        }

        public static Node PosFromSlice(Node _node, Slice _slice)
        {
            Vector2Int posInSlice = _node.PosInSlice();

            switch (_slice)
            {
                case Slice.Center:
                    return Node.zero;
                case Slice.Top:
                    return new Node(posInSlice.y, posInSlice.x);
                case Slice.TopRight:
                    return new Node(posInSlice.x, -(posInSlice.y - posInSlice.x));
                case Slice.BottomRight:
                    return new Node(posInSlice.x - posInSlice.y, -posInSlice.y);
                case Slice.Bottom:
                    return new Node(-posInSlice.y, -posInSlice.x);
                case Slice.BottomLeft:
                    return new Node(-posInSlice.x, posInSlice.y - posInSlice.x);
                case Slice.TopLeft:
                    return new Node(-(posInSlice.x - posInSlice.y), posInSlice.y);
                default:
                    return Node.zero;
            }
        }

        public static Node[] NodesInSlice(Slice _slice, int _layers)
        {
            List<Node> result = new List<Node>();

            for (int i = 1; i < _layers + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    result.Add(PosFromSlice(new Vector2Int(i, j), _slice));
                }
            }

            return result.ToArray();
        }

        public static Vector2 WorldPosition(Node _node, float _scale)
        {
            Vector2 xAxis = Vector2.right * (_node.x * _scale);
            return xAxis - new Vector2(_node.y * _scale * Mathf.Cos(-60 * Mathf.Deg2Rad), _node.y * _scale * Mathf.Sin(-60 * Mathf.Deg2Rad));
        }
        #endregion


        public static Node zero => new Node(Vector2Int.zero);

        public static bool operator ==(Node _nodeA, Node _nodeB)
        {
            return _nodeA.Equals(_nodeB);
        }

        public static bool operator !=(Node _nodeA, Node _nodeB)
        {
            return !_nodeA.Equals(_nodeB);
        }

        public override string ToString()
        {
            return "Node(" + x + ", " + y + ")";
        }
    }
}