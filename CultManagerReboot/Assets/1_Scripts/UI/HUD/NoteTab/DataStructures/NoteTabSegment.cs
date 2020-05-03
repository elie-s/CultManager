﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;
using Grid = CultManager.HexagonalGrid.HexGrid;

namespace CultManager
{
    [System.Serializable]
    public class NoteTabSegment
    {
        public Segment segment;
        public int colorIndex;

        public NoteTabSegment(Segment _segment, int _colorIndex)
        {
            colorIndex = _colorIndex;
        }

        public void SetColorIndex(int _colorIndex)
        {
            colorIndex = _colorIndex;
        }
    }
}

