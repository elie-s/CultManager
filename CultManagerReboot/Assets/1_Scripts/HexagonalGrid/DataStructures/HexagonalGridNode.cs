using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct HexagonalGridNode : System.IEquatable<HexagonalGridNode>
    {
        public int x;
        public int y;
        public HexagonalGridSlice slice;

        public HexagonalGridNode(Vector2Int _pos)
        {
            x = _pos.x;
            y = _pos.y;
            slice = GetSlice(x, y);
        }

        public HexagonalGridNode(int _x, int _y)
        {
            x = _x;
            y = _y;
            slice = GetSlice(x, y);
        }

        public Vector2Int PosInSlice()
        {
            switch (slice)
            {
                case HexagonalGridSlice.Center:
                    return Vector2Int.zero;
                case HexagonalGridSlice.Top:
                    return new Vector2Int(y, x);
                case HexagonalGridSlice.TopRight:
                    return new Vector2Int(x, x - y);
                case HexagonalGridSlice.BottomRight:
                    return new Vector2Int(x - y, -y);
                case HexagonalGridSlice.Bottom:
                    return new Vector2Int(-y, -x);
                case HexagonalGridSlice.BottomLeft:
                    return new Vector2Int(-x, y - x);
                case HexagonalGridSlice.TopLeft:
                    return new Vector2Int(y - x, y);
                default:
                    return Vector2Int.zero;
            }
        }
        public HexagonalGridNode[] GetNeighbours()
        {
            HexagonalGridNode[] results = new HexagonalGridNode[6]
            {
            new HexagonalGridNode(x + 1, y + 0),
            new HexagonalGridNode(x + 1, y + 1),
            new HexagonalGridNode(x + 0, y + 1),
            new HexagonalGridNode(x - 1, y + 0),
            new HexagonalGridNode(x - 1, y - 1),
            new HexagonalGridNode(x + 0, y - 1)
            };

            return results;
        }

        public HexagonalGridNode ShiftFromCenter()
        {
            Vector2Int posInSlice = PosInSlice();

            HexagonalGridNode result = PosFromSlice(new Vector2Int(posInSlice.x * 2, posInSlice.y * 2), slice);

            //Debug.Log(this + " -> " + result);

            return result;
        }

        #region Interfaces Implementations
        public bool Equals(HexagonalGridNode other)
        {
            return x == other.x && y == other.y;
        }
        #endregion

        #region Static Methods
        public static HexagonalGridSlice GetSlice(int _x, int _y)
        {
            if (_x == 0 && _y == 0) return HexagonalGridSlice.Center;

            if (_x >= 0)
            {
                if (_y > 0)
                {
                    if (_x == 0 || _x < _y) return HexagonalGridSlice.Top;
                    else return HexagonalGridSlice.TopRight;
                }
                else if (_x != 0) return HexagonalGridSlice.BottomRight;
            }

            if (_x <= 0)
            {
                if (_y < 0)
                {
                    if (_x == 0 || _y < _x) return HexagonalGridSlice.Bottom;
                    else return HexagonalGridSlice.BottomLeft;
                }
            }

            return HexagonalGridSlice.TopLeft;
        }

        public static HexagonalGridNode PosFromSlice(Vector2Int _posInSlice, HexagonalGridSlice _slice)
        {
            switch (_slice)
            {
                case HexagonalGridSlice.Center:
                    return HexagonalGridNode.zero;
                case HexagonalGridSlice.Top:
                    return new HexagonalGridNode(_posInSlice.y, _posInSlice.x);
                case HexagonalGridSlice.TopRight:
                    return new HexagonalGridNode(_posInSlice.x, -(_posInSlice.y - _posInSlice.x));
                case HexagonalGridSlice.BottomRight:
                    return new HexagonalGridNode(_posInSlice.x - _posInSlice.y, -_posInSlice.y);
                case HexagonalGridSlice.Bottom:
                    return new HexagonalGridNode(-_posInSlice.y, -_posInSlice.x);
                case HexagonalGridSlice.BottomLeft:
                    return new HexagonalGridNode(-_posInSlice.x, _posInSlice.y - _posInSlice.x);
                case HexagonalGridSlice.TopLeft:
                    return new HexagonalGridNode(-(_posInSlice.x - _posInSlice.y), _posInSlice.y);
                default:
                    return HexagonalGridNode.zero;
            }
        }

        public static HexagonalGridNode PosFromSlice(HexagonalGridNode _node, HexagonalGridSlice _slice)
        {
            Vector2Int posInSlice = _node.PosInSlice();

            switch (_slice)
            {
                case HexagonalGridSlice.Center:
                    return HexagonalGridNode.zero;
                case HexagonalGridSlice.Top:
                    return new HexagonalGridNode(posInSlice.y, posInSlice.x);
                case HexagonalGridSlice.TopRight:
                    return new HexagonalGridNode(posInSlice.x, -(posInSlice.y - posInSlice.x));
                case HexagonalGridSlice.BottomRight:
                    return new HexagonalGridNode(posInSlice.x - posInSlice.y, -posInSlice.y);
                case HexagonalGridSlice.Bottom:
                    return new HexagonalGridNode(-posInSlice.y, -posInSlice.x);
                case HexagonalGridSlice.BottomLeft:
                    return new HexagonalGridNode(-posInSlice.x, posInSlice.y - posInSlice.x);
                case HexagonalGridSlice.TopLeft:
                    return new HexagonalGridNode(-(posInSlice.x - posInSlice.y), posInSlice.y);
                default:
                    return HexagonalGridNode.zero;
            }
        }

        public static HexagonalGridNode[] NodesInSlice(HexagonalGridSlice _slice, int _layers)
        {
            List<HexagonalGridNode> result = new List<HexagonalGridNode>();

            for (int i = 1; i < _layers + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    result.Add(PosFromSlice(new Vector2Int(i, j), _slice));
                }
            }

            return result.ToArray();
        }
        #endregion


        public static HexagonalGridNode zero => new HexagonalGridNode(Vector2Int.zero);

        public static bool operator ==(HexagonalGridNode _nodeA, HexagonalGridNode _nodeB)
        {
            return _nodeA.Equals(_nodeB);
        }

        public static bool operator !=(HexagonalGridNode _nodeA, HexagonalGridNode _nodeB)
        {
            return !_nodeA.Equals(_nodeB);
        }

        public override string ToString()
        {
            return "Node(" + x + ", " + y + ")";
        }
    }
}