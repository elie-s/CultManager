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
            data.grid = new HexGrid(scale, 1);
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            data.grid = new HexGrid(scale, 1);
            data.pattern = new Pattern(data.grid, settings[0]);

            for (int i = 1; i < settings.Length; i++)
            {
                data.grid.DoubleGrid();
                data.pattern.DoubleSize();
                data.pattern.AddToShape(settings[i], true);
            }

            gridConstruction = data.pattern;
            Debug.Log(gridConstruction.stepSegments[0].Count);
            data.grid = new HexGrid(data.pattern);
            data.pattern = new Pattern(data.grid, patternSettings);
            //Debug.Log("Segment amount: " + pattern.segments.Count);

            display?.DisplayPuzzle(scale);
        }

        [ContextMenu("Generate2")]
        public void Generate2()
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

            display?.DisplayPuzzle2(scale);
        }

        private void OnDrawGizmosSelected()
        {
            if (data.grid != null && gridConstruction.stepSegments == null)
            {
                foreach (Segment segment in data.grid.segments)
                {
                    //Gizmos.color = Color.blue;
                    //Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.a), 0.05f);
                    //Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.b), 0.05f);
                    //Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.85f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(data.grid.NodeToWorldPosition(segment.a), data.grid.NodeToWorldPosition(segment.b));
                }
            }
            else if (gridConstruction.stepSegments != null)
            {
                for (int i = 0; i < gridConstruction.stepSegments.Count; i++)
                {
                    Debug.Log(gridConstruction.stepSegments[i].Count);
                    foreach (Segment segment in gridConstruction.stepSegments[i])
                    {

                        //Gizmos.color = Color.blue;
                        //Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.a), 0.05f);
                        //Gizmos.DrawSphere(data.grid.NodeToWorldPosition(segment.b), 0.05f);
                        //Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.85f);
                        Gizmos.color = i == 0 ? Color.red : (i == 1 ? Color.magenta : Color.green);
                        Gizmos.DrawLine(data.grid.NodeToWorldPosition(segment.a), data.grid.NodeToWorldPosition(segment.b));
                    }
                }
            }

            if (data.pattern != null)
            {
                Gizmos.color = new Color(1.0f, 0.92f, 0.016f, 0.75f);

                foreach (Segment segment in data.pattern.segments)
                {
                    Gizmos.DrawLine(data.grid.NodeToWorldPosition(segment.a), data.grid.NodeToWorldPosition(segment.b));
                }
            }
        }
    }
}