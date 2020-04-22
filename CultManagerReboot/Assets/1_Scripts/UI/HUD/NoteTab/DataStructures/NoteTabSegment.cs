using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using Grid = CultManager.HexagonalGrid.HexGrid;

namespace CultManager
{
    [System.Serializable]
    public class NoteTabSegment
    {
        public float[] node1= new float[2];
        public float[] node2= new float[2];
        public int iconIndex;

        public NoteTabSegment(Segment _segment, int _iconIndex)
        {
            node1[0] = _segment.a.x;
            node1[1] = _segment.a.y;
            node2[0] = _segment.b.x;
            node2[1] = _segment.b.y;
            iconIndex = _iconIndex;
        }

        public void SetIconIndex(int _iconIndex)
        {
            iconIndex = _iconIndex;
        }
    }
}

