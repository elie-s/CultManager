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
        public bool selected;
        public bool patternSegment;
        public bool canBeSelected;

        public Node a => segment.a;
        public Node b => segment.b;

        public PuzzleSegment(Segment _segment, BloodType _type = BloodType.none, bool _enabled = false)
        {
            segment = _segment;
            type = _type;
            selected = _enabled;
            patternSegment = false;
            canBeSelected = false;
        }

        public void SetAsPatternSegment()
        {
            patternSegment = true;
        }

        public void EnableSegment()
        {
            canBeSelected = true;
        }
        
        public void DisableSegment()
        {
            canBeSelected = false;
        }

        public void ToggleSegmentState()
        {
            canBeSelected = !canBeSelected;
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