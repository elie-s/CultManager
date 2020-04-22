using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CultManager.HexagonalGrid
{
    [System.Serializable]
    public class Pattern
    {
        public List<Segment> segments;
        public bool startAtCenter;

        public HexGrid grid { get; private set; }
        public List<Node> nodes { get; private set; }
        private Mode mode;

        public List<List<Segment>> stepSegments;

        public Pattern(HexGrid _grid, bool _center, Mode _mode)
        {
            grid = _grid;
            startAtCenter = _center;
            segments = new List<Segment>();
            nodes = new List<Node>();
            mode = _mode;

            stepSegments = new List<List<Segment>>();

            NewShape(3);
        }

        public Pattern(HexGrid _grid, PatternGenerationSettings _settings)
        {
            segments = new List<Segment>();
            nodes = new List<Node>();

            grid = _grid;

            startAtCenter = _settings.startAtCenter;
            mode = _settings.mode;
            NewShape(_settings.shapeSegments);
        }

        public Pattern(Segment[] _segments, HexGrid _grid)
        {
            segments = _segments.ToList();
            nodes = new List<Node>();

            foreach (Segment segment in segments)
            {
                if (!nodes.Contains(segment.a)) nodes.Add(segment.a);
                if (!nodes.Contains(segment.b)) nodes.Add(segment.b);
            }

            grid = _grid;

            startAtCenter = false;
            mode = Mode.NoSymmetry;

        }

        public void AddToShape(PatternGenerationSettings _settings, bool _mustBeConnected)
        {
            mode = _settings.mode;
            startAtCenter = _settings.startAtCenter;
            stepSegments.Add(new List<Segment>());

            Node currentNode = startAtCenter ? grid.nodes[0] : grid.nodes[Random.Range(0, grid.nodes.Count)];
            Segment segment = grid.RandomSegmentFromNode(currentNode);

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

        private void SaveAddedSgements(List<Segment> _segments)
        {
            stepSegments.Add(_segments);
        }

        private void AddStepSegment(Segment _segment)
        {
            stepSegments[stepSegments.Count - 1].Add(_segment);
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
            segments = new List<Segment>();
            nodes = new List<Node>();
            stepSegments = new List<List<Segment>>();
            stepSegments.Add(new List<Segment>());

            Node currentNode = startAtCenter ? grid.nodes[0] : grid.nodes[Random.Range(0, grid.nodes.Count)];
            Segment segment = grid.RandomSegmentFromNode(currentNode);

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

        private void NewSegment(Segment _segment, bool _mustBeConnected)
        {
            if (!segments.Contains(_segment))
            {
                if ((_mustBeConnected && (nodes.Contains(_segment.a) || nodes.Contains(_segment.b))) || !_mustBeConnected)
                {
                    segments.Add(_segment);

                    if (!nodes.Contains(_segment.a)) nodes.Add(_segment.a);
                    if (!nodes.Contains(_segment.b)) nodes.Add(_segment.b);

                    AddStepSegment(_segment);
                }
            }
        }

        private void AddSegment(Segment _segment, bool _mustBeConnected)
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
            List<Segment> tmpSegments = new List<Segment>();

            Segment tmpSegment;

            foreach (Segment segment in segments)
            {
                tmpSegment = segment.StretchSegment();

                Segment[] newSegments = tmpSegment.CutSegment();

                if (!tmpSegments.Contains(newSegments[0])) tmpSegments.Add(newSegments[0]);
                if (!tmpSegments.Contains(newSegments[1])) tmpSegments.Add(newSegments[1]);
            }

            segments = tmpSegments;
            nodes = new List<Node>();

            foreach (Segment segment in segments)
            {
                if (!nodes.Contains(segment.a)) nodes.Add(segment.a);
                if (!nodes.Contains(segment.b)) nodes.Add(segment.b);
            }

            List<List<Segment>> tmpStepSegments = new List<List<Segment>>();

            foreach (List<Segment> segmentList in stepSegments)
            {
                List<Segment> tmpList = new List<Segment>();

                foreach (Segment segment in segmentList)
                {
                    Segment tmpSegment2 = segment.StretchSegment();

                    tmpList.Add(tmpSegment2.CutSegment()[0]);
                    tmpList.Add(tmpSegment2.CutSegment()[1]);
                }

                tmpStepSegments.Add(tmpList);
            }

            stepSegments = tmpStepSegments;
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