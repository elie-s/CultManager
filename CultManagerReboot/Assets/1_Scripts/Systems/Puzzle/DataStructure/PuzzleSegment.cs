using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    [System.Serializable]
    public class PuzzleSegment
    {
        public Segment segment;
        public BloodType type;
        public bool enabled;
        public bool patternSegment;

        public Node a => segment.a;
        public Node b => segment.b;

        public PuzzleSegment(Segment _segment, BloodType _type = BloodType.none, bool _enabled = false)
        {
            segment = _segment;
            type = _type;
            enabled = _enabled;
            patternSegment = false;
        }

        public void SetAsPatternSegment()
        {
            patternSegment = true;
        }

        public bool IsSegment(Segment _segment)
        {
            return segment.Equals(_segment);
        }

        public bool IsConnected(PuzzleSegment _segment)
        {
            return _segment.segment.ConnectedTo(segment);
        }
    }
}