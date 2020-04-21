using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class HexagonalGridPattern
    {
        public List<HexagonalGridSegment> segments;
        public bool startAtCenter;

        public HexagonalGrid grid { get; private set; }
        public List<HexagonalGridNode> nodes { get; private set; }
        private Mode mode;

        public HexagonalGridPattern(HexagonalGrid _grid, bool _center, Mode _mode)
        {
            grid = _grid;
            startAtCenter = _center;
            segments = new List<HexagonalGridSegment>();
            nodes = new List<HexagonalGridNode>();
            mode = _mode;

            NewShape(3);
        }

        public HexagonalGridPattern(HexagonalGrid _grid, HexagonalGridPatternGenerationsSettings _settings)
        {
            segments = new List<HexagonalGridSegment>();
            nodes = new List<HexagonalGridNode>();

            grid = _grid;

            startAtCenter = _settings.startAtCenter;
            mode = _settings.mode;
            NewShape(_settings.shapeSegments);
        }

        public void AddToShape(HexagonalGridPatternGenerationsSettings _settings, bool _mustBeConnected)
        {
            mode = _settings.mode;
            startAtCenter = _settings.startAtCenter;

            HexagonalGridNode currentNode = startAtCenter ? grid.nodes[0] : grid.nodes[Random.Range(0, grid.nodes.Count)];
            HexagonalGridSegment segment = grid.RandomSegmentFromNode(currentNode);

            AddSegment(segment, _mustBeConnected);

            for (int i = 1; i < _settings.shapeSegments; i++)
            {
                int safety = 0;
                do
                {
                    safety++;
                    currentNode = nodes[Random.Range(0, nodes.Count)];
                    int protection = 0;
                    do
                    {
                        segment = grid.RandomSegmentFromNode(currentNode);
                        protection++;
                    } while (segments.Contains(segment) && protection < 36);
                } while (segments.Contains(segment) && safety < 36);


                if (safety < 36) AddSegment(segment, _mustBeConnected);
            }
        }

        public void NewMode(Mode _mode)
        {
            mode = _mode;
        }

        public void StartAtCenter(bool _value)
        {
            startAtCenter = _value;
        }

        public void NewShape(int _segmentCounts)
        {
            segments = new List<HexagonalGridSegment>();
            nodes = new List<HexagonalGridNode>();

            HexagonalGridNode currentNode = startAtCenter ? grid.nodes[0] : grid.nodes[Random.Range(0, grid.nodes.Count)];
            HexagonalGridSegment segment = grid.RandomSegmentFromNode(currentNode);

            AddSegment(segment, false);

            for (int i = 1; i < _segmentCounts; i++)
            {
                int safety = 0;
                do
                {
                    safety++;
                    currentNode = nodes[Random.Range(0, nodes.Count)];
                    int protection = 0;
                    do
                    {
                        segment = grid.RandomSegmentFromNode(currentNode);
                        protection++;
                    } while (segments.Contains(segment) && protection < 36);
                } while (segments.Contains(segment) && safety < 36);


                if (safety < 36) AddSegment(segment, false);
            }
        }

        private void NewSegment(HexagonalGridSegment _segment, bool _mustBeConnected)
        {
            if (!segments.Contains(_segment))
            {
                if ((_mustBeConnected && (nodes.Contains(_segment.a) || nodes.Contains(_segment.b))) || !_mustBeConnected)
                {
                    segments.Add(_segment);

                    if (!nodes.Contains(_segment.a)) nodes.Add(_segment.a);
                    if (!nodes.Contains(_segment.b)) nodes.Add(_segment.b);
                }
            }
        }

        private void AddSegment(HexagonalGridSegment _segment, bool _mustBeConnected)
        {
            NewSegment(_segment, _mustBeConnected);

            switch (mode)
            {
                case Mode.SymmetryAxial:
                    NewSegment(grid.GetVerticalAxialSimmetry(_segment), _mustBeConnected);
                    break;
                case Mode.SymmetryRotation3:
                    NewSegment(grid.RotateSegment(_segment, 2), _mustBeConnected);
                    NewSegment(grid.RotateSegment(_segment, 4), _mustBeConnected);
                    break;
                case Mode.SymmetryRotation6:
                    NewSegment(grid.RotateSegment(_segment, 1), _mustBeConnected);
                    NewSegment(grid.RotateSegment(_segment, 2), _mustBeConnected);
                    NewSegment(grid.RotateSegment(_segment, 3), _mustBeConnected);
                    NewSegment(grid.RotateSegment(_segment, 4), _mustBeConnected);
                    NewSegment(grid.RotateSegment(_segment, 5), _mustBeConnected);
                    break;
                default:
                    return;
            }
        }

        public void DoubleSize()
        {
            List<HexagonalGridSegment> tmpSegments = new List<HexagonalGridSegment>();

            HexagonalGridSegment tmpSegment;

            foreach (HexagonalGridSegment segment in segments)
            {
                tmpSegment = segment.StretchSegment();

                HexagonalGridSegment[] newSegments = tmpSegment.CutSegment();

                if (!tmpSegments.Contains(newSegments[0])) tmpSegments.Add(newSegments[0]);
                if (!tmpSegments.Contains(newSegments[1])) tmpSegments.Add(newSegments[1]);
            }

            segments = tmpSegments;
            nodes = new List<HexagonalGridNode>();

            foreach (HexagonalGridSegment segment in segments)
            {
                if (!nodes.Contains(segment.a)) nodes.Add(segment.a);
                if (!nodes.Contains(segment.b)) nodes.Add(segment.b);
            }
        }


        public enum Mode
        {
            NoSymmetry,
            SymmetryAxial,
            SymmetryRotation3,
            SymmetryRotation6
        }
    }
}