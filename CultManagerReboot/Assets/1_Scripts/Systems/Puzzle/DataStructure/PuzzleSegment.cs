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

        public PuzzleSegment[] GetNeighbours(PuzzleSegment[] _segments)
        {
            List<PuzzleSegment> results = new List<PuzzleSegment>();

            foreach (PuzzleSegment other in _segments)
            {
                if (segment.ConnectedTo(other.segment)) results.Add(other);
            }

            return results.ToArray();
        }

        public PuzzleSegment[] GetNeighbours(List<PuzzleSegment> _segments)
        {
            List<PuzzleSegment> results = new List<PuzzleSegment>();

            foreach (PuzzleSegment other in _segments)
            {
                if (IsConnected(other) && !IsSegment(other.segment)) results.Add(other);
            }

            return results.ToArray();
        }

        public PuzzleSegment[] GetSelectedNeighbours(List<PuzzleSegment> _segments)
        {
            List<PuzzleSegment> results = new List<PuzzleSegment>();

            foreach (PuzzleSegment other in _segments)
            {
                if (IsConnected(other) && !IsSegment(other.segment) && other.selected) results.Add(other);
            }

            return results.ToArray();
        }

        public bool IsEndPoint(List<PuzzleSegment> _segments)
        {
            PuzzleSegment[] selectedNeighbours = GetSelectedNeighbours(_segments);

            bool aEnd = true;
            bool bEnd = true;

            foreach (PuzzleSegment other in selectedNeighbours)
            {
                if (segment.Equals(other)) continue;

                if (other.segment.ConnectedTo(a)) aEnd = false;
                if (other.segment.ConnectedTo(b)) bEnd = false;

                if (!aEnd && !bEnd)
                {
                    //Debug.Log("Isn't Endpoint");
                    return false;
                }
            }

            //Debug.Log("Is Endpoint");
            return true;
        }

        public void UpdateStatus(List<PuzzleSegment> _segments)
        {
            PuzzleSegment[] selectedNeighbours = GetSelectedNeighbours(_segments);

            if (selected)
            {
                if (IsEndPoint(_segments)) EnableSegment();
                else DisableSegment();
            }
            else
            {
                //Debug.Log(selectedNeighbours.Length);
                if (selectedNeighbours.Length > 0) EnableSegment();
                else DisableSegment();
            }
        }
    }
}