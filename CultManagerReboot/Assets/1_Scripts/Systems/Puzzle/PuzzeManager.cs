using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class PuzzeManager : MonoBehaviour
    {
        [SerializeField] private PuzzleData data = default;
        [SerializeField] private PuzzleDisplay display = default;
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private PatternGenerationSettings[] settings;
        [SerializeField] private PatternGenerationSettings patternSettings;

        private Pattern gridConstruction;

        private void Start()
        {
            if (data.puzzle != null && data.puzzle.Count > 0) display?.DisplayPuzzle(scale);
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            HexGrid grid = new HexGrid(scale, 1);
            Pattern pattern = new Pattern(grid, settings[0]);
            data.puzzle = new List<PuzzleSegment>();

            for (int i = 1; i < settings.Length; i++)
            {
                grid.DoubleGrid();
                pattern.DoubleSize();
                pattern.AddToShape(settings[i], true);
            }

            for (int i = 0; i < pattern.stepSegments.Count; i++)
            {
                for (int j = 0; j < pattern.stepSegments[i].Count; j++)
                {
                    data.puzzle.Add(new PuzzleSegment(pattern.stepSegments[i][j], (BloodType)(i + 1)));
                }
            }

            grid = new HexGrid(pattern);
            pattern = new Pattern(grid, patternSettings);

            foreach (PuzzleSegment segment in data.puzzle)
            {
                for (int i = 0; i < pattern.segments.Count; i++)
                {
                    if(segment.IsSegment(pattern.segments[i]))
                    {
                        segment.SetAsPatternSegment();
                        break;
                    }
                }
            }

            display?.DisplayPuzzle(scale);
        }


        /*public bool ValidateConnections()
        {
            int ;
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].selected)
                {
                    PuzzleSegment current = data.puzzle[i];
                }                
                int ctr = 0;
                for (int j = 0; j < data.puzzle.Count; j++)
                {
                    if (current.IsConnected(data.puzzle[j]))
                    {
                        ctr++;
                    }
                }
                if()
            }
        }*/

        private void OnDrawGizmosSelected()
        {
            if(data.puzzle != null)
            {
                foreach (PuzzleSegment segment in data.puzzle)
                {
                    Gizmos.color = segment.patternSegment ? Color.yellow : Color.blue;
                    Gizmos.DrawLine(Node.WorldPosition(segment.a, scale) + (Vector2)transform.position, Node.WorldPosition(segment.b, scale) + (Vector2)transform.position);
                }
            }
        }
    }
}